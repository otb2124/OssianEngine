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

        public override bool Check(StatsEntity Entity)
        {
            bool result = false;

            if (Entity.StatsManager.GetStatAbility<DoubleJumpAbility>() != null)
            {
                result = Entity.StatsManager.GetStatAbility<DoubleJumpAbility>().AllowDoubleJump;
            }

            return IsNegation ? !result : result;
        }
    }
}