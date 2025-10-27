using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Resources;
using SharpDX.Direct3D9;
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

        public UIFramePartType frameType;

        public const int FRAMEPARTSIZE = 16;

        public UIFramePartComponent(int id, UIFramePartType frameType, Vector2 framePos, Vector2 frameSize) : base(id)
        {
            this.frameType = frameType;

            type = UIComponentTypes.FRAMEPART;

            aManager = new Animator(mapFramePartSpriteData());

            IsStickToCameraState = true;
            IsStickToZoomState = true;
            IsAppliedHalfScreenOriginState = true;

            adjustAll(framePos, frameSize, new Vector2(3, 3));
        }

        private StaticSpriteFactory.SpriteData mapFramePartSpriteData()
        {
            switch(frameType)
            {
                case UIFramePartType.CORNER_LB:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[2];
                case UIFramePartType.CORNER_RB:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[3];
                case UIFramePartType.CORNER_LT:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[0];
                case UIFramePartType.CORNER_RT:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[1];
                

                case UIFramePartType.BORDER_T:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[4];
                case UIFramePartType.BORDER_B:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[5];

                case UIFramePartType.BORDER_L:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[6];
                case UIFramePartType.BORDER_R:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[7];

                case UIFramePartType.INNER:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[8];

                default:
                    return StaticSpriteFactory.UIFrameCut(new Vector2(0, 0), 16)[0];
            }
        }


        private void adjustAll(Vector2 framePos, Vector2 frameSize, Vector2 scale)
        {
            Vector2 adjustedPosition = framePos;
            Vector2 adjustedScale = scale;

            Vector2 cornerSize = new Vector2(FRAMEPARTSIZE / 2 * scale.X, FRAMEPARTSIZE / 2 * scale.Y);
            Vector2 borderSize = cornerSize * 2;

            //FRAME POSITION - OFFSET FOR CORNER SPRITE * SCALE
            Vector2 leftBottomCorner = new Vector2(framePos.X - cornerSize.X, framePos.Y - cornerSize.Y);
            //FRAME POSITION - OFFSET FOR CORNER SPRITE * SCALE + FRAME SIZE + HORIZONTAL FLIP OFFSET
            Vector2 rightBottomCorner = new Vector2(leftBottomCorner.X + frameSize.X, leftBottomCorner.Y);

            Vector2 leftTopCorner = new Vector2(leftBottomCorner.X, leftBottomCorner.Y + frameSize.Y);
            Vector2 rightTopCorner = new Vector2(rightBottomCorner.X, leftTopCorner.Y);

            Vector2 bottomBorder = new Vector2(framePos.X + cornerSize.X, framePos.Y - cornerSize.Y);
            Vector2 topBorder = new Vector2(bottomBorder.X, framePos.Y + frameSize.Y - cornerSize.Y);
            Vector2 leftBorder = new Vector2(framePos.X - cornerSize.X, framePos.Y + cornerSize.Y);
            Vector2 rightBorder = new Vector2(leftBorder.X + frameSize.X, leftBorder.Y);

            switch (frameType)
            {

                case UIFramePartType.CORNER_LB:
                    adjustedPosition = leftBottomCorner;
                    break;
                case UIFramePartType.CORNER_RB:
                    adjustedPosition = rightBottomCorner;
                    break;
                case UIFramePartType.CORNER_LT:
                    adjustedPosition = leftTopCorner;
                    break;
                case UIFramePartType.CORNER_RT:
                    adjustedPosition = rightTopCorner;
                    break;

                case UIFramePartType.BORDER_B:
                    adjustedPosition = bottomBorder;
                    break;
                case UIFramePartType.BORDER_T:
                    adjustedPosition = topBorder;
                    break;

                case UIFramePartType.BORDER_L:
                    adjustedPosition = leftBorder;
                    break;
                case UIFramePartType.BORDER_R:
                    adjustedPosition = rightBorder;
                    break;

                case UIFramePartType.INNER:
                    adjustedPosition = new Vector2(framePos.X, framePos.Y);
                    break;
            }


            Vector2 frameSizeInTiles = new Vector2(frameSize.X / cornerSize.X/2, frameSize.Y / cornerSize.Y/2);
            Vector2 frameSizeInTilesWithCornerOffset = new Vector2(frameSizeInTiles.X - 1, frameSizeInTiles.Y - 1);

            switch (frameType)
            {
                case UIFramePartType.BORDER_T:
                    adjustedScale *= new Vector2(frameSizeInTilesWithCornerOffset.X, 1);
                    break;
                case UIFramePartType.BORDER_B:
                    adjustedScale *= new Vector2(frameSizeInTilesWithCornerOffset.X, 1);
                    break;
                case UIFramePartType.BORDER_L:
                    adjustedScale *= new Vector2(1, frameSizeInTilesWithCornerOffset.Y);
                    break;
                case UIFramePartType.BORDER_R:
                    adjustedScale *= new Vector2(1, frameSizeInTilesWithCornerOffset.Y);
                    break;

                case UIFramePartType.INNER:
                    adjustedScale *= new Vector2(frameSizeInTiles.X, frameSizeInTiles.Y);
                    break;
            }


            Position = adjustedPosition;
            Scale = adjustedScale;
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
                aManager.DrawCurrent(adjPosition, Color, adjRotation, adjOrigin, adjScale, 0f, mapFramePartSpriteData().Effect);
            }
        }
    }
}
