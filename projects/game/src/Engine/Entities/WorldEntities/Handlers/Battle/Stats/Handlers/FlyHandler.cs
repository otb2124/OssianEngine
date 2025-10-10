using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class FlyHandler : EntityStatFeature
    {

        public int FlyingCounter = 0;
        public float MaxFlyTimeSec = 0.5f;
        public float FlyHeightPointOverHead = 50f;
        public float CurrentFlyHeightPointOverHead = 50f;
        public float LandPoint;


        public FlyHandler()
        {
            Type = EntityStatFeatures.FLY;
        }


        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            if (statsManager.IsGrounded)
            {
                LandPoint = model.Body.Position.Y;
                CurrentFlyHeightPointOverHead = FlyHeightPointOverHead;
            }

            if (model.ModelState != ModelStates.FLYING && model.ModelState != ModelStates.FLYING_AND_MOVING)
            {
                FlyingCounter = 0;
                return;
            }


            if (model.Body.Position.Y < LandPoint + CurrentFlyHeightPointOverHead && statsManager.FlyingUpwards)
            {
                statsManager.FlyingUpwards = true;
                return;
            }

            if (statsManager.FlyingUpwards)
            {
                FlyingCounter++;

                if (FlyingCounter >= MaxFlyTimeSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    statsManager.FlyingUpwards = false;
                    FlyingCounter = (int)(MaxFlyTimeSec * Graphics.Graphics.UpdatesPerSecond);
                }
            }
            else
            {
                FlyingCounter--;
                if (FlyingCounter <= 0)
                {
                    statsManager.FlyingUpwards = true;
                    FlyingCounter = 0;
                }
            }


        }
    }
}
