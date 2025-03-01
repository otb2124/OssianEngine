using Resources;

namespace UI
{
    class UICursorComponent : UIComponent
    {
        public UICursorComponent() : base()
        {
            this.sprite = StaticSpriteFactory.StaticSprites.CURSOR;
            this.aManager.AddStaticAnimation(this.sprite);
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
