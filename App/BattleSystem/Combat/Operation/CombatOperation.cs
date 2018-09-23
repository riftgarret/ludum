using App.BattleSystem.Combat.CombatNode;
using App.BattleSystem.Combat.Operation.Result;
using App.BattleSystem.Events;
using System.Collections.Generic;
using UnityEngine;

namespace App.BattleSystem.Combat.Operation
{
    /// <summary>
    /// Combat operation executes a grouping of Combat Logic nodes to pull together the resolving
    /// combat operations.
    /// </summary>
    public class CombatOperation : ICombatOperation
    {
        public delegate bool Evaluate(ICombatLogic[] conditions);

        private Configuration config;
        private CompositeLogic result;

        private CombatOperation(Configuration config)
        {
            this.config = config;
        }

        public void Execute()
        {
            result = new CompositeLogic();

            foreach (LogicBlob logic in config.orderedLogic)
            {
                logic.TryExecute(config.src, config.dest, result);
            }

        }

        public void GenerateEvents(Queue<IBattleEvent> queue)
        {
            result.GenerateEvents(config.src, config.dest, queue);
        }

        public class Builder
        {
            Dictionary<ICombatLogic, LogicBlob> logicBlobMap = new Dictionary<ICombatLogic, LogicBlob>();

            public LogicBlob AddLogic(ICombatLogic logic)
            {
                LogicBlob blob;
                logicBlobMap.TryGetValue(logic, out blob);
                if (blob == null)
                {
                    blob = new LogicBlob(this, logic);
                    logicBlobMap[logic] = blob;
                }
                return blob;
            }

            public CombatOperation Build(EntityCombatResolver src, EntityCombatResolver dest)
            {
                Configuration config = ResolveConfiguration();
                config.src = src;
                config.dest = dest;
                return new CombatOperation(config);
            }

            private class GraphNode
            {
                internal HashSet<GraphNode> incomingNodes;
                internal HashSet<GraphNode> outgoingNodes;
                internal ICombatLogic node;
                internal LogicBlob blob;
                internal GraphNode(ICombatLogic logic)
                {
                    node = logic;
                    incomingNodes = new HashSet<GraphNode>();
                    outgoingNodes = new HashSet<GraphNode>();
                }
            }

            private GraphNode GetNode(ICombatLogic logic, Dictionary<ICombatLogic, GraphNode> nodes)
            {
                GraphNode node;
                nodes.TryGetValue(logic, out node);
                if (node == null)
                {
                    node = new GraphNode(logic);
                    nodes[logic] = node;
                }
                return node;
            }

            private Configuration ResolveConfiguration()
            {
                Dictionary<ICombatLogic, GraphNode> nodes = new Dictionary<ICombatLogic, GraphNode>();

                // first generate all edges
                foreach (LogicBlob blob in logicBlobMap.Values)
                {
                    GraphNode logicNode = GetNode(blob.logic, nodes);
                    logicNode.blob = blob;
                    if (blob.conditionBlob != null)
                    {
                        foreach (ICombatLogic condition in blob.conditionBlob.conditions)
                        {
                            GraphNode conditionNode = GetNode(condition, nodes);
                            conditionNode.outgoingNodes.Add(logicNode);
                            logicNode.incomingNodes.Add(conditionNode);
                        }
                    }
                }

                /*

    L ← Empty list that will contain the sorted elements
    S ← Set of all nodes with no incoming edges
    while S is non-empty do
        remove a node n from S
        add n to tail of L
        for each node m with an edge e from n to m do
            remove edge e from the graph
            if m has no other incoming edges then
                insert m into S
    if graph has edges then
        return error (graph has at least one cycle)
    else 
        return L (a topologically sorted order)

                 */
                // turn set into a queue now that they are unique
                Queue<ICombatLogic> nodeQueue = new Queue<ICombatLogic>(nodes.Keys);
                LinkedList<LogicBlob> orderedNodes = new LinkedList<LogicBlob>();

                // topological sort
                while (nodeQueue.Count > 0)
                {
                    ICombatLogic cur = nodeQueue.Dequeue();

                    orderedNodes.AddLast(nodes[cur].blob);
                    GraphNode curNode = nodes[cur];
                    foreach (GraphNode outgoing in new List<GraphNode>(curNode.outgoingNodes))
                    {
                        if (outgoing.incomingNodes.Count > 0)
                        {
                            if (!nodeQueue.Contains(outgoing.node))
                            {
                                nodeQueue.Enqueue(outgoing.node);
                            }
                            orderedNodes.Remove(nodes[outgoing.node].blob);
                        }

                        curNode.outgoingNodes.Remove(outgoing);
                        outgoing.incomingNodes.Remove(curNode);
                    }
                    if (curNode.outgoingNodes.Count == 0 && curNode.incomingNodes.Count == 0)
                    {
                        nodes.Remove(curNode.node);
                    }
                }

                // sanity check
                if (nodes.Count > 0)
                {
                    Debug.LogError("Cycle detected in topilogical sort inside CombatOperation.Builder.ResolveConfiguration");
                }

                // TODO we have ordered, now we just need to ensure we execute them properly

                Configuration config = new Configuration();
                config.orderedLogic.AddRange(orderedNodes);
                return config;
            }
        }

        private static ConditionBlob EMPTY_CONDITION = new ConditionBlob(new ICombatLogic[0], delegate (ICombatLogic[] logicBlobs) { return true; });


        public class LogicBlob
        {
            Builder builder;

            internal ICombatLogic logic;
            internal ConditionBlob conditionBlob;

            internal LogicBlob(Builder builder, ICombatLogic logic)
            {
                this.builder = builder;
                this.logic = logic;
                this.conditionBlob = EMPTY_CONDITION;
            }

            public LogicBlob AddLogic(ICombatLogic logic)
            {
                return builder.AddLogic(logic);
            }

            public CombatOperation Build(EntityCombatResolver src, EntityCombatResolver dest)
            {
                return builder.Build(src, dest);
            }

            public Builder Require(Evaluate evalFunc, params ICombatLogic[] conditions)
            {
                foreach (ICombatLogic condLogic in conditions)
                {
                    builder.AddLogic(condLogic);
                }
                conditionBlob = new ConditionBlob(conditions, evalFunc);
                return builder;
            }

            internal void TryExecute(EntityCombatResolver src, EntityCombatResolver dest, CompositeLogic resultLogic)
            {
                if (conditionBlob.ShouldExecute())
                {
                    logic.Execute(src, dest);
                    resultLogic.Add(logic);
                }
            }
        }

        private class Configuration
        {
            public EntityCombatResolver src = null;
            public EntityCombatResolver dest = null;

            public List<LogicBlob> orderedLogic = new List<LogicBlob>();
        }

        public class ConditionBlob
        {
            internal ICombatLogic[] conditions;
            internal Evaluate evalFunc;
            internal ConditionBlob(ICombatLogic[] conditions, Evaluate evalFunc)
            {
                this.conditions = conditions;
                this.evalFunc = evalFunc;
            }

            internal bool ShouldExecute()
            {

                // check to make sure all have been executed
                foreach (ICombatLogic condition in conditions)
                {
                    if (!condition.IsExecuted)
                    {
                        return false;
                    }
                }

                return evalFunc.Invoke(conditions);
            }
        }
    } 
}


