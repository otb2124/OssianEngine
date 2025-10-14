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

        public override bool Check()
        {
            bool result = Entities.Player.StatsManager.OnStaminaRegen;
            return IsNegation ? !result : result;
        }
    }
}
