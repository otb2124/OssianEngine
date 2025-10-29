using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Resources.StaticSpriteFactory;
using Utils;
using Microsoft.Xna.Framework;
using Resources;

namespace UI
{
    public class UIInventorySortingOptionComponent : UIComponent
    {

        
        public UIInventorySortingOptions OptionType;
        public bool OnClick = false;

        public UIInventorySortingOptionComponent(int id, Vector2 pos, UIInventorySortingOptions optionType) : base(id)
        {
            Position = pos;

            OptionType = optionType;

            type = UIComponentTypes.INVENTORY_SORTING_PANEL_OPTION;

            children = new UIComponent[2];

            StaticSprite sortingIconSlotSprite = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 16 * 0, 128, 32, 32), 0);

            children[0] = new UIButtonIconComponent(-1, 15, Position, sortingIconSlotSprite, new Vector2(0.75f, 0.75f));
            children[1] = new UIIconComponent(-1, GetSpriteData(), Position, new Vector2(0.75f, 0.75f));
        }


        public StaticSprite GetSpriteData()
        {
            StaticSprite spriteData = new StaticSprite();

            switch(OptionType)
            {
                case UIInventorySortingOptions.NONE:
                    spriteData = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 32 * 1, 128, 32, 32), 0);
                    break;
                case UIInventorySortingOptions.WEAPONS:
                    spriteData = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 32 * 2, 128, 32, 32), 0);
                    break;
                case UIInventorySortingOptions.ARMORS:
                    spriteData = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 32 * 3, 128, 32, 32), 0);
                    break;
                case UIInventorySortingOptions.ACCESSORIES:
                    spriteData = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 32 * 4, 128, 32, 32), 0);
                    break;
                case UIInventorySortingOptions.MATERIALS:
                    spriteData = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 32 * 5, 128, 32, 32), 0);
                    break;
                case UIInventorySortingOptions.CONSUMABLES:
                    spriteData = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 32 * 6, 128, 32, 32), 0);
                    break;
                case UIInventorySortingOptions.KEYS:
                    spriteData = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 32 * 7, 128, 32, 32), 0);
                    break;
                case UIInventorySortingOptions.QUEST_ITEMS:
                    spriteData = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 32 * 8, 128, 32, 32), 0);
                    break;
                case UIInventorySortingOptions.CURRENCIES:
                    spriteData = new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64 + 32 * 9, 128, 32, 32), 0);
                    break;
            }

            return spriteData;
        }


        public override void Update()
        {
            OnClick = false;

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
                OnClick = true;
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
