using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class IsOnStaminaRegenRequirement : Requirement
    {


        public IsOnStaminaRegenRequirement(bool negation = false)
        {
            IsNegation = negation;
        }

        public override bool Check(StatsEntity Entity)
        {
            bool result = Entity.StatsManager.GetStatAbility<StaminaRegenerationAbility>().OnStaminaRegen;
            return IsNegation ? !result : result;
        }
    }
}
