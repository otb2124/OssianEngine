using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UI
{
    public class UIEquipmentComponent : UIComponent
    {

        public UIEquipmentComponent(int id, Vector2 pos, LivingEntity ent) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY;

            EquipmentSlot[] slots = ent.sManager.equipmentManager.slots;

            children = new UIComponent[0];

            if (slots.Length > 0)
            {
                children = new UIComponent[slots.Length];

                SetInventory(slots);
            }
        }

        public void SetInventory(EquipmentSlot[] inventory)
        {
            // Define slot indices for left and right columns
            var leftColumnIndices = new[]
            {
                new[] { 0, 1 },    // Row 1
                new[] { 6, 7 },    // Row 2
                new[] { 8, 9 },    // Row 3
                new[] { 10, 11 },  // Row 4
                new[] { 12 }       // Row 5 (single item)
            };
            var rightColumnIndices = new[] { 2, 3, 4, 5 }; // Right column: one item per row

            float slotSize = 64 * 0.75f; // Consistent with original code
            float slotSpacing = 4;
            float rightColumnOffsetX = slotSize * 2 + slotSpacing * 2 + 20; // Offset for right column

            // Populate left column
            for (int row = 0; row < leftColumnIndices.Length && row * 2 < inventory.Length; row++)
            {
                for (int col = 0; col < leftColumnIndices[row].Length && (row * 2 + col) < inventory.Length; col++)
                {
                    int index = leftColumnIndices[row][col];
                    if (index >= inventory.Length) continue; // Skip if index exceeds inventory length

                    children[index] = new UIEquipmentSlotComponent( 
                        -1,
                        new Vector2(
                            Position.X + (slotSize + slotSpacing) * col,
                            Position.Y - (slotSize + slotSpacing) * row
                        )
                    );

                    if (inventory[index].Equipment != null)
                    {
                        ((UIEquipmentSlotComponent)children[index]).SetItem(inventory[index].Equipment);
                    }
                }
            }

            // Populate right column
            for (int row = 0; row < rightColumnIndices.Length && row < inventory.Length; row++)
            {
                int index = rightColumnIndices[row];
                if (index >= inventory.Length) continue; // Skip if index exceeds inventory length

                children[index] = new UIEquipmentSlotComponent(
                    -1,
                    new Vector2(
                        Position.X + rightColumnOffsetX,
                        Position.Y - (slotSize + slotSpacing) * row
                    )
                );

                if (inventory[index].Equipment != null)
                {
                    ((UIEquipmentSlotComponent)children[index]).SetItem(inventory[index].Equipment);
                }
            }
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
    }
}
