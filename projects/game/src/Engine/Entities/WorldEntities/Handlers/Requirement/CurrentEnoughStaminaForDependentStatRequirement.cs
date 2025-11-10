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

        public CurrentEnoughStaminaForDependentStatRequirement(EntityStats stats, bool negation = false)
        {
            IsNegation = negation;
            Stat = stats;
        }

        public override bool Check()
        {
            bool result = Entities.Player.StatsManager.CheckEnoughStaminaForStat(Stat);
            return IsNegation ? !result : result;
        }
    }
}
