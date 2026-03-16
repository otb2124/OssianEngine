using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UI
{
    public class UIInventorySlotBoardComponent : UIComponent
    {
        public enum UIInventorySlotBoardLayoutTypes
        {
            CUSTOM,
            INVENTORY,
            EQUIPMENT,
        };


        public List<Item> Items;

        public UIInventorySlotBoardLayoutTypes SlotLayoutType;
        public int[][] SlotLayout; //if { {-1} } then use custom layout for equipment
        public EquipmentSlot.EquipmentSlotTypes[][] EquipmentSlotTypeLayout; //to set equipment slot restrictions

        public UIInventorySlotBoardComponent(int id, Vector2 pos, List<Item> items, int[][] slotLayout = null, EquipmentSlot.EquipmentSlotTypes[][] equipmentSlotTypes = null) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_SLOTBOARD;

            children = new UIComponent[0];

            Items = items;

            SlotLayout = slotLayout;
            EquipmentSlotTypeLayout = equipmentSlotTypes;

            if (SlotLayout == null)
            {
                SlotLayoutType = UIInventorySlotBoardLayoutTypes.INVENTORY;
            }
            else if (SlotLayout != null && SlotLayout.Length == 1 && SlotLayout[0].Length == 1 && SlotLayout[0][0] == -1)
            {
                SlotLayoutType = UIInventorySlotBoardLayoutTypes.EQUIPMENT;
            }
            else
            {
                SlotLayoutType = UIInventorySlotBoardLayoutTypes.CUSTOM;
            }

            UpdateSlotsLayout();
            UpdateSlots();
            UpdateSlotItems();
        }

        public void UpdateSlots()
        {
            int slotsCount = Items.Count;

            if (slotsCount > UIInventoryPagerService.MAX_SLOT_COUNT_PER_PAGE)
            {
                slotsCount = UIInventoryPagerService.MAX_SLOT_COUNT_PER_PAGE;
            }

            children = new UIComponent[slotsCount];

            for (int row = 0; row < SlotLayout.Length; row++)
            {
                for (int col = 0; col < SlotLayout[row].Length; col++)
                {
                    int slotId = SlotLayout[row][col];
                    if (slotId == -1 || slotId >= Items.Count)
                    {
                        continue; //skip invalid slots (-1 or out-of-bounds)
                    }

                    EquipmentSlot.EquipmentSlotTypes slotType = EquipmentSlotTypeLayout != null && row < EquipmentSlotTypeLayout.Length && col < EquipmentSlotTypeLayout[row].Length
                        ? EquipmentSlotTypeLayout[row][col]
                        : EquipmentSlot.EquipmentSlotTypes.NONE;

                    children[slotId] = new UIInventorySlotComponent(
                        -1,
                        new Vector2(
                            Position.X + (((64 * 0.75f) + 4) * col),
                            Position.Y - (((64 * 0.75f) + 4) * row)
                        ),
                        slotType
                    );
                }
            }
        }


        public void UpdateSlotItems()
        {
            for (int row = 0; row < SlotLayout.Length; row++)
            {
                for (int col = 0; col < SlotLayout[row].Length; col++)
                {
                    int slotId = SlotLayout[row][col];
                    if (slotId == -1 || slotId >= Items.Count)
                    {
                        continue; //skip invalid slots (-1 or out-of-bounds)
                    }

                    ((UIInventorySlotComponent)children[slotId]).SetItem(Items[slotId]);
                }
            }
        }

        public void UpdateSlotsLayout()
        {
            if (SlotLayoutType == UIInventorySlotBoardLayoutTypes.INVENTORY)
            {
                //default structure (inventory)
                int slotsCount = Items.Count;

                if (slotsCount > UIInventoryPagerService.MAX_SLOT_COUNT_PER_PAGE)
                {
                    slotsCount = UIInventoryPagerService.MAX_SLOT_COUNT_PER_PAGE;
                }

                int slotsInRow = 5;
                int rowsCount = (int)Math.Ceiling((float)slotsCount / slotsInRow);

                SlotLayout = new int[rowsCount][];
                for (int row = 0; row < rowsCount; row++)
                {
                    int colsInRow = Math.Min(slotsInRow, slotsCount - row * slotsInRow);
                    SlotLayout[row] = new int[colsInRow];
                    for (int col = 0; col < colsInRow; col++)
                    {
                        SlotLayout[row][col] = row * slotsInRow + col;
                    }
                }
            }

            if (SlotLayoutType == UIInventorySlotBoardLayoutTypes.EQUIPMENT)
            {
                //equipmemt layout
                SlotLayout = new int[][]
                {
                    new int[] {  0, -1, -1,  5,  6},
                    new int[] {  1,  2, -1,  7,  8},
                    new int[] {  3,  4, -1,  9, 10},
                    new int[] { -1, 12, -1, 11, -1},
                };

                EquipmentSlotTypeLayout = new EquipmentSlot.EquipmentSlotTypes[][]
                {
                    new EquipmentSlot.EquipmentSlotTypes[] { EquipmentSlot.EquipmentSlotTypes.WEAPON,      EquipmentSlot.EquipmentSlotTypes.NONE,       EquipmentSlot.EquipmentSlotTypes.NONE, EquipmentSlot.EquipmentSlotTypes.NECKLACE, EquipmentSlot.EquipmentSlotTypes.CAPE},
                    new EquipmentSlot.EquipmentSlotTypes[] { EquipmentSlot.EquipmentSlotTypes.HELMET,      EquipmentSlot.EquipmentSlotTypes.CHESTPLATE, EquipmentSlot.EquipmentSlotTypes.NONE, EquipmentSlot.EquipmentSlotTypes.BELT,     EquipmentSlot.EquipmentSlotTypes.RING_L},
                    new EquipmentSlot.EquipmentSlotTypes[] { EquipmentSlot.EquipmentSlotTypes.BOOTS,       EquipmentSlot.EquipmentSlotTypes.GLOVES,     EquipmentSlot.EquipmentSlotTypes.NONE, EquipmentSlot.EquipmentSlotTypes.RING_R,   EquipmentSlot.EquipmentSlotTypes.PET },
                    new EquipmentSlot.EquipmentSlotTypes[] { EquipmentSlot.EquipmentSlotTypes.NONE,        EquipmentSlot.EquipmentSlotTypes.CONTAINMENT,EquipmentSlot.EquipmentSlotTypes.NONE, EquipmentSlot.EquipmentSlotTypes.NONE,     EquipmentSlot.EquipmentSlotTypes.NONE},
                };
            }
        }

        public override void Refresh()
        {
            if (Items.Count > 0)
            {
                UpdateSlotItems();
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