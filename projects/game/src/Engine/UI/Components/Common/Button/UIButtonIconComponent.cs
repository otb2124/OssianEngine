using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using System.Drawing;
using Utils;
using static Resources.StaticSpriteFactory;
using Color = Microsoft.Xna.Framework.Color;

namespace UI
{
    public class UIButtonIconComponent : UIComponent
    {


        public Vector2 Size;
        public int ButtonId;

        public bool IsOnHover;
        public bool IsOnClick;

        public UIButtonIconComponent(int id, int buttonid, Vector2 position, StaticSprites sprite, Vector2 scale) : base(id)
        {
            type = UIComponentTypes.BUTTON_ICON;

            this.sprite = sprite;
            aManager = new AnimationManager();
            aManager.AddStaticAnimation(StaticSpriteFactory.spriteMappings[this.sprite]);

            Position = position;
            Size = aManager.GetCurrent().GetCurrentFrame().Size.ToVector2();
            ButtonId = buttonid;

            IsStickToCameraState = true;
            IsStickToZoomState = true;
            IsAppliedHalfScreenOriginState = true;

            Scale = scale;
        }

        public UIButtonIconComponent(int id, int buttonid, Vector2 position, SpriteData spriteData, Vector2 scale) : base(id)
        {
            type = UIComponentTypes.BUTTON_ICON;

            this.spriteData = spriteData;
            aManager = new AnimationManager();
            aManager.AddStaticAnimation(spriteData);

            Position = position;
            Size = aManager.GetCurrent().GetCurrentFrame().Size.ToVector2();
            ButtonId = buttonid;

            IsStickToCameraState = true;
            IsStickToZoomState = true;
            IsAppliedHalfScreenOriginState = true;

            Scale = scale;
        }

        public override void Update()
        {
            base.Update();

            
            RectangleF buttonRect = new RectangleF(new PointF(adjPosition.X, adjPosition.Y), new SizeF(Size.X * adjScale.X, Size.Y * adjScale.Y));
            IsOnHover = UIButtonHandler.CheckHover(buttonRect);
            IsOnClick = UIButtonHandler.CheckClick(buttonRect);

            if(IsOnHover)
            {
                UIButtonHandler.HandleHover(ButtonId);
            }
            if(IsOnClick)
            {
                UIButtonHandler.HandleClick(ButtonId);
            }
        }


        public override void DrawDebug()
        {
            Graphics.Graphics.shapes.DrawBoxFill(adjPosition.X, adjPosition.Y, Size.X * adjScale.X, Size.Y * adjScale.Y, Color.Red);
        }
    }
}
