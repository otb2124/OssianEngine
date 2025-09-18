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
    public static class UIButtonHandler
    {

        public static bool CheckHover(RectangleF buttonRect)
        {
            PointF mousePos = new PointF(Inputs.Inputs.mouse.GetMouseWorldPosition().X, Inputs.Inputs.mouse.GetMouseWorldPosition().Y);

            if (buttonRect.Contains(mousePos))
            {
                return true;
            }

            return false;
        }

        public static bool CheckClick(RectangleF buttonRect)
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


        public static void HandleHover(int id)
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


        public static void HandleClick(int id)
        {

            switch (id)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    UI.UIInnerNavigator.HandleDynamicButtonNavigation(id);
                    break;
                default:
                    break;
            }
        }
    }
}
