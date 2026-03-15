using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class IsTouchingCeilingRequirement : Requirement
    {


        public IsTouchingCeilingRequirement(bool negation = false)
        {
            IsNegation = negation;
        }

        public override bool Check()
        {
            bool result = Entities.Player.StatsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsTouchingCeiling;
            return IsNegation ? !result : result;
        }
    }
}
