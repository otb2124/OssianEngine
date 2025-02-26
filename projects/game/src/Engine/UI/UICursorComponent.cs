using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphics;
using System.Drawing.Drawing2D;

namespace UI
{
    class UICursorComponent : UIComponent
    {
        public UICursorComponent()
        {
            Sprite = ResourceLoader.sprites[Resources.Sprite.Sprites.CURSOR];
        }

        public override void Update()
        {
            //
        }

        public override void Draw()
        {

            base.Draw();
        }
    }
}
