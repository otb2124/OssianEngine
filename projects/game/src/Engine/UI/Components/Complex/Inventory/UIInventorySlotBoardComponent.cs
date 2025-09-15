using Entities;
using Microsoft.Xna.Framework;
using System;

namespace UI
{
    public class UIInventorySlotBoardComponent : UIComponent
    {

        public Inventory Inventory;
        public bool AllowSwapBetweenSlots;

        public UIInventorySlotBoardComponent(int id, Vector2 pos, Inventory inv, bool allowSwapBetweenSlots = true) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_SLOTBOARD;

            children = new UIComponent[0];

            Inventory = inv;
            AllowSwapBetweenSlots = allowSwapBetweenSlots;

            SetInventory(Inventory);
        }

        public void SetInventory(Inventory inventory)
        {
            children = new UIComponent[inventory.SlotsAmount];

            int slotsCount = inventory.SlotsAmount;
            int slotsInRow = 5;
            int rowsCount = (int)Math.Ceiling((float)slotsCount / slotsInRow);

            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < slotsInRow && (row * slotsInRow + col) < slotsCount; col++)
                {
                    int slotIndex = row * slotsInRow + col;
                    int childIndex = slotIndex;
                    children[childIndex] = new UIInventorySlotComponent(
                        -1,
                        new Vector2(
                            Position.X + (((64 * 0.75f) + 4) * col),
                            Position.Y - (((64 * 0.75f) + 4) * row)
                        )
                    );

                    if (slotIndex < inventory.Items.Count)
                    {
                        ((UIInventorySlotComponent)children[childIndex]).SetItem(inventory.Items[slotIndex]);
                    }
                }
            }
        }

        public override void Refresh()
        {
            if (Inventory.SlotsAmount > 0)
            {
                SetInventory(Inventory);
            }

            base.Refresh();
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

            if (!AllowSwapBetweenSlots)
            {
                return;
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
    }
}
