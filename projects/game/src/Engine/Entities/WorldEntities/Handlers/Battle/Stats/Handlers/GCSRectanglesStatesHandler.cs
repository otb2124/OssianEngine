using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class GCSRectanglesStatesHandler
    {

        public bool IsGrounded;
        public bool IsTouchingCeiling;
        public bool IsTouchingWalls;

        public void Update(Resources.Model model)
        {
            IsGrounded = CollisionHelper.GetAnyGround(model) != null;
            IsTouchingCeiling = CollisionHelper.GetAnyCeiling(model) != null;
            IsTouchingWalls = CollisionHelper.GetAnyWalls(model) != null;
        }

        public void Reset(Resources.Model model)
        {
            // Reset highestJumpY when grounded
            if (IsGrounded)
            {
                model.highestJumpY = float.MinValue;
            }
        }
    }
}
