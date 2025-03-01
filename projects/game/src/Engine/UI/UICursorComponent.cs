using Resources;
using System.Diagnostics;

namespace UI
{
    class UICursorComponent : UIComponent
    {
        public UICursorComponent() : base()
        {
            this.sprite = StaticSpriteFactory.StaticSprites.CURSOR;
            this.aManager.AddStaticAnimation(this.sprite);
            stickToCamera = true;
            stickToCursor = true;
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
