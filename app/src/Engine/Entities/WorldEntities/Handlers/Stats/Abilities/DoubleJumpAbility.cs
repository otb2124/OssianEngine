using Resources;
using System;

namespace Entities
{
    public class DoubleJumpAbility : EntityAbility
    {
        public bool AllowDoubleJump = false;

        public DoubleJumpAbility()
        {
            Type = EntityStatFeatures.DOUBLE_JUMP;
        }

        public override void Update(StatsManager statsManager, Model model)
        {
            // Allow double jump only while descending from a normal jump
            if (model.ModelState == ModelStates.JUMPING_DESCENDING ||
                model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING)
            {
                AllowDoubleJump = true;
            }

            // Once the double jump fires, consume the permission
            if (model.ModelState == ModelStates.DOUBLE_JUMPING ||
                model.ModelState == ModelStates.DOUBLE_JUMPING_AND_MOVING)
            {
                AllowDoubleJump = false;
            }

            // Reset when grounded
            if (statsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsGrounded)
            {
                AllowDoubleJump = false;
            }
        }
    }
}