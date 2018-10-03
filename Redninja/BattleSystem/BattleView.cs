using Redninja.BattleSystem.Entities;
using Redninja.BattleSystem.GUI;
using System.Collections.Generic;

namespace Redninja.BattleSystem
{
    public class BattleView : MonoBehaviour, IBattleView
    {
        [SerializeField]
        private CharacterLayoutComponent enemyCharacterLayout;

        [SerializeField]
        private CharacterLayoutComponent pcCharacterLayout;

        public void CreateEnemyCharacterGui(IEnumerable<EnemyBattleEntity> entities)
        {
            enemyCharacterLayout.SetEntities(entities);
        }

        public void CreatePcCharacterGui(IEnumerable<PCBattleEntity> entities)
        {
            pcCharacterLayout.SetEntities(entities);
        }

        public void SetEntityActionPercent(BattleEntity battleEntity, float actionPercent)
        {            
            GetLayoutForEntity(battleEntity).SetEntityActionPercent(battleEntity, actionPercent);
        }

        public void SetEntityHps(BattleEntity battleEntity, float hpCurrent, float hpMax)
        {
            GetLayoutForEntity(battleEntity).SetEntityHp(battleEntity, hpCurrent, hpMax);
        }

        private CharacterLayoutComponent GetLayoutForEntity(BattleEntity battleEntity)
        {
            return battleEntity.IsPC ? pcCharacterLayout : enemyCharacterLayout;
        }        
    }
}
