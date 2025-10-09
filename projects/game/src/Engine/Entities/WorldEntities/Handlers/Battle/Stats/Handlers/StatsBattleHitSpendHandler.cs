using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatsBattleHitSpendHandler
    {

        public bool StatsPerAttackHitSpent = false;

        public void SpendStatsForBattleHit(IndicatorStats iStats, BattleEntity ent)
        {
            iStats.Stamina -= BattleStatsCalculator.GetFinalStaminaPerHitCostForBattleEntity(ent);
            iStats.Mana -= BattleStatsCalculator.GetFinalManaPerHitCostForBattleEntity(ent);
            StatsPerAttackHitSpent = true;
        }
    }
}
