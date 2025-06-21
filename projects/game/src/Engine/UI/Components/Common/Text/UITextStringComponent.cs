using Graphics;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static UI.UIFramePartComponent;
using Resources;

namespace UI
{
    public class UITextStringComponent : UIComponent
    {



        public UITextStringComponent(int id, Vector2 position, string text, string[] fontAttr, Vector2 scale) : base(id)
        {
            this.text = text;
            this.font = new Font(fontAttr);

            stickToCamera = true;
            stickToZoom = true;
            applyHalfScreenOrigin = true;

            Position = position;
            Scale = scale;

            type = UIComponentTypes.TEXT;
        }


        public override void Update()
        {
            //

            base.Update();

        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
