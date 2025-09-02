using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIButtonHandler
    {

        public UIButtonHandler() { }


        public bool CheckHover(RectangleF buttonRect)
        {
            PointF mousePos = new PointF(Inputs.Inputs.mouse.GetMouseWorldPosition().X, Inputs.Inputs.mouse.GetMouseWorldPosition().Y);

            if (buttonRect.Contains(mousePos))
            {
                return true;
            }

            return false;
        }

        public bool CheckClick(RectangleF buttonRect)
        {
            if(CheckHover(buttonRect))
            {
                if (Inputs.Inputs.mouse.IsAnyMouseButtonPressed())
                {
                    UI.UIManager.PreventButtonPressedOverlap = true;
                    return true;
                }
                else
                {
                    UI.UIManager.PreventButtonPressedOverlap = false;
                    return false;
                }
            }

            return false;
        }


        public void HandleHover(int id)
        {

            switch (id)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    break;
                default:
                    break;
            }
        }


        public void HandleClick(int id)
        {

            switch (id)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    UI.UINavigator.HandleDynamicButtonNavigation(id);
                    break;
                default:
                    break;
            }
        }
    }
}
