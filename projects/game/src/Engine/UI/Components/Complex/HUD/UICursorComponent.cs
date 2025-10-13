using Microsoft.Xna.Framework;
using Resources;
using Utils;
using Graphics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace UI
{
    class UICursorComponent : UIComponent
    {
        public UICursorComponent(int id) : base(id)
        {
            type = UIComponentTypes.CURSOR;

            this.sprite = StaticSprites.UI_CURSOR;
            aManager = new AnimationManager();
            aManager.AddStaticAnimation(StaticSpriteFactory.spriteMappings[this.sprite]);

            Position = Vector2.Zero;
            Origin = StaticSpriteFactory.spriteMappings[sprite].SrcRect.Size.ToVector2();
            Origin.X -= 24;
            Origin.Y += 0;
            Scale /= 2;

            IsStickToCameraState = true;
            IsStickToCursorState = true;
            IsStickToZoomState = true;
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

        public override void DrawDebug()
        {
            Graphics.Graphics.shapes.DrawBoxFill(adjPosition.X, adjPosition.Y, 32 * adjScale.X, 32 * adjScale.Y, Color.Red);
        }
    }
}
