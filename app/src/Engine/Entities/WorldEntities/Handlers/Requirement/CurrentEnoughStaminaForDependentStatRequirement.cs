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

        public override bool Check(StatsEntity Entity)
        {
            bool result = Entity.StatsManager.CheckEnoughStaminaForStat(Stat);
            return IsNegation ? !result : result;
        }
    }
}
