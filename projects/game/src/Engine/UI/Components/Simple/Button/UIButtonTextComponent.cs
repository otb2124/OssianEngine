using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using System.Drawing;
using Utils;
using static Resources.StaticSpriteFactory;
using Color = Microsoft.Xna.Framework.Color;

namespace UI
{
    public class UIButtonTextComponent : UIComponent
    {

        public int ButtonChildId;

        public UIButtonTextComponent(int id, int ButtonId, Vector2 position, string text, int fontId, Vector2 scale, Color textColor) : base(id)
        {
            type = UIComponentTypes.BUTTON_TEXT;

            Position = position;

            children = new UIComponent[2];
            children[0] = new UITextStringComponent(-1, Position, text, fontId, scale, textColor);

            //TODO: calculate the string size
            children[1] = new UIButtonComponent(-1, ButtonId, new Vector2(Position.X, Position.Y), new Vector2(100, 20));

            ButtonChildId = 1;
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
