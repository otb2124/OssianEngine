using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIButtonComponent : UIComponent
    {

        public Vector2 Size;
        public int ButtonId;

        public UIButtonComponent(int id, int buttonid, Vector2 position, Vector2 size) : base(id)
        {
            type = UIComponentTypes.BUTTON;

            Position = position;
            Size = size;
            ButtonId = buttonid;

            IsStickToCameraState = true;
            IsStickToZoomState = true;
            IsAppliedHalfScreenOriginState = true;
        }


        public override void Update()
        {
            //
            //UI.UIButtonHandler.CheckHover(ButtonId, Position, Size);

            base.Update();
        }

        public override void DrawDebug()
        {
            Graphics.Graphics.shapes.DrawBoxFill(Position.X, Position.Y, Size.X, Size.Y, Color.Red);
        }
    }
}
