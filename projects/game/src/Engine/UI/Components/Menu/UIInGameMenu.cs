using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIInGameMenu : UIComponent
    {

        public UIInGameMenu(int id) : base(id)
        {
            Position = new Vector2(0, Graphics.Graphics.screen.Height/2);

            type = UIComponentTypes.FRAME;

            children = new UIComponent[5];
            children[0] = new UIFrameComponent(-1, Position, new Vector2(100, Graphics.Graphics.screen.Height / 2));
            children[1] = new UIButtonIconComponent(-1, 0, Position);
            children[2] = new UIButtonIconComponent(-1, 1, Position);
            children[3] = new UIButtonIconComponent(-1, 2, Position);
            children[4] = new UIButtonIconComponent(-1, 3, Position);
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
    }
}
