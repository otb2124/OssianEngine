using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Resources.StaticSpriteFactory;
using static UI.UIComponent;
using Utils;
using Microsoft.Xna.Framework;

namespace UI
{
    public class UIInventoryPagerComponent : UIComponent
    {

        public bool OnPrevClick = false;
        public bool OnNextClick = false;

        public UIInventoryPagerComponent(int id, Vector2 pos, string indicatorValue) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_PAGER;

            children = new UIComponent[3];

            children[0] = new UIButtonIconComponent(-1, -1, new Vector2(Position.X, Position.Y), new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(0, 64+64, 32, 32), 0), new Vector2(0.75f, 0.75f));
            children[1] = new UIButtonIconComponent(-1, -1, new Vector2(Position.X + 250, Position.Y), new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(32, 64+64, 32, 32), 0), new Vector2(0.75f, 0.75f));
            children[2] = new UITextStringComponent(-1, new Vector2(Position.X + 100, Position.Y), indicatorValue, 0, Vector2.One);
        }


        public override void Update()
        {
            OnPrevClick = false;
            OnNextClick = false;

            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i] != null)
                    {
                        children[i].Update();
                    }
                }
            }

            if (((UIButtonIconComponent)children[0]).IsOnClick)
            {
                OnPrevClick = true;
            }

            if (((UIButtonIconComponent)children[1]).IsOnClick)
            {
                OnNextClick = true;
            }
        }


        public void UpdateIndicator(string indicatorValue)
        {
            children[2].text = indicatorValue;
        }

        public override void Draw()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i] != null)
                    {
                        children[i].Draw();
                    }
                }
            }
        }
    }
}
