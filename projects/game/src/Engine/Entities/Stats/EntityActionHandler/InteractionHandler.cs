using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class InteractionHandler
    {


        public static void HandleInterraction(InteractiveEntity interractiveEnt, StatsEntity livingEnt)
        {
            //Debug.WriteLine("interraction!");
            if (interractiveEnt is InteractiveItemEntity itemInterractiveEnt)
            {
                HandleInterractiveItemInterraction(itemInterractiveEnt, livingEnt);
            }
        }

        public static void HandleInterractiveItemInterraction(InteractiveItemEntity itemEnt, StatsEntity livingEnt)
        {
            if (itemEnt.interactiveItemType == InteractiveItemEntity.InteractiveItemType.PICKUP)
            {
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.INTERRACTIONPRESSED])
                {
                    Entities.player.Inventory.AddInventory(itemEnt.Containment);
                    Physics.Physics.flatWorld.RemoveBody(itemEnt.Model.body);
                    Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Remove(itemEnt);

                }
            }
            else if (itemEnt.interactiveItemType == InteractiveItemEntity.InteractiveItemType.PICKUP_AUTO)
            {
                Entities.player.Inventory.AddInventory(itemEnt.Containment);
                Physics.Physics.flatWorld.RemoveBody(itemEnt.Model.body);
                Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Remove(itemEnt);
            }
        }
    }
}
