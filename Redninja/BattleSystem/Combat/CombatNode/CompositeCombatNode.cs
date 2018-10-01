using System.Collections.Generic;

namespace Redninja.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// This combat node represents one or several combat nodes.
    /// </summary>
    public class CompositeCombatNode : ICombatNode
    {
        private List<ICombatNode> children;

        public CompositeCombatNode()
        {
            children = new List<ICombatNode>();
        }

        public void AddNode(ICombatNode combatNode)
        {
            children.Add(combatNode);
        }

        public float GetProperty(CombatPropertyType property)
        {

            float total = 0;
            foreach (ICombatNode mod in children)
            {
                total += mod.GetProperty(property);
            }
            return total;

        }

        public float GetPropertyScalar(CombatPropertyType property)
        {

            float total = 0;
            foreach (ICombatNode mod in children)
            {
                total += mod.GetPropertyScalar(property);
            }
            return total;
        }

        public bool HasFlag(CombatFlag flag)
        {
            foreach (ICombatNode mod in children)
            {
                if (mod.HasFlag(flag))
                {
                    return true;
                }
            }
            return false;
        }
    } 
}


