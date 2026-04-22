using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CurrentEnoughBattleStaminaRequirement : Requirement
    {

        public CurrentEnoughBattleStaminaRequirement()
        {
        }

        public override bool Check(StatsEntity Entity)
        {
            

            if (Entity != null && Entity is BattleEntity ent)
            {
                return Entity.StatsManager.CheckEnoughFinalBattleStamina(ent);
            }

            return false;
        }
    }
}
