using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Resources.StaticSpriteFactory;

namespace UI
{
    public class UIIngameMenuComponent : UIComponent
    {

        public UIIngameMenuComponent(int id) : base(id)
        {
            Vector2 margins = new Vector2(10, 80);
            Point iconSrcRectSize = new Point(64, 64);
            Vector2 buttonSize = new Vector2(iconSrcRectSize.X, iconSrcRectSize.Y);
            Vector2 buttonMargins = new Vector2(8, 12);

            Vector2 frameSize = new Vector2(80, (buttonSize.Y + buttonMargins.Y) * 6);
            Position = new Vector2(0 + margins.X, Graphics.Graphics.Screen.Height - frameSize.Y - margins.Y);

            type = UIComponentTypes.MENU_INGAME;

            children = new UIComponent[7];
            children[0] = new UIFrameComponent(-1, Position, frameSize);

            children[1] = new UIButtonIconComponent(-1, 0, new Vector2(Position.X + buttonMargins.X, Position.Y + frameSize.Y - buttonSize.Y*1 - buttonMargins.Y*0), new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(iconSrcRectSize.X * 0, 0, iconSrcRectSize.X, iconSrcRectSize.Y), 100), Vector2.One);
            children[2] = new UIButtonIconComponent(-1, 1, new Vector2(Position.X + buttonMargins.X, Position.Y + frameSize.Y - buttonSize.Y*2 - buttonMargins.Y * 1), new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(iconSrcRectSize.X * 1, 0, iconSrcRectSize.X, iconSrcRectSize.Y), 100), Vector2.One);
            children[3] = new UIButtonIconComponent(-1, 2, new Vector2(Position.X + buttonMargins.X, Position.Y + frameSize.Y - buttonSize.Y*3 - buttonMargins.Y * 2), new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(iconSrcRectSize.X * 2, 0, iconSrcRectSize.X, iconSrcRectSize.Y), 100), Vector2.One);
            children[4] = new UIButtonIconComponent(-1, 3, new Vector2(Position.X + buttonMargins.X, Position.Y + frameSize.Y - buttonSize.Y*4 - buttonMargins.Y * 3), new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(iconSrcRectSize.X * 3, 0, iconSrcRectSize.X, iconSrcRectSize.Y), 100), Vector2.One);
            children[5] = new UIButtonIconComponent(-1, 4, new Vector2(Position.X + buttonMargins.X, Position.Y + frameSize.Y - buttonSize.Y * 5 - buttonMargins.Y * 4), new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(iconSrcRectSize.X * 4, 0, iconSrcRectSize.X, iconSrcRectSize.Y), 100), Vector2.One);
            children[6] = new UIButtonIconComponent(-1, 5, new Vector2(Position.X + buttonMargins.X, Position.Y + frameSize.Y - buttonSize.Y * 6 - buttonMargins.Y * 5), new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(iconSrcRectSize.X * 5, 0, iconSrcRectSize.X, iconSrcRectSize.Y), 100), Vector2.One);
        }

        public override void Update()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Update();
                }
            }
        }

        public override void Draw()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Draw();
                }
            }
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

        public override void Destroy()
        {
        }
    }
}
