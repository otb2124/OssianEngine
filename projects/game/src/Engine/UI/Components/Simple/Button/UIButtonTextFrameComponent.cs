using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIButtonTextFrameComponent : UIComponent
    {

        public UIButtonTextFrameComponent(int id, int ButtonId, Vector2 position, string text, int fontId, Vector2 scale, Vector2 paddings, Color textColor) : base(id)
        {
            type = UIComponentTypes.BUTTON_TEXT_FRAME;

            children = new UIComponent[2];
            children[0] = new UITextFrameComponent(-1, position, text, fontId, scale, paddings, textColor);

            Position = ((UITextFrameComponent)children[0]).FramePos;

            children[1] = new UIButtonComponent(-1, ButtonId, new Vector2(Position.X, Position.Y + Graphics.Graphics.Screen.Height + ((UITextFrameComponent)children[0]).FrameSize.Y), ((UITextFrameComponent)children[0]).FrameSize);
        }


        public override void Update()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Update();
                }
            }
        }

        public override void Draw()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Draw();
                }
            }
        }

        public override void DrawDebug()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].DrawDebug();
                }
            }
        }
    }
}
