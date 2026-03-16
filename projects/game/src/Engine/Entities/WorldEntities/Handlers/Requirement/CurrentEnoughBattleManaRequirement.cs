using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CurrentEnoughBattleManaRequirement : Requirement
    {
        public CurrentEnoughBattleManaRequirement()
        {
        }

        public override bool Check(StatsEntity Entity)
        {
            if (Entity != null && Entity is BattleEntity ent)
            {
                return Entity.StatsManager.CheckEnoughFinalBattleMana(ent);
            }

            return false;
        }
    }
}
