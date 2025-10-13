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

        public override bool Check()
        {
            return Entities.Player.StatsManager.CheckEnoughFinalBattleStamina(Entities.Player);
        }
    }
}
