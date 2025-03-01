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

        public void Update()
        {
            {
                for (int i = 0; i < components.Count; i++)
                {
                    components[i].Update();
                }
            }
        }

        public void Draw()
        {
            {
                for (int i = 0; i < components.Count; i++)
                {
                    components[i].Draw();
                }
            }
        }
    }
}