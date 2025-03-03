using Microsoft.Xna.Framework;
using Resources;
using Utils;

namespace UI
{
    class UICursorComponent : UIComponent
    {
        public UICursorComponent() : base()
        {
            this.sprite = StaticSprites.CURSOR;
            this.aManager.AddStaticAnimation(this.sprite);

            Position = Vector2.Zero;
            Origin = StaticSpriteFactory.spriteMappings[sprite].srcRect.Size.ToVector2()/2;

            stickToCamera = true;
            stickToCursor = true;
            stickToZoom = true;
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
