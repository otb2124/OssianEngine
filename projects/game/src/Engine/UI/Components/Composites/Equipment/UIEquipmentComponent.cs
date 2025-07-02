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

            type = UIComponentTypes.EQUIPMENT;

            EquipmentSlot[] slots = ent.statsManager.equipmentManager.slots;

            Vector2 inGameMenuSize = new Vector2(80, (64 + 12) * 6);
            Vector2 inGameMenuPos = new Vector2(0 + 10, Graphics.Graphics.screen.Height - inGameMenuSize.Y - 10);

            Vector2 frameSize = new Vector2((inGameMenuSize.X + 10 + 10 + 10 + 10 + 10) * 3, Graphics.Graphics.screen.Height - (10 + 10));
            Vector2 framePos = new Vector2(Position.X, Graphics.Graphics.screen.Height - frameSize.Y);

            if (slots.Length > 0)
            {
                // Initialize children array with size slots.Length + 1 to accommodate the frame
                children = new UIComponent[slots.Length + 1];

                // Add UIFrameComponent as child 0
                children[0] = new UIFrameComponent(-1, framePos, frameSize);

                SetInventory(slots);
            }
        }

        public void SetInventory(EquipmentSlot[] inventory)
        {
            // Define slot indices for left and right columns
            var leftColumnIndices = new[]
            {
                new[] { 0, 1 },    // Row 1
                new[] { 3, 2 },    // Row 2
                new[] { 5, 4 },    // Row 3
                new[] { 13 },      // Row 4 (single item)
            };

            var rightColumnIndices = new[]
            {
                new[] { 6, 7 },    // Row 1
                new[] { 9, 10 },   // Row 2
                new[] { 8 },       // Row 3 (single item)
                new[] { 11, 12 },  // Row 4
            };

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

                    // Store in children[index + 1] to account for frame at index 0
                    children[index + 1] = new UIEquipmentSlotComponent(
                        -1,
                        new Vector2(
                            Position.X + (slotSize + slotSpacing) * col,
                            Position.Y - (slotSize + slotSpacing) * row
                        )
                    );

                    if (inventory[index].Equipment != null)
                    {
                        ((UIEquipmentSlotComponent)children[index + 1]).SetItem(inventory[index].Equipment);
                    }
                }
            }

            // Populate right column
            for (int row = 0; row < rightColumnIndices.Length && row * 2 < inventory.Length; row++)
            {
                for (int col = 0; col < rightColumnIndices[row].Length && (row * 2 + col) < inventory.Length; col++)
                {
                    int index = rightColumnIndices[row][col];
                    if (index >= inventory.Length) continue; // Skip if index exceeds inventory length

                    // Store in children[index + 1] to account for frame at index 0
                    children[index + 1] = new UIEquipmentSlotComponent(
                        -1,
                        new Vector2(
                            Position.X + rightColumnOffsetX + (slotSize + slotSpacing) * col,
                            Position.Y - (slotSize + slotSpacing) * row
                        )
                    );

                    if (inventory[index].Equipment != null)
                    {
                        ((UIEquipmentSlotComponent)children[index + 1]).SetItem(inventory[index].Equipment);
                    }
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
