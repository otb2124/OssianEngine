using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{


    public class ItemPickupAbility : EntityAbility
    {

        public int PickupCounter = 0;
        public float PickupLockSec = 0.25f;

        public bool AllowPickup = true;

        public ItemPickupAbility()
        {
            Type = EntityStatFeatures.ITEM_PICKUP;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            if (!AllowPickup)
            {
                PickupCounter++;
                if (PickupCounter > PickupLockSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    AllowPickup = true;
                    PickupCounter = 0;
                }
            }
        }
    }
}
