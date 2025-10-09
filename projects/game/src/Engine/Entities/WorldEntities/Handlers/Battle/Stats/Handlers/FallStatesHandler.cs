using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FallStatesHandler
    {

        public bool IsFalling = false;

        public bool IsFallen = false;
        public float FallenTimer = 0f;
        public float FallenDurationAllowedSec = 3f;


        public FallStatesHandler() { }



        public void UpdateFallen(Resources.Model model, PoiseStats poiseStats)
        {

            if (model.Body.Angle > 0.5f || model.Body.Angle < -0.5f || poiseStats.Poise <= 0)
            {
                if (!model.Body.IsColliding)
                {
                    if (!IsFallen)
                    {
                        IsFalling = true;
                        model.ModelState = Utils.ModelStates.FALLING;
                    }
                }
                else
                {
                    IsFallen = true;
                    IsFalling = false;
                    model.ModelState = Utils.ModelStates.FALLEN;
                }
            }

            if (IsFallen)
            {
                FallenTimer++;
                if (FallenTimer >= FallenDurationAllowedSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    IsFallen = false;
                    FallenTimer = 0f;
                    model.Body.Move(new FlatVector(0, 10f));
                    model.Body.RotateTo(0f);

                    //regen Poise
                    poiseStats.Refill();
                }
            }
        }
    }
}
