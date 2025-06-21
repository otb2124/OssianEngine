using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UINavigator
    {

        public void HandleInitialNavigation()
        {
            UI.UIManager.components.Add(new UICursorComponent());
        }

        public void HandleNavigation()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.TOGGLEMENUPRESSED])
            {
                UI.UIManager.ToggleComponent(new UIInGameMenu(0), UIComponent.UIComponentTypes.MENU_INGAME);
            }
        }
    }
}