using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.EquipmentSlot;
using static Resources.StaticSpriteFactory;
using Utils;
using Microsoft.Xna.Framework;

namespace UI
{
    public class UIInventorySortingPanelComponent : UIComponent
    {

        public UIInventorySortingPanelComponent(int id, Vector2 pos) : base(id)
        {
            Position = new Vector2(pos.X, pos.Y + 64);

            type = UIComponentTypes.INVENTORY_SORTING_PANEL;

            children = new UIComponent[5];

            children[0] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((64 * 0.75f) + 4) * 0, Position.Y), UIInventorySortingOptionComponent.UIInventorySortingOptions.WEAPONS);
            children[1] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((64 * 0.75f) + 4) * 1, Position.Y), UIInventorySortingOptionComponent.UIInventorySortingOptions.ARMORS);
            children[2] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((64 * 0.75f) + 4) * 2, Position.Y), UIInventorySortingOptionComponent.UIInventorySortingOptions.ACCESSORIES);
            children[3] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((64 * 0.75f) + 4) * 3, Position.Y), UIInventorySortingOptionComponent.UIInventorySortingOptions.POTIONS);
            children[4] = new UIInventorySortingOptionComponent(-1, new Vector2(Position.X + ((64 * 0.75f) + 4) * 4, Position.Y), UIInventorySortingOptionComponent.UIInventorySortingOptions.MISC);
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
