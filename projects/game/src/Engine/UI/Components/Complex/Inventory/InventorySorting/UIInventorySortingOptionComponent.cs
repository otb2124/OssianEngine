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
    public class UIInventorySortingOptionComponent : UIComponent
    {

        public enum UIInventorySortingOptions
        {
            WEAPONS,
            ARMORS,
            ACCESSORIES,
            POTIONS,
            MISC,
        };

        public UIInventorySortingOptions OptionType;

        public UIInventorySortingOptionComponent(int id, Vector2 pos, UIInventorySortingOptions optionType) : base(id)
        {
            Position = pos;

            OptionType = optionType;

            type = UIComponentTypes.INVENTORY_SORTING_PANEL_OPTION;

            children = new UIComponent[2];

            SpriteData sortingIconSlotSprite = new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(64, 64, 64, 64), 0);

            children[0] = new UIButtonIconComponent(-1, 15, Position, sortingIconSlotSprite, new Vector2(0.75f, 0.75f));
            children[1] = new UIIconComponent(-1, GetSpriteData(), Position, new Vector2(0.75f, 0.75f));
        }


        public SpriteData GetSpriteData()
        {
            SpriteData spriteData = new SpriteData();

            switch(OptionType)
            {
                case UIInventorySortingOptions.WEAPONS:
                    spriteData = new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(64 * 2, 64, 64, 64), 0);
                    break;
                case UIInventorySortingOptions.ARMORS:
                    spriteData = new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(64 * 3, 64, 64, 64), 0);
                    break;
                case UIInventorySortingOptions.ACCESSORIES:
                    spriteData = new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(64 * 4, 64, 64, 64), 0);
                    break;
                case UIInventorySortingOptions.POTIONS:
                    spriteData = new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(64 * 5, 64, 64, 64), 0);
                    break;
                case UIInventorySortingOptions.MISC:
                    spriteData = new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(64 * 6, 64, 64, 64), 0);
                    break;
            }

            return spriteData;
        }


        public override void Update()
        {
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
