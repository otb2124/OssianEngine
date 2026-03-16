using System;
using Utils;

namespace Entities
{
    public class AllowDoubleJumpRequirement : Requirement
    {
        public AllowDoubleJumpRequirement(bool negation = false)
        {
            IsNegation = negation;
        }

        public override bool Check()
        {
            bool result = Entities.Player.StatsManager.GetStatAbility<DoubleJumpAbility>().AllowDoubleJump;
            return IsNegation ? !result : result;
        }
    }
}