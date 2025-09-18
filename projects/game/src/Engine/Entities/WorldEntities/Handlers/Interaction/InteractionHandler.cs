using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class InteractionHandler
    {


        public static void HandleInteraction(WorldEntity interractiveEnt, StatsEntity livingEnt)
        {
            if (interractiveEnt is InteractiveItemEntity itemInterractiveEnt)
            {
                HandleInteractiveItemInteraction(itemInterractiveEnt, livingEnt);
            }
            else if(interractiveEnt is NPCEntity npcEnt)
            {
                HandleNPCInteraction(npcEnt, livingEnt);
            }
        }

        public static void HandleInteractiveItemInteraction(InteractiveItemEntity itemEnt, StatsEntity livingEnt)
        {
            if (itemEnt.interactiveItemTrigger == InteractionTriggers.INTERACTION_BUTTON_PRESSED)
            {
                if(livingEnt.Stats.AllowPickup)
                {
                    if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.INTERACTRESSED])
                    {
                        Entities.Player.Inventory.AddInventory(itemEnt.Containment);
                        Physics.Physics.flatWorld.RemoveBody(itemEnt.Model.Body);
                        Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Remove(itemEnt);
                        UI.UI.UIManager.RefreshComponentsByType(UI.UIComponent.UIComponentTypes.MENU_INGAME_INVENTORY);
                        livingEnt.Stats.AllowPickup = false;
                    }
                }
            }
            else if (itemEnt.interactiveItemTrigger == InteractionTriggers.AUTO)
            {
                Entities.Player.Inventory.AddInventory(itemEnt.Containment);
                Physics.Physics.flatWorld.RemoveBody(itemEnt.Model.Body);
                Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Remove(itemEnt);
                UI.UI.UIManager.RefreshComponentsByType(UI.UIComponent.UIComponentTypes.MENU_INGAME_INVENTORY);
            }
        }

        public static void HandleNPCInteraction(NPCEntity npcEnt, StatsEntity livingEnt)
        {
            
            if(npcEnt.NPCInteractionManager.InteractionTrigger == InteractionTriggers.INTERACTION_BUTTON_PRESSED)
            {
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.INTERACTRESSED])
                {
                    Console.WriteLine("npc interaction");
                }
            }
            else if(npcEnt.NPCInteractionManager.InteractionTrigger == InteractionTriggers.AUTO)
            {
                Console.WriteLine("npc interaction");
            }
        }
    }
}
