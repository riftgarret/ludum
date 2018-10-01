using Redninja.BattleSystem.Entity;
using System.Collections.Generic;

namespace Redninja.BattleSystem
{
    public interface IBattleView
    {
        /// <summary>
        /// Create and popualate PC Character GUI. 
        /// </summary>
        /// <param name="entities"></param>
        void CreatePcCharacterGui(IEnumerable<PCBattleEntity> entities);

        /// <summary>
        /// Create and popualte Enemy Chracter GUI.
        /// </summary>
        /// <param name="entities"></param>
        void CreateEnemyCharacterGui(IEnumerable<EnemyBattleEntity> entities);

        /// <summary>
        /// Update entity Hp's
        /// </summary>
        /// <param name="battleEntity"></param>
        /// <param name="hpCurrent"></param>
        /// <param name="hpMax"></param>
        void SetEntityHps(BattleEntity battleEntity, float hpCurrent, float hpMax);

        /// <summary>
        /// Update action percent.
        /// </summary>
        /// <param name="battleEntity"></param>
        /// <param name="actionPercent"></param>
        void SetEntityActionPercent(BattleEntity battleEntity, float actionPercent);
    }
}
