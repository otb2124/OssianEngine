using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Resources.StaticSpriteFactory;
using Color = Microsoft.Xna.Framework.Color;

namespace UI
{
    public class UIButtonIconComponent : UIComponent
    {


        public Vector2 Size;
        public int ButtonId;

        public UIButtonIconComponent(int id, int buttonid, Vector2 position, StaticSprites sprite, Vector2 scale) : base(id)
        {
            type = UIComponentTypes.BUTTON_ICON;

            this.sprite = sprite;
            aManager = new AnimationManager();
            aManager.AddStaticAnimation(StaticSpriteFactory.spriteMappings[this.sprite]);

            Position = position;
            Size = aManager.GetCurrent().GetCurrentFrame().Size.ToVector2();
            ButtonId = buttonid;

            stickToCamera = true;
            stickToZoom = true;
            applyHalfScreenOrigin = true;

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

            stickToCamera = true;
            stickToZoom = true;
            applyHalfScreenOrigin = true;

            Scale = scale;
        }

        public override void Update()
        {
            base.Update();

            RectangleF buttonRect = new RectangleF(new PointF(adjPosition.X - Graphics.Graphics.camera.position.X + Graphics.Graphics.screen.Width / 4f - Size.X, adjPosition.Y - Graphics.Graphics.camera.position.Y + Graphics.Graphics.screen.Height / 4f), new SizeF(Size.X, Size.Y));

            Console.WriteLine("buttonRect" + buttonRect);

            UI.UIButtonHandler.CheckHover(ButtonId, buttonRect);
        }


        public override void DrawDebug()
        {
            Graphics.Graphics.shapes.DrawBoxFill(adjPosition.X + Graphics.Graphics.screen.Width / 4f - Size.X, adjPosition.Y + Graphics.Graphics.screen.Height / 4f, Size.X * adjScale.X, Size.Y * adjScale.Y, Color.Red);
        }
    }
}
