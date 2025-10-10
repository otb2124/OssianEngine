using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class DescencionHandler : EntityStatFeature
    {

        public float DescendingMultiplier;
        public float MaxDescendingSec;
        public int DescendingCounter = 0;

        public bool IsJumpDescending;

        public DescencionHandler(float maxDescendingSec, float descendingMultiplier)
        {
            MaxDescendingSec = maxDescendingSec;
            DescendingMultiplier = descendingMultiplier;
            Type = EntityStatFeatures.DESCENCION;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {

            statsManager.DescendingMultiplier = DescendingMultiplier;

            if (statsManager.IsTouchingWalls)
            {
                statsManager.IsTouchingCeiling = false;
                statsManager.IsGrounded = false;
            }

            if (model.ModelState == ModelStates.JUMPING ||
                 model.ModelState == ModelStates.JUMPING_AND_MOVING ||
                 model.ModelState == ModelStates.JUMPING_DESCENDING ||
                 model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING)
            {
                IsJumpDescending = CollisionHelper.IsDescending(model);
            }


            if (statsManager.AllowJumpDescendingLock && IsJumpDescending)
            {
                DescendingCounter++;
                statsManager.AllowJumpDescending = true;
                if (DescendingCounter > MaxDescendingSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    IsJumpDescending = false;
                    statsManager.AllowJumpDescendingLock = false;
                    statsManager.AllowJumpDescending = false;
                    DescendingCounter = 0;
                }
            }
            else
            {
                DescendingCounter = 0;
            }


            if (statsManager.IsGrounded || statsManager.IsTouchingCeiling)
            {
                IsJumpDescending = false;
                statsManager.AllowJumpDescendingLock = false;
                statsManager.AllowJumpDescending = false;
                DescendingCounter = 0;
            }

            if (statsManager.IsGrounded)
            {
                model.highestJumpY = float.MinValue;
            }
        }
    }
}
