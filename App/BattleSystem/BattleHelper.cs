using App.BattleSystem.Entity;
using System.Collections.Generic;
using System.Linq;

namespace App.BattleSystem
{
    /// <summary>
    /// This class contains helper methods for calculating game logic.
    /// </summary>
    public class BattleHelper
    {
        private BattleHelper() { }


        public static bool CheckForVictory(BattleEntityManager entityManager)
            => CheckForAnnilatation(entityManager.EnemyEntities);

        public static bool CheckForDefeat(BattleEntityManager entityManager)
            => CheckForAnnilatation(entityManager.PCEntities);

        private static bool CheckForAnnilatation(IEnumerable<BattleEntity> entityGroup)
            => entityGroup.Where(x => x.Character.curHP > 0).Count() == 0;        

        

        /// <summary>
        /// Get action percent for this battle entity.
        /// </summary>
        /// <param name="battleEntity"></param>
        /// <returns></returns>
        public static float GetActionPercent(BattleEntity battleEntity)
        {
            switch (battleEntity.Phase)
            {
                // PREPARE -> animate 0 to 1 as completes
                case PhaseState.PREPARE:
                    return battleEntity.TurnPercent;
                // EXECUTING -> stays at 1
                case PhaseState.EXECUTE:
                    return 1f;
                // RECOVER -> animate 1 to 0 as completes
                case PhaseState.RECOVER:
                    return 1f - battleEntity.TurnPercent;
                // awaiting input, at 0 
                case PhaseState.REQUIRES_INPUT:
                default:
                    return 0f;
            }
        }
    }
}
