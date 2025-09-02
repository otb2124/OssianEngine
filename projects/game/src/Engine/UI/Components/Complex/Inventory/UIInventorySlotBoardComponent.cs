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
        public int FromSlotId;
        public int ToSlotId;
        private Inventory Inventory;

        public bool IsRightDrag;

        public UIInventorySlotBoardComponent(int id, Vector2 pos, StatsEntity ent) : base(id)
        {
            Position = new Vector2(pos.X, pos.Y);

            type = UIComponentTypes.INVENTORY_SLOTBOARD;


            Inventory = ent.Inventory;

            children = new UIComponent[0];

            Refresh();

            IsRightDrag = false;

            FromSlotId = -1;
            ToSlotId = -1;
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

        public void Refresh()
        {
            if (Inventory.SlotsAmount > 0)
            {
                children = new UIComponent[Inventory.SlotsAmount + 1];
                children[0] = new UIFrameComponent(-1, new Vector2(100, 20), new Vector2(300, 620));

                SetInventory(Inventory);
            }

            FromSlotId = -1;
            ToSlotId = -1;
            IsRightDrag = false;
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

            // Check for dragging slot
            if (FromSlotId == -1)
            {
                foreach (var child in children)
                {
                    if (child is UIInventorySlotComponent fromSlot && (fromSlot.IsLeftDragging || fromSlot.IsRightDragging))
                    {
                        FromSlotId = Array.IndexOf(children, fromSlot) - 1;
                        IsRightDrag = fromSlot.IsRightDragging;
                        break;
                    }
                }
            }

            // Check for drop
            if (FromSlotId != -1 && !Inputs.Inputs.mouse.IsLeftMouseButtonDown() && !Inputs.Inputs.mouse.IsRightMouseButtonDown())
            {
                PointF mousePos = new PointF(Inputs.Inputs.mouse.GetMouseWorldPosition().X, Inputs.Inputs.mouse.GetMouseWorldPosition().Y);
                float screenHeight = Graphics.Graphics.screen.Height;

                UIInventorySlotComponent fromSlot = (FromSlotId + 1 < children.Length) ? (UIInventorySlotComponent)children[FromSlotId + 1] : null;
                if (fromSlot == null)
                {
                    FromSlotId = -1;
                    IsRightDrag = false;
                    return;
                }

                foreach (var child in children)
                {
                    if (child is UIInventorySlotComponent toSlot && child != fromSlot)
                    {
                        if (toSlot.children[0] is UIButtonIconComponent button && button.IsOnHover)
                        {
                            ToSlotId = Array.IndexOf(children, toSlot) - 1;

                            if (Inventory != null && ToSlotId != FromSlotId && ToSlotId >= 0)
                            {
                                // Ensure inventory.Items has enough capacity
                                while (Inventory.Items.Count <= Math.Max(FromSlotId, ToSlotId))
                                {
                                    Inventory.Items.Add(null);
                                }

                                Item draggedItem = fromSlot.Item;


                                if(fromSlot.Item != null)
                                {
                                    if (IsRightDrag)
                                    {
                                        if (fromSlot.Item.Stackable)
                                        {
                                            if (toSlot.Item != null && toSlot.Item.Stackable)
                                            {
                                                if (toSlot.Item.ItemKey == draggedItem.ItemKey)
                                                {
                                                    toSlot.Item.Count += 1;
                                                    fromSlot.Item.Count -= 1;
                                                    Inventory.Items[ToSlotId] = toSlot.Item;
                                                    Inventory.Items[FromSlotId] = fromSlot.Item;
                                                }
                                                else
                                                {
                                                    SwapItems(toSlot, fromSlot, draggedItem);
                                                }
                                            }
                                            else
                                            {
                                                if (fromSlot.Item.Count > 1)
                                                {
                                                    draggedItem = new Item(fromSlot.Item.ItemKey) { Count = 1, Stackable = fromSlot.Item.Stackable, Type = fromSlot.Item.Type };
                                                    fromSlot.Item.Count -= 1;
                                                    fromSlot.SetItem(fromSlot.Item);
                                                    Inventory.Items[FromSlotId] = fromSlot.Item;
                                                    Inventory.Items[ToSlotId] = draggedItem;
                                                }
                                                else
                                                {
                                                    Inventory.Items[ToSlotId] = fromSlot.Item;
                                                    Inventory.Items[FromSlotId] = null;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SwapItems(toSlot, fromSlot, draggedItem);
                                        }
                                    }
                                    //left drag
                                    else
                                    {
                                        if (fromSlot.Item.Stackable)
                                        {
                                            if (toSlot.Item != null && toSlot.Item.Stackable)
                                            {
                                                if (toSlot.Item.ItemKey == draggedItem.ItemKey)
                                                {
                                                    toSlot.Item.Count += fromSlot.Item.Count;
                                                    Inventory.Items[ToSlotId] = toSlot.Item;
                                                    Inventory.Items[FromSlotId] = null;
                                                }
                                                else
                                                {
                                                    SwapItems(toSlot, fromSlot, draggedItem);
                                                }
                                            }
                                            else
                                            {
                                                SwapItems(toSlot, fromSlot, draggedItem);
                                            }
                                        }
                                        else
                                        {
                                            SwapItems(toSlot, fromSlot, draggedItem);
                                        }
                                    }
                                }
                                
                                

                                Refresh();
                            }
                            break;
                        }
                    }
                }

                FromSlotId = -1;
                ToSlotId = -1;
                IsRightDrag = false;
            }
        }

        public void SwapItems(UIInventorySlotComponent toSlot, UIInventorySlotComponent fromSlot, Item draggedItem)
        {
            Item tempItem = toSlot.Item;
            toSlot.SetItem(draggedItem);
            fromSlot.SetItem(tempItem);
            Inventory.Items[ToSlotId] = toSlot.Item;
            Inventory.Items[FromSlotId] = tempItem;
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
