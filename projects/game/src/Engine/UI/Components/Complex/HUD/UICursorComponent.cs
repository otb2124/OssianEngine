using Microsoft.Xna.Framework;
using Resources;
using Utils;
using Graphics;

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
            Origin = StaticSpriteFactory.spriteMappings[sprite].srcRect.Size.ToVector2();
            Origin.X -= 24;
            Origin.Y += 0;
            Scale /= 2;

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
