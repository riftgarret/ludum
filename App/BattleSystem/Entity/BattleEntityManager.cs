using App.BattleSystem.Actions;
using App.Core.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            internal PCCharacter.RowPosition RowPosition { get; set; }
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

        public IEnumerable<PCBattleEntity> FrontRowEntities => GetRow(PCCharacter.RowPosition.FRONT);
        
        public IEnumerable<PCBattleEntity> MiddleRowEntities => GetRow(PCCharacter.RowPosition.MIDDLE);

        public IEnumerable<PCBattleEntity> BackRowEntities => GetRow(PCCharacter.RowPosition.BACK);


        /// <summary>
        /// Gets the PCBattleEntities for this row.
        /// </summary>
        /// <returns>The row.</returns>
        /// <param name="rowPos">Row position.</param>
        public IEnumerable<PCBattleEntity> GetRow(PCCharacter.RowPosition rowPosition)
        {           
            return from x in entityMap
                   where !x.IsEnemy && x.RowPosition == rowPosition
                   select x.Entity as PCBattleEntity;
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

            foreach (PCCharacter c in pcChars)
            {
                ManagedEntity entity = new ManagedEntity();
                entity.Entity = new PCBattleEntity(c);
                entity.RowPosition = c.rowPosition;
                entityMap.Add(entity);
            }
            
            foreach(EnemyCharacter c in enemyChars)
            {
                ManagedEntity entity = new ManagedEntity();
                entity.Entity = new EnemyBattleEntity(c);
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