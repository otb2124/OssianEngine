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

            //default state
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.TOGGLEMENUPRESSED])
            {
                ToggleInGameMenu();
            }
        }





        public void ToggleInGameMenu()
        {
            if (UI.UIManager.GetComponent(UIComponent.ComponentTypes.FRAME) != null)
            {
                UI.UIManager.RemoveLatestComponent(UIComponent.ComponentTypes.FRAME);
            }
            else
            {
                UI.UIManager.components.Add(new UIFrameComponent(new Vector2(0, 0), new Vector2(Graphics.Graphics.screen.Width / 2, Graphics.Graphics.screen.Height / 2)));
            }
            
        }
    }
}
