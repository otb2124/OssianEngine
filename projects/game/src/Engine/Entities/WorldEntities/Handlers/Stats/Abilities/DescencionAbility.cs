using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class DescencionAbility : EntityAbility
    {

        public float DescendingMultiplier;
        public float MaxDescendingSec;
        public int DescendingCounter = 0;

        public bool IsJumpDescending;

        public bool AllowJumpDescendingLock;
        public bool AllowJumpDescending;

        public DescencionAbility(float maxDescendingSec, float descendingMultiplier)
        {
            MaxDescendingSec = maxDescendingSec;
            DescendingMultiplier = descendingMultiplier;
            Type = EntityStatFeatures.DESCENCION;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            if (statsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsTouchingWalls)
            {
                statsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsTouchingCeiling = false;
                statsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsGrounded = false;
            }

            if (model.ModelState == ModelStates.JUMPING ||
                 model.ModelState == ModelStates.JUMPING_AND_MOVING ||
                 model.ModelState == ModelStates.JUMPING_DESCENDING ||
                 model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING)
            {
                IsJumpDescending = IsDescending(statsManager, model);
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


            if (statsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsGrounded || statsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsTouchingCeiling)
            {
                IsJumpDescending = false;
                AllowJumpDescendingLock = false;
                AllowJumpDescending = false;
                DescendingCounter = 0;
            }

            if (statsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsGrounded)
            {
                model.highestJumpY = float.MinValue;
            }
        }


        public bool IsDescending(StatsManager statsManager, Resources.Model model)
        {
            if (!statsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsGrounded)
            {
                PhysicalBody body = model.Body;

                if (body.Position.Y > model.HighestJumpY)
                {
                    model.HighestJumpY = body.Position.Y;
                    return false;
                }
                else if (body.Position.Y < model.HighestJumpY)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
