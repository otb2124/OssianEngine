using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace UI
{
    public class UIInnerNavigator
    {

        public void HandleInitialNavigation()
        {
            UI.UIManager.ToggleComponent(new UIHUDComponent(0), UIComponent.UIComponentTypes.HUD);
        }

        public void HandleNavigation()
        {
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.TOGGLEMENUPRESSED])
            {
                ToggleInGameMenu();

                if(UI.UIManager.GetComponent(UIComponent.UIComponentTypes.HUD) == null)
                {
                    UI.UIManager.ToggleComponent(new UIHUDComponent(0), UIComponent.UIComponentTypes.HUD);
                }
            }

            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.TOGGLEHUDPRESSED])
            {
                UI.UIManager.ToggleComponent(new UIHUDComponent(0), UIComponent.UIComponentTypes.HUD);
            }
        }

        public void HandleDynamicButtonNavigation(int id)
        {
            switch(id)
            {
                case 0:
                    ToggleInGameMenu();
                    break;
                case 1:
                    ClearInGameMenu(UIComponent.UIComponentTypes.MENU_INGAME_INVENTORY);
                    UI.UIManager.ToggleComponent(new UIInGameMenuInventoryComponent(2), UIComponent.UIComponentTypes.MENU_INGAME_INVENTORY);
                    break;
                case 2:
                    ClearInGameMenu(UIComponent.UIComponentTypes.MENU_INGAME_SKILLS);
                    UI.UIManager.ToggleComponent(new UIInGameMenuSkillsComponent(3), UIComponent.UIComponentTypes.MENU_INGAME_SKILLS);
                    break;
                case 3:
                    ClearInGameMenu(UIComponent.UIComponentTypes.MENU_INGAME_QUESTBOOK);
                    UI.UIManager.ToggleComponent(new UIInGameMenuQuestBookComponent(4), UIComponent.UIComponentTypes.MENU_INGAME_QUESTBOOK);
                    break;
                case 4:
                    ClearInGameMenu(UIComponent.UIComponentTypes.MENU_INGAME_STATISTICS);
                    UI.UIManager.ToggleComponent(new UIInGameMenuStatisticsComponent(5), UIComponent.UIComponentTypes.MENU_INGAME_STATISTICS);
                    break;
                case 5:
                    UI.UIManager.ToggleComponent(new UIWarningWindowComponent(6), UIComponent.UIComponentTypes.WARNING_WINDOW);
                    break;
            }
        }

        public void ToggleInGameMenu()
        {
            if(UI.UIManager.GetComponent(UIComponent.UIComponentTypes.INVENTORY_TO_INVENTORY) == null)
            {
                UI.UIManager.ToggleComponent(new UIIngameMenuComponent(1), UIComponent.UIComponentTypes.MENU_INGAME);
                ClearInGameMenu();


                if (UI.UIManager.GetComponent(UIComponent.UIComponentTypes.MENU_INGAME) == null)
                {
                    UI.PreventButtonPressedOverlap = false;
                }
                else
                {
                    UI.PreventButtonPressedOverlap = true;
                }
            }
        }

        private void ClearInGameMenu(UIComponent.UIComponentTypes? exception = null)
        {
            var menuComponents = new[]
            {
                (Type: UIComponent.UIComponentTypes.MENU_INGAME_INVENTORY, Id: 2),
                (Type: UIComponent.UIComponentTypes.MENU_INGAME_SKILLS, Id: 3),
                (Type: UIComponent.UIComponentTypes.MENU_INGAME_QUESTBOOK, Id: 4),
                (Type: UIComponent.UIComponentTypes.MENU_INGAME_STATISTICS, Id: 5)
            };

            foreach (var (type, id) in menuComponents)
            {
                if (exception == null || type != exception)
                {
                    UI.UIManager.RemoveComponent(type, id);
                }
            }
        }
    }
}