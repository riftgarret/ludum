using App.BattleSystem.Actions;
using App.BattleSystem.Targeting;
using App.Core.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace App.BattleSystem.Entity
{
    /// <summary>
    /// This component needs to be rethought out.
    /// </summary>
    public class BattleEntityManager
    {
        public BattleEntity.OnDecisionRequired OnDecisionRequiredDelegate;
        public BattleEntity.OnExecuteOperation OnExecuteOperationDelegate;

        internal class ManagedEntity
        {
            internal BattleEntity Entity { get; set; }
            internal EntityPosition TilePosition { get => Entity.Position; }
            internal bool IsEnemy => Entity is EnemyBattleEntity;
        }

        private HashSet<ManagedEntity> entityMap = new HashSet<ManagedEntity>();


        public IEnumerable<BattleEntity> EnemyEntities
        {
            get
            {
                return from x in entityMap
                       where x.IsEnemy
                       select x.Entity as PCBattleEntity;
            }
        }
        
        public IEnumerable<PCBattleEntity> PCEntities
        {
            get
            {
                return from x in entityMap
                       where !x.IsEnemy
                       select x.Entity as PCBattleEntity;
            }
        }

        public IEnumerable<BattleEntity> AllEntities
        {
            get { return from x in entityMap select x.Entity; }
        }
        
        /// <summary>
        ///  TODO this will take a pattern and location and return all battle entities that are valid.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public IEnumerable<BattleEntity> GetPattern(int anchorRow, int anchorColumn, bool isEnemies, ITargetPattern pattern)
        {
            return (isEnemies ? EnemyEntities : PCEntities)
                .Where(entity =>
            {
                // need to check all squares that are within character
                EntityPosition position = entity.Position;
                for (int targetRow = 0; targetRow < position.Size; targetRow++)
                {
                    for (int targetColumn = 0; targetColumn < position.Size; targetColumn++)
                    {
                        if (pattern.IsInPattern(anchorRow, anchorColumn, targetRow, targetColumn))
                        {
                            return true;
                        }
                    }
                }
                return false;
            });
        }      

        /// <summary>
        /// Get Row of entities.
        /// </summary>
        /// <param name="anchorRow"></param>
        /// <param name="isEnemy"></param>
        /// <returns></returns>
        public IEnumerable<BattleEntity> GetRow(int anchorRow, bool isEnemy)
        {
            return GetPattern(anchorRow, 0, isEnemy, TargetPatternFactory.CreateRowPattern());
        }

        /// <summary>
        /// Populate entities.
        /// </summary>
        /// <param name="partyComponent"></param>
        /// <param name="enemyComponent"></param>
        public void LoadEntities(PartyComponent partyComponent, EnemyComponent enemyComponent)
        {
            LoadCharacters(partyComponent.Characters.ToArray(), enemyComponent.Characters.ToArray());
            InitializeBattlePhase();
        }         

        private void LoadCharacters(Character[] pcChars, Character[] enemyChars)
        {
            // temp assign tile positions
            int row = 0;
            int column = 0;
            foreach (PCCharacter c in pcChars)
            {
                ManagedEntity entity = new ManagedEntity();
                entity.Entity = new PCBattleEntity(c);
                entity.Entity.MovePosition(column++, row);
                entityMap.Add(entity);
            }

            column = 0;
            foreach(EnemyCharacter c in enemyChars)
            {
                ManagedEntity entity = new ManagedEntity();
                entity.Entity = new EnemyBattleEntity(c);
                entity.Entity.MovePosition(column++, row);
                entityMap.Add(entity);
            }

            foreach(ManagedEntity e in entityMap)
            {
                HookEntityDelegates(e.Entity);
            }
        }

        private void HookEntityDelegates(BattleEntity entity)
        {
            entity.OnDecisionRequiredDelegate = e => OnDecisionRequiredDelegate?.Invoke(e);
            entity.OnExecuteOperationDelegate = (e, operation) => OnExecuteOperationDelegate?.Invoke(e, operation); 
        }

        /// <summary>
        /// Initialize the battle phase, this sets the initial 'Initiative action' 
        /// </summary>
        private void InitializeBattlePhase() => AllEntities.ToList().ForEach(unit => unit.InitializeBattlePhase());

        /// <summary>
        /// Increments the time delta. This will update the internal time
        /// by adjusting it to the unit of time being used.
        /// </summary>
        /// <param name="deltaTime">Delta time.</param>
        public void IncrementTimeDelta(float deltaTime)
            => AllEntities.ToList().ForEach(unit => unit.IncrementGameClock(deltaTime));

        /// <summary>
        /// Set the action for this entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="action"></param>
        public void SetAction(BattleEntity entity, IBattleAction action)
            => entity.Action = action;        
    }     
}