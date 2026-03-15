using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class IsGroundedRequirement : Requirement
    {


        public IsGroundedRequirement(bool negation = false)
        {
            IsNegation = negation;
        }

        public override bool Check()
        {
            bool result = Entities.Player.StatsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsGrounded;
            return IsNegation ? !result : result;
        }
    }
}
