using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIManager
    {
        public List<UIComponent> components;


        public UIManager()
        {
            components = new List<UIComponent>();
        }

        public void Init()
        {
            components.Add(new UICursorComponent());
        }

        public void Draw()
        {
            //Graphics.Graphics.sprites.Draw(Resources.ResourceLoader.sprites[0].texture, Vector2.One, new Rectangle(0, 0, 64, 64), Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.FlipVertically, 0f);
            {
                for (int i = 0; i < components.Count; i++)
                {
                    components[i].Draw();
                }
            }
        }
    }
}