using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UITextFrameComponent : UIComponent
    {

        public Vector2 Paddings;
        public Vector2 FrameSize;
        public Vector2 FramePos;

        public UITextFrameComponent(int id, Vector2 position, string text, int fontId, Vector2 scale, Vector2 paddings, Color color) : base(id)
        {
            Position = position;
            Paddings = paddings;

            type = UIComponentTypes.TEXT_FRAME;

            children = new UIComponent[2];
            children[0] = new UITextStringComponent(-1, Position, text, fontId, scale, color);
            children[1] = CalculateFrameProperties(children[0] as UITextStringComponent);
        }


        public UIFrameComponent CalculateFrameProperties(UITextStringComponent textStringComponent)
        {

            SpriteFont spriteFont = textStringComponent.Font.GetCurrentFont();

            Vector2 textSize = spriteFont.MeasureString(textStringComponent.Text);

            textSize *= textStringComponent.Scale;

            float frameCornerOffsetX = 10f;
            float frameCornerOffsetY = 10f;
            Vector2 frameSize = new Vector2(textSize.X*2 - frameCornerOffsetX*2, textSize.Y + frameCornerOffsetY);
            Vector2 framePosition = new Vector2(textStringComponent.Position.X - frameCornerOffsetX, textStringComponent.Position.Y);


            //APPLY SIZE PADDINGS
            FrameSize = new Vector2(frameSize.X + Paddings.X, frameSize.Y + Paddings.Y);

            //CENTER
            FramePos = new Vector2(framePosition.X - Paddings.X/2, framePosition.Y - Paddings.Y/2);

            return new UIFrameComponent(-1, FramePos, FrameSize);
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
                children[1].Draw();
                children[0].Draw();
            }
        }
    }
}
