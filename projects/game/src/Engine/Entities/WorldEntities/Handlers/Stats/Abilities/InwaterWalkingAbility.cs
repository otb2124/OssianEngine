using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InwaterWalkingAbility : EntityAbility
    {

        public float MovementMultiplier;

        public bool MultiplierApplied = false;
        public bool StatRestored = true;

        public InwaterWalkingAbility(float movementMultiplier)
        {
            Type = EntityStatFeatures.INWATER_WALKING;
            MovementMultiplier = movementMultiplier;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            if (model.ModelState == ModelStates.MOVING || model.ModelState == ModelStates.INWATER_MOVING || model.ModelState == ModelStates.WEAPON_OUT_MOVING)
            {
                WaterTileEntity waterTile = CollisionHelper.GetAnyWaterTiles(model.Body);
                if (waterTile != null)
                {
                    if(!MultiplierApplied)
                    {
                        statsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue *= MovementMultiplier;
                        MultiplierApplied = true;
                        StatRestored = false;
                    }

                    model.ModelState = ModelStates.INWATER_MOVING;
                }
                else
                {
                    MultiplierApplied = false;

                    if(!StatRestored)
                    {
                        statsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue /= MovementMultiplier;
                        StatRestored = true;
                    }
                }
            }
            
        }
    }
}
