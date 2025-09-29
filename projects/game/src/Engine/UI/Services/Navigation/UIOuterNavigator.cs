using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIOuterNavigator
    {


        public void HandleNavigation()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.TOGGLEMENUPRESSED])
            {
                UI.UIManager.RemoveComponent(UIComponent.UIComponentTypes.INVENTORY_TO_INVENTORY, 999);
                UI.UIManager.RemoveComponent(UIComponent.UIComponentTypes.DIALOGUE, 999);
            }
        }


        public void ToggleTradeComponent(StatsEntity entFrom, StatsEntity entTo)
        {
            if(UI.UIManager.GetComponent(UIComponent.UIComponentTypes.MENU_INGAME) == null)
            {
                UI.UIManager.ToggleComponent(new UIInventoryInventoryBoardsComponent(999, Vector2.Zero, entFrom.Inventory, entTo.Inventory), UIComponent.UIComponentTypes.INVENTORY_TO_INVENTORY);
            }

            if (UI.UIManager.GetComponent(UIComponent.UIComponentTypes.INVENTORY_TO_INVENTORY) == null)
            {
                UI.PreventButtonPressedOverlap = false;
            }
            else
            {
                UI.PreventButtonPressedOverlap = true;
            }
        }

        public void ToggleDialogueComponent(Dialogue dialogueFrame)
        {
            if (UI.UIManager.GetComponent(UIComponent.UIComponentTypes.MENU_INGAME) == null)
            {
                UI.UIManager.ToggleComponent(new UIDialogueComponent(999, dialogueFrame), UIComponent.UIComponentTypes.DIALOGUE);
            }

            if (UI.UIManager.GetComponent(UIComponent.UIComponentTypes.DIALOGUE) == null)
            {
                UI.PreventButtonPressedOverlap = false;
            }
            else
            {
                UI.PreventButtonPressedOverlap = true;
            }
        }

        public void ShowDialogueComponent(Dialogue dialogueFrame)
        {
            if (UI.UIManager.GetComponent(UIComponent.UIComponentTypes.MENU_INGAME) == null)
            {
                UI.UIManager.components.Add(new UIDialogueComponent(999, dialogueFrame));
            }

            if (UI.UIManager.GetComponent(UIComponent.UIComponentTypes.DIALOGUE) == null)
            {
                UI.PreventButtonPressedOverlap = false;
            }
            else
            {
                UI.PreventButtonPressedOverlap = true;
            }
        }

        public void RemoveDialogueComponent()
        {
            UI.UIManager.RemoveComponent(UIComponent.UIComponentTypes.DIALOGUE, 999);   
        }

        public void SetDialogueComponentData(Dialogue dialogueFrame)
        {
            UIDialogueComponent component = (UIDialogueComponent)UI.UIManager.GetComponent(UIComponent.UIComponentTypes.DIALOGUE);

            if (component != null)
            {
                component.SetDialogue(dialogueFrame);
            }
        }
    }
}
