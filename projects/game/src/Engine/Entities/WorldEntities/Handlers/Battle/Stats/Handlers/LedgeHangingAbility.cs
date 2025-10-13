using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class LedgeHangingAbility : EntityAbility
    {

        public bool AllowHangingOnLedge = true;
        public int HangingCounter = 0;
        public int UnHangingCounter = 0;


        public LedgeHangingAbility()
        {
            Type = EntityStatFeatures.LEDGE_HANG;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            if (!AllowHangingOnLedge && model.ModelState != ModelStates.HANGING_ON_LEDGE)
            {
                UnHangingCounter++;

                if (UnHangingCounter > 0.5f * Graphics.Graphics.UpdatesPerSecond)
                {
                    AllowHangingOnLedge = true;
                    UnHangingCounter = 0;
                }
            }

            if (statsManager.IsTouchingWalls)
            {
                LedgeEntity ledge = CollisionHelper.GetAnyLedges(model.Body);
                if (ledge != null && AllowHangingOnLedge)
                {
                    model.ModelState = ModelStates.HANGING_ON_LEDGE;
                    model.Body.MoveTo(FlatConverter.ToFlatVector(ledge.HangingPosition));

                    HangingCounter++;
                    if (HangingCounter > 0.25f * Graphics.Graphics.UpdatesPerSecond)
                    {
                        AllowHangingOnLedge = false;
                        HangingCounter = 0;
                    }

                    //TODO FIX THE LEDGES DIRECTION SWAP
                    if (ledge.Model.Direction == Directions.RIGHT)
                    {
                        model.AnimationState = AnimationStates.HANGING_ON_LEDGE_RIGHT;
                    }
                    else
                    {
                        model.AnimationState = AnimationStates.HANGING_ON_LEDGE_LEFT;
                    }
                }


                //autoclimb case
                if (ledge != null && !AllowHangingOnLedge)
                {
                    if (ledge.AutoClimbing)
                    {
                        model.Body.MoveTo(FlatConverter.ToFlatVector(ledge.AutoClimbingDestination));
                        model.ModelState = ModelStates.IDLE;
                    }
                }
            }
        }
    }
}
