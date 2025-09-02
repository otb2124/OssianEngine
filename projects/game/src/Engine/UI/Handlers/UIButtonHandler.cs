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


        public void CheckHover(int id, RectangleF buttonRect)
        {
            PointF mousePos = new PointF(Inputs.Inputs.mouse.GetMouseWorldPosition().X, Inputs.Inputs.mouse.GetMouseWorldPosition().Y);

            if (buttonRect.Contains(mousePos))
            {
                HandleHover(id);
                CheckClick(id);
            }
        }

        public void CheckClick(int id)
        {
            if(Inputs.Inputs.mouse.IsLeftMouseButtonPressed())
            {
                HandleClick(id);
            }
        }

        public void HandleHover(int id)
        {

            Debug.WriteLine("Button with Id " + id + " on hover");


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
                    Debug.WriteLine("Button with Id " + "empty" + " on hover");
                    break;
            }
        }


        public void HandleClick(int id)
        {
            Debug.WriteLine("Button with Id " + id + " was hit");

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
                    Debug.WriteLine("Button with Id " + "empty" + " was hit");
                    break;
            }
        }
    }
}
