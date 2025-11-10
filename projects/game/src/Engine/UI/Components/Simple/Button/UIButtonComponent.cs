using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;

namespace UI
{
    public class UIButtonComponent : UIComponent
    {

        public Vector2 Size;
        public int ButtonId;

        public bool IsOnHover;
        public bool IsOnClick;

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
            base.Update();

            RectangleF buttonRect = new RectangleF(new PointF(adjPosition.X, adjPosition.Y), new SizeF(Size.X * adjScale.X, Size.Y * adjScale.Y));
            IsOnHover = UIButtonService.CheckHover(buttonRect);
            IsOnClick = UIButtonService.CheckClick(buttonRect);

            if (IsOnHover)
            {
                UIButtonService.HandleHover(ButtonId);
            }
            if (IsOnClick)
            {
                UIButtonService.HandleClick(ButtonId);
            }
        }

        public override void DrawDebug()
        {
            Graphics.Graphics.Shapes.DrawBoxFill(adjPosition.X, adjPosition.Y, Size.X * adjScale.X, Size.Y * adjScale.Y, Color.Red);
        }
    }
}
