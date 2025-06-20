using Microsoft.Xna.Framework;
using Resources;
using Utils;
using Graphics;

namespace UI
{
    class UICursorComponent : UIComponent
    {
        public UICursorComponent() : base()
        {
            type = ComponentTypes.CURSOR;

            this.sprite = StaticSprites.UI_CURSOR;
            this.Init();

            Position = Vector2.Zero;
            Origin = StaticSpriteFactory.spriteMappings[sprite].srcRect.Size.ToVector2() / 2;

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
