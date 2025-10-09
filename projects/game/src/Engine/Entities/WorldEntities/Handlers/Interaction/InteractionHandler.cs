using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;

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
            else if (interractiveEnt is NPCEntity npcEnt)
            {
                HandleNPCInteraction(npcEnt, livingEnt);
            }
        }

        public static void HandleInteractiveItemInteraction(InteractiveItemEntity itemEnt, StatsEntity livingEnt)
        {
            if (itemEnt.InteractionManager.InteractionData.Trigger == InteractionTriggers.INTERACTION_BUTTON_PRESSED)
            {

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.INTERACTRESSED])
                {
                    if (livingEnt.StatsManager.ItemPickupHandler.AllowPickup)
                    {
                        if (itemEnt.InteractionManager.InteractionData.Action == InteractionActions.ADD_ITEM_TO_INVENTORY)
                        {
                            Entities.Player.Inventory.AddInventory(itemEnt.Containment);
                            Physics.Physics.flatWorld.RemoveBody(itemEnt.Model.Body);
                            Entities.EntityMapManager.maps[Entities.EntityMapManager.CurrentMapId].Entities.Remove(itemEnt);
                            UI.UI.UIManager.RefreshComponentsByType(UI.UIComponent.UIComponentTypes.INVENTORY);
                            livingEnt.StatsManager.ItemPickupHandler.AllowPickup = false;
                        }


                    }
                }
            }
            else if (itemEnt.InteractionManager.InteractionData.Trigger == InteractionTriggers.AUTO)
            {
                if (itemEnt.InteractionManager.InteractionData.Action == InteractionActions.ADD_ITEM_TO_INVENTORY)
                {
                    Entities.Player.Inventory.AddInventory(itemEnt.Containment);
                    Physics.Physics.flatWorld.RemoveBody(itemEnt.Model.Body);
                    Entities.EntityMapManager.maps[Entities.EntityMapManager.CurrentMapId].Entities.Remove(itemEnt);
                    UI.UI.UIManager.RefreshComponentsByType(UI.UIComponent.UIComponentTypes.INVENTORY);
                }
            }
        }

        public static void HandleNPCInteraction(NPCEntity npcEnt, StatsEntity livingEnt)
        {

            if (npcEnt.InteractionManager.InteractionData.Trigger == InteractionTriggers.INTERACTION_BUTTON_PRESSED)
            {
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.INTERACTRESSED])
                {
                    if (npcEnt.InteractionManager.InteractionData.Action == InteractionActions.START_TRADE)
                    {
                        UI.UI.UIOuterNavigator.ToggleTradeComponent(npcEnt, livingEnt);
                    }
                    else if (npcEnt.InteractionManager.InteractionData.Action == InteractionActions.START_DIALOGUE)
                    {
                        npcEnt.InteractionManager.InteractionData.DialogueSequenceData.StartCurrentDialogue();
                    }
                }
            }
            else if (npcEnt.InteractionManager.InteractionData.Trigger == InteractionTriggers.AUTO)
            {

            }
        }
    }
}
