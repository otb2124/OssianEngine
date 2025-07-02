using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class HitboxHandler
    {


        public static void HandleHit(LivingEntity fromEnt, LivingEntity toEnt)
        {
            //Debug.WriteLine("hit!");
            fromEnt.statsManager.DealDamageTo(toEnt);
        }


        public static void HandleInterraction(InteractiveEntity interractiveEnt, LivingEntity livingEnt)
        {
            //Debug.WriteLine("interraction!");
            if(interractiveEnt is InteractiveItemEntity itemInterractiveEnt)
            {
                HandleInterractiveItemInterraction(itemInterractiveEnt, livingEnt);
            }
        }

        public static void HandleInterractiveItemInterraction(InteractiveItemEntity itemEnt, LivingEntity livingEnt)
        {
            if (itemEnt.interactiveItemType == InteractiveItemEntity.InteractiveItemType.PICKUP)
            {
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.INTERRACTIONPRESSED])
                {
                    Entities.player.statsManager.inventory.AddInventory(itemEnt.Containment);
                    Physics.Physics.flatWorld.RemoveBody(itemEnt.model.body);
                    Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Remove(itemEnt);
                    
                }
            }
            else if (itemEnt.interactiveItemType == InteractiveItemEntity.InteractiveItemType.PICKUP_AUTO)
            {
                Entities.player.statsManager.inventory.AddInventory(itemEnt.Containment);
                Physics.Physics.flatWorld.RemoveBody(itemEnt.model.body);
                Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Remove(itemEnt);
            }
        }
    }
}
