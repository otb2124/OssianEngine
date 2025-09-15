using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UI
{
    public class UIInventorySlotBoardComponent : UIComponent
    {
        public InventorySlotDragNDropManager InventorySlotDragNDropManager;

        public Inventory Inventory;
        public bool AllowSwapBetweenSlots;

        public UIInventorySlotBoardComponent(int id, Vector2 pos, Inventory inv, bool allowSwapBetweenSlots = true) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_SLOTBOARD;

            children = new UIComponent[0];

            Inventory = inv;
            AllowSwapBetweenSlots = allowSwapBetweenSlots;

            InventorySlotDragNDropManager = new InventorySlotDragNDropManager(Inventory.Items);

            Refresh();
            InventorySlotDragNDropManager.Refresh();
        }

        public void SetInventory(Inventory inventory)
        {
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
                children = new UIComponent[Inventory.SlotsAmount];
                SetInventory(Inventory);

                InventorySlotDragNDropManager.Slots = new List<UIComponent>();
                InventorySlotDragNDropManager.AddSlots(children);
            }
        }

        public override void Update()
        {
            if(InventorySlotDragNDropManager.WasRefreshedFlag)
            {
                Refresh();
            }

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


            InventorySlotDragNDropManager.Update();
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
