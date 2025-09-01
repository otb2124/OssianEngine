using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIButtonFrameComponent : UIComponent
    {


        public UIButtonFrameComponent(int id, int ButtonId, Vector2 position, Vector2 size, Vector2 offset) : base(id)
        {
            type = UIComponentTypes.BUTTON_FRAME;

            Position = position;

            children = new UIComponent[2];
            children[0] = new UIButtonComponent(-1, ButtonId, new Vector2(position.X + offset.X, position.Y + offset.Y), size);
            children[1] = new UIFrameComponent(-1, new Vector2(position.X-16,position.Y-16), size);
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
