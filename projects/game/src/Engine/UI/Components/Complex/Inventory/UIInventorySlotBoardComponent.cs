using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static UI.UIComponent;

namespace UI
{
    public class UIInventorySlotBoardComponent : UIComponent
    {

        private UIInventorySlotComponent CurrentDraggingSlot;
        private Item DraggedItem;
        private StatsEntity Entity;

        public UIInventorySlotBoardComponent(int id, Vector2 pos, StatsEntity ent) : base(id)
        {
            Position = new Vector2(pos.X, pos.Y);

            type = UIComponentTypes.INVENTORY_SLOTBOARD;

            Entity = ent;

            Inventory inventory = ent.Inventory;

            children = new UIComponent[0];

            if (inventory.SlotsAmount > 0)
            {
                children = new UIComponent[inventory.SlotsAmount + 1];
                children[0] = new UIFrameComponent(-1, new Vector2(100, 20), new Vector2(300, 620));

                SetInventory(inventory);
            }

            CurrentDraggingSlot = null;
            DraggedItem = null;
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
                    int childIndex = slotIndex + 1;
                    children[childIndex] = new UIInventorySlotComponent(
                        -1,
                        new Vector2(
                            new Vector2(100 + 20, 500).X + (((64 * 0.75f) + 4) * col),
                            new Vector2(100 + 20, 500).Y - (((64 * 0.75f) + 4) * row)
                        )
                    );

                    if (slotIndex < inventory.Items.Count)
                    {
                        ((UIInventorySlotComponent)children[childIndex]).SetItem(inventory.Items[slotIndex]);
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

            // Check for dragging slot
            if (CurrentDraggingSlot == null)
            {
                foreach (var child in children)
                {
                    if (child is UIInventorySlotComponent slot && slot.IsDragging)
                    {
                        Console.WriteLine("dragging");
                        CurrentDraggingSlot = slot;
                        DraggedItem = slot.Item;
                        break;
                    }
                }
            }

            // Check for drop
            if (CurrentDraggingSlot != null && !Inputs.Inputs.mouse.IsLeftMouseButtonDown())
            {
                PointF mousePos = new PointF(Inputs.Inputs.mouse.GetMouseWorldPosition().X, Inputs.Inputs.mouse.GetMouseWorldPosition().Y);
                float screenHeight = Graphics.Graphics.screen.Height;

                Console.WriteLine("dropped");

                foreach (var child in children)
                {
                    if (child is UIInventorySlotComponent slot && slot != CurrentDraggingSlot)
                    {
                        if (slot.children[0] is UIButtonIconComponent button && button.IsOnHover)
                        {
                            // Swap items
                            Item tempItem = slot.Item;
                            slot.SetItem(DraggedItem);
                            CurrentDraggingSlot.SetItem(tempItem);

                            // Update inventory
                            int draggedChildIndex = Array.IndexOf(children, CurrentDraggingSlot);
                            int targetChildIndex = Array.IndexOf(children, slot);
                            if (draggedChildIndex > 0 && targetChildIndex > 0)
                            {
                                int draggedSlotIndex = draggedChildIndex - 1;
                                int targetSlotIndex = targetChildIndex - 1;
                                Console.WriteLine($"successful drop: children[{draggedChildIndex}] (slot {draggedSlotIndex}) <-> children[{targetChildIndex}] (slot {targetSlotIndex})");

                                Inventory inventory = Entity.Inventory;
                                if (inventory != null)
                                {
                                    // Ensure inventory.Items has enough capacity
                                    while (inventory.Items.Count <= Math.Max(draggedSlotIndex, targetSlotIndex))
                                    {
                                        inventory.Items.Add(null);
                                    }
                                    inventory.Items[draggedSlotIndex] = slot.Item;
                                    inventory.Items[targetSlotIndex] = tempItem;
                                }
                            }
                            break;
                        }
                    }
                }

                CurrentDraggingSlot = null;
                DraggedItem = null;
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
