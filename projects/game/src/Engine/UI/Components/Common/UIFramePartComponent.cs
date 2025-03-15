using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using System.Diagnostics;
using Utils;

namespace UI
{
    class UIFramePartComponent : UIComponent
    {

        public enum UIFramePartType
        {
            CORNER_LT,
            CORNER_RT,
            CORNER_LB,
            CORNER_RB,

            BORDER_L,
            BORDER_R,
            BORDER_T,
            BORDER_B,

            INNER
        }

        public UIFramePartType type;

        public UIFramePartComponent(UIFramePartType type) : base()
        {
            this.type = type;

            aManager = new AnimationManager();
            aManager.AddStaticAnimation(mapFramePartSpriteData());

            stickToCamera = true;
            stickToZoom = true;

            Position = Vector2.Zero;
            Scale = new Vector2(3, 3);
        }

        private StaticSpriteFactory.SpriteData mapFramePartSpriteData()
        {
            switch(type)
            {
                case UIFramePartType.CORNER_LT:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[0];
                case UIFramePartType.CORNER_RT:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[1];
                case UIFramePartType.CORNER_LB:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[2];
                case UIFramePartType.CORNER_RB:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[3];
                default:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[0];
            }
            
        }

        public override void Update()
        {
            //

            base.Update();

        }

        public override void Draw()
        {
            if (aManager != null)
            {
                aManager.GetCurrent().Draw(adjPosition, Color.White, adjRotation, adjOrigin, adjScale, mapFramePartSpriteData().effect, 0f);
            }
        }
    }
}
