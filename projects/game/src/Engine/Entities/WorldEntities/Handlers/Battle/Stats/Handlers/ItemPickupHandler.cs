using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{


    public class ItemPickupHandler
    {

        public bool AllowPickup = true;
        public int PickupCounter = 0;
        public float PickupLockSec = 0.25f;


        public void Update()
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
