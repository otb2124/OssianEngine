using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class AllowJumpDescendingRequirement : Requirement
    {

        public AllowJumpDescendingRequirement(bool negation = false)
        {
            IsNegation = negation;
        }

        public override bool Check()
        {
            bool result = Entities.Player.StatsManager.GetStatAbility<DescencionAbility>().AllowJumpDescending;
            return IsNegation ? !result : result;
        }
    }
}
