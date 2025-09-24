using Entities;
using Microsoft.Xna.Framework;
using static UI.UIInventorySortingOptionComponent;

namespace UI
{
    public class UIInventorySortingPanelComponent : UIComponent
    {

        public UIInventorySortingOptions CurrentOptionType = UIInventorySortingOptions.WEAPONS;

        public bool WasOptionTypeChangedFlag = false;

        public UIInventorySortingPanelComponent(int id, Vector2 pos) : base(id)
        {
            Position = new Vector2(pos.X, pos.Y + 64);

            type = UIComponentTypes.INVENTORY_SORTING_PANEL;

            children = new UIComponent[9];

            children[0] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((32 * 0.75f) + 2) * 0, Position.Y), UIInventorySortingOptions.NONE);
            children[1] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((32 * 0.75f) + 2) * 1, Position.Y), UIInventorySortingOptions.WEAPONS);
            children[2] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((32 * 0.75f) + 2) * 2, Position.Y), UIInventorySortingOptions.ARMORS);
            children[3] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((32 * 0.75f) + 2) * 3, Position.Y), UIInventorySortingOptions.ACCESSORIES);
            children[4] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((32 * 0.75f) + 2) * 4, Position.Y), UIInventorySortingOptions.MATERIALS);
            children[5] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((32 * 0.75f) + 2) * 5, Position.Y), UIInventorySortingOptions.CONSUMABLES);
            children[6] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((32 * 0.75f) + 2) * 6, Position.Y), UIInventorySortingOptions.KEYS);
            children[7] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((32 * 0.75f) + 2) * 7, Position.Y), UIInventorySortingOptions.QUEST_ITEMS);
            children[8] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((32 * 0.75f) + 2) * 8, Position.Y), UIInventorySortingOptions.CURRENCIES);
        }



        public override void Update()
        {
            WasOptionTypeChangedFlag = false;

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

            foreach (UIInventorySortingOptionComponent option in children)
            {
                if(option.OnClick)
                {
                    CurrentOptionType = option.OptionType;
                    WasOptionTypeChangedFlag = true;
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
