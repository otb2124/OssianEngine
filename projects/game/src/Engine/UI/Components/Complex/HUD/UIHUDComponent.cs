using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Resources.StaticSpriteFactory;

namespace UI
{
    public class UIHUDComponent : UIComponent
    {

        public UIHUDComponent(int id) : base(id)
        {
            
            Position = Vector2.Zero;

            type = UIComponentTypes.HUD;

            children = new UIComponent[2];
            children[0] = new UICursorComponent(-1);

            //INDICATORS
            Vector2 topLeft = new Vector2(0, Graphics.Graphics.Screen.Height - 32*1.5f);
            children[1] = new UIPlayerIndicatorsComponent(-1, new Vector2(Position.X, Position.Y + topLeft.Y - 16));
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
