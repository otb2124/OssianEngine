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

namespace UI
{
    public class UIButtonIconFrameComponent : UIComponent
    {


        public UIButtonIconFrameComponent(int id, int buttonid, Vector2 position, StaticSprites sprite, Vector2 paddings) : base(id)
        {
            type = UIComponentTypes.BUTTON_ICON_FRAME;

            Position = new Vector2(position.X + paddings.X / 2 - UIFramePartComponent.FRAMEPARTSIZE, position.Y + paddings.Y / 2);

            this.sprite = sprite;
            aManager = new AnimationManager();
            aManager.AddStaticAnimation(StaticSpriteFactory.spriteMappings[this.sprite]);

            Vector2 Size = aManager.GetCurrent().GetCurrentFrame().Size.ToVector2();

            IsStickToCameraState = true;
            IsStickToZoomState = true;
            IsAppliedHalfScreenOriginState = true;

            children = new UIComponent[1];
            children[0] = new UIButtonFrameComponent(-1, buttonid, position, Size+paddings, new Vector2(0, Graphics.Graphics.screen.Height / 2 + Size.Y));
        }


        public override void Update()
        {
            if (children != null)
            {
                children[0].Update();
            }

            base.Update();
        }

        public override void Draw()
        {
            if (children != null)
            {
                children[0].Draw();
            }

            base.Draw();
        }

        public override void DrawDebug()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].DrawDebug();
                }
            }
        }
    }
}
