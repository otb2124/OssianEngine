using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class DescencionHandler
    {

        public float MaxDescendingSec;
        public int DescendingCounter = 0;
        public float DescendingMultiplier;

        public bool IsJumpDescending;
        public bool AllowJumpDescending;
        public bool AllowJumpDescendingLock = true;

        public DescencionHandler(float maxDescendingSec, float descendingMultiplier)
        {
            MaxDescendingSec = maxDescendingSec;
            DescendingMultiplier = descendingMultiplier;
        }

        public void UpdateDescending(Resources.Model model, GCSRectanglesStatesHandler gcsHandler)
        {
            if (gcsHandler.IsTouchingWalls)
            {
                gcsHandler.IsTouchingCeiling = false;
                gcsHandler.IsGrounded = false;
            }

            if (model.ModelState == ModelStates.JUMPING ||
                 model.ModelState == ModelStates.JUMPING_AND_MOVING ||
                 model.ModelState == ModelStates.JUMPING_DESCENDING ||
                 model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING)
            {
                IsJumpDescending = CollisionHelper.IsDescending(model);
            }


            if (AllowJumpDescendingLock && IsJumpDescending)
            {
                DescendingCounter++;
                AllowJumpDescending = true;
                if (DescendingCounter > MaxDescendingSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    IsJumpDescending = false;
                    AllowJumpDescendingLock = false;
                    AllowJumpDescending = false;
                    DescendingCounter = 0;
                }
            }
            else
            {
                DescendingCounter = 0;
            }


            if (gcsHandler.IsGrounded || gcsHandler.IsTouchingCeiling)
            {
                IsJumpDescending = false;
                AllowJumpDescendingLock = false;
                AllowJumpDescending = false;
                DescendingCounter = 0;
            }

            gcsHandler.Reset(model);
        }
    }
}
