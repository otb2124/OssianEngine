using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FallAbility : EntityAbility
    {

        public float FallenTimer = 0f;
        public float FallenDurationAllowedSec = 3f;

        public bool IsFallen;
        public bool IsFalling;


        public FallAbility() 
        {
            Type = EntityStatFeatures.FALL;
        }



        public override void Update(StatsManager statsManager, Resources.Model model)
        {

            EntityStat poise = statsManager.GetStat(EntityStats.POISE);

            if (model.Body.Angle > 0.5f || model.Body.Angle < -0.5f ||  poise.CurrentValue <= 0)
            {
                if (!model.Body.IsColliding)
                {
                    if (!IsFallen)
                    {
                        IsFalling = true;
                        model.ModelState = ModelStates.FALLING;
                    }
                }
                else
                {
                    IsFallen = true;
                    IsFalling = false;
                    model.ModelState = ModelStates.FALLEN;
                }
            }

            if (IsFallen)
            {
                FallenTimer++;
                if (FallenTimer >= FallenDurationAllowedSec * Graphics.Graphics.GraphicsFrameRate)
                {
                    IsFallen = false;
                    FallenTimer = 0f;
                    model.Body.Move(new PhysicalVector(0, 10f));
                    model.Body.RotateTo(0f);

                    //regen Poise
                    poise.Refill();
                }
            }
        }
    }
}
