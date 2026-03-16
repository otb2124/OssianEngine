using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class IsDyingRequirement : Requirement
    {


        public IsDyingRequirement(bool negation = false)
        {
            IsNegation = negation;
        }

        public override bool Check(StatsEntity Entity)
        {
            bool result = false;

            if(Entity.StatsManager.GetStatAbility<DieAbility>() != null)
            {
                result = Entity.StatsManager.GetStatAbility<DieAbility>().IsDying;
            }

            return IsNegation ? !result : result;
        }
    }
}
