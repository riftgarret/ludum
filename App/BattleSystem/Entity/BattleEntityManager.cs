using App.BattleSystem.Action;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.BattleSystem.Entity
{
    /// <summary>
    /// This component needs to be rethought out.
    /// </summary>
    public class BattleEntityManager
    {
        public BattleEntity.OnDecisionRequired OnDecisionRequiredDelegate;
        public BattleEntity.OnExecutionStarted OnExecutionStartedDelegate;

        private EnemyBattleEntity[] mEnemyEntities;
        public EnemyBattleEntity[] enemyEntities
        {
            get { LazyInit(); return mEnemyEntities; }
        }

        private PCBattleEntity[] mPcEntities;
        public PCBattleEntity[] pcEntities
        {
            get { LazyInit(); return mPcEntities; }
        }

        private BattleEntity[] mAllEntities;
        public BattleEntity[] allEntities
        {
            get { LazyInit(); return mAllEntities; }
        }

        private PCBattleEntity[] mFrontRowEntities;
        public PCBattleEntity[] frontRowEntities
        {
            get { LazyInit(); return mFrontRowEntities; }
        }

        private PCBattleEntity[] mMiddleRowEntities;
        public PCBattleEntity[] middleRowEntities
        {
            get { LazyInit(); return mMiddleRowEntities; }
        }

        private PCBattleEntity[] mBackRowEntities;
        public PCBattleEntity[] backRowEntities
        {
            get { LazyInit(); return mBackRowEntities; }
        }


        /// <summary>
        /// Create a new instance with these party and enemy components.
        /// </summary>
        /// <param name="partyComponent"></param>
        /// <param name="enemyComponent"></param>
        public BattleEntityManager(PartyComponent partyComponent, EnemyComponent enemyComponent)
        {
            LoadCharacters(partyComponent.Characters.ToArray(), enemyComponent.Characters.ToArray());
        }

        /// <summary>
        /// Raises the row update event. Should be called upon listening to row changes.
        /// </summary>
        /// <param name="character">Character.</param>
        public void OnRowUpdate(PCCharacter character)
        {
            // re-evaluate all rows
            BuildRowEntities();
        }

        /// <summary>
        /// Gets the PCBattleEntities for this row.
        /// </summary>
        /// <returns>The row.</returns>
        /// <param name="rowPos">Row position.</param>
        public PCBattleEntity[] GetRow(PCCharacter.RowPosition rowPos)
        {
            LazyInit();
            switch (rowPos)
            {
                case PCCharacter.RowPosition.FRONT:
                    return mFrontRowEntities;
                case PCCharacter.RowPosition.MIDDLE:
                    return mMiddleRowEntities;
                case PCCharacter.RowPosition.BACK:
                    return mBackRowEntities;
            }
            return null;
        }

        private void LazyInit()
        {
            if (mPcEntities != null)
            {
                return;
            }


        }


        private void LoadCharacters(Character[] pcChars, Character[] enemyChars)
        {
            // combine 
            mAllEntities = new BattleEntity[pcChars.Length + enemyChars.Length];
            mPcEntities = new PCBattleEntity[pcChars.Length];
            mEnemyEntities = new EnemyBattleEntity[enemyChars.Length];

            for (int i = 0; i < mPcEntities.Length; i++)
            {
                if (pcChars[i] is PCCharacter)
                {
                    mPcEntities[i] = new PCBattleEntity((PCCharacter)pcChars[i]);
                    mAllEntities[i] = mPcEntities[i];
                    HookEntityDelegates(mPcEntities[i]);
                }
            }

            for (int i = 0; i < mEnemyEntities.Length; i++)
            {
                if (enemyChars[i] is EnemyCharacter)
                {
                    mEnemyEntities[i] = new EnemyBattleEntity((EnemyCharacter)enemyChars[i]);
                    mAllEntities[pcChars.Length + i] = mEnemyEntities[i];
                    HookEntityDelegates(mEnemyEntities[i]);
                }
            }
            // create row specifics
            BuildRowEntities();
        }

        private void HookEntityDelegates(BattleEntity entity)
        {
            entity.OnDecisionRequiredDelegate += delegate (BattleEntity e) { OnDecisionRequiredDelegate?.Invoke(e); };
            entity.OnExecutionStartedDelegate += delegate (BattleEntity e, IBattleAction action) { OnExecutionStartedDelegate?.Invoke(e, action); };
        }

        /// <summary>
        /// Builds the row entities. This is to optimize our logic for testing if row is still valid
        /// </summary>
        private void BuildRowEntities()
        {
            List<PCBattleEntity> frontRow = new List<PCBattleEntity>();
            List<PCBattleEntity> midRow = new List<PCBattleEntity>();
            List<PCBattleEntity> backRow = new List<PCBattleEntity>();

            foreach (PCBattleEntity entity in mPcEntities)
            {
                switch (entity.pcCharacter.rowPosition)
                {
                    case PCCharacter.RowPosition.FRONT:
                        frontRow.Add(entity);
                        break;
                    case PCCharacter.RowPosition.MIDDLE:
                        midRow.Add(entity);
                        break;
                    case PCCharacter.RowPosition.BACK:
                        backRow.Add(entity);
                        break;

                }
            }
            mFrontRowEntities = frontRow.ToArray();
            mMiddleRowEntities = midRow.ToArray();
            mBackRowEntities = backRow.ToArray();
        }
    }     
}