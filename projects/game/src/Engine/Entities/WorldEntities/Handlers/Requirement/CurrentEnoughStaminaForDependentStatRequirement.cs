using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CurrentEnoughStaminaForDependentStatRequirement : Requirement
    {

        public EntityStats Stat;

        public CurrentEnoughStaminaForDependentStatRequirement(EntityStats stats)
        {
            Stat = stats;
        }

        public override bool Check()
        {
            return Entities.Player.StatsManager.CheckEnoughStaminaForStat(Stat);
        }
    }
}
