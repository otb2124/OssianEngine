using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatsBattleHitSpendTool
    {

        public bool StatsPerAttackHitSpent = false;

        public void SpendStatsForBattleHit(EntityStat stamina, EntityStat mana, BattleEntity ent)
        {
            stamina.CurrentValue -= BattleStatsCalculator.GetFinalStaminaPerHitCostForBattleEntity(ent);
            mana.CurrentValue -= BattleStatsCalculator.GetFinalManaPerHitCostForBattleEntity(ent);
            StatsPerAttackHitSpent = true;
        }
    }
}
