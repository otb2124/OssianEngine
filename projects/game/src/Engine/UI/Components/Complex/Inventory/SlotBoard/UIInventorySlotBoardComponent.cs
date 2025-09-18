using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UI
{
    public class UIInventorySlotBoardComponent : UIComponent
    {

        public List<Item> Items;
        public int[][] SlotLayout; //if { {-1} } then use custom layout for equipment
        public EquipmentSlot.EquipmentSlotTypes[][] EquipmentSlotTypeLayout; //to set equipment slot restrictions

        public UIInventorySlotBoardComponent(int id, Vector2 pos, List<Item> items, int[][] slotLayout = null, EquipmentSlot.EquipmentSlotTypes[][] equipmentSlotTypes = null) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_SLOTBOARD;

            children = new UIComponent[0];

            Items = items;
            SlotLayout = slotLayout;

            SetSlots();
            EquipmentSlotTypeLayout = equipmentSlotTypes;
        }

        public void SetSlots()
        {
            SetSlotsLayout();
            children = new UIComponent[Items.Count];

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

                    ((UIInventorySlotComponent)children[slotId]).SetItem(Items[slotId]);
                }
            }
        }

        public void SetSlotsLayout()
        {
            if (SlotLayout == null)
            {
                //default structure (inventory)
                int slotsCount = Items.Count;
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

            if (SlotLayout != null && SlotLayout.Length == 1 && SlotLayout[0].Length == 1 && SlotLayout[0][0] == -1)
            {
                //equipmemt layout
                SlotLayout = new int[][]
                {
                    new int[] {  0,  1, -1,  6,  7},
                    new int[] {  2,  3, -1,  8,  9},
                    new int[] {  4,  5, -1, 10, 11},
                    new int[] { -1, 13, -1, 12, -1},
                };

                EquipmentSlotTypeLayout = new EquipmentSlot.EquipmentSlotTypes[][]
                {
                    new EquipmentSlot.EquipmentSlotTypes[] { EquipmentSlot.EquipmentSlotTypes.WEAPON_L,    EquipmentSlot.EquipmentSlotTypes.WEAPON_R,   EquipmentSlot.EquipmentSlotTypes.NONE, EquipmentSlot.EquipmentSlotTypes.NECKLACE, EquipmentSlot.EquipmentSlotTypes.CAPE},
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
                SetSlots();
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
