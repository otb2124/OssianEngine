using Entities;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace UI
{
    public class InventorySlotDragNDropManager
    {

        public int FromSlotId;
        public int ToSlotId;
        public List<List<Item>> ItemLists;

        private List<Item> Items;

        public bool IsRightDrag;

        public List<UIComponent> Slots;

        public bool WasRefreshedFlag;

        public InventorySlotDragNDropManager(List<List<Item>> itemLists)
        {
            Refresh();

            IsRightDrag = false;

            ItemLists = new List<List<Item>>();
            ItemLists.AddRange(itemLists);

            Items = new List<Item>();
            foreach (List<Item> list in itemLists)
            {
                foreach (Item item in list)
                {
                    Items.Add(item);
                }
            }

            FromSlotId = 0;
            ToSlotId = 0;

            WasRefreshedFlag = false;

            Slots = new List<UIComponent>();
        }

        public void AddSlots(UIComponent[] slots)
        {
            foreach (UIComponent slot in slots)
            {
                Slots.Add(slot);
            }
        }

        public void Refresh()
        {
            FromSlotId = 0;
            ToSlotId = 0;
            IsRightDrag = false;
            WasRefreshedFlag = true;
        }

        public void AddItems(List<Item> newItems)
        {
            Items.AddRange(newItems);
        }

        public void Update()
        {
            WasRefreshedFlag = false;

            //dragging slot
            if (FromSlotId == 0)
            {
                foreach (var child in Slots)
                {
                    if (child is UIInventorySlotComponent fromSlot && (fromSlot.IsLeftDragging || fromSlot.IsRightDragging))
                    {
                        Console.WriteLine("dragged");

                        FromSlotId = Slots.IndexOf(fromSlot);
                        IsRightDrag = fromSlot.IsRightDragging;

                        Console.WriteLine("fromslotId: " + FromSlotId);
                        break;
                    }
                }
            }

            //drop
            if (FromSlotId != 0 && !Inputs.Inputs.mouse.IsLeftMouseButtonDown() && !Inputs.Inputs.mouse.IsRightMouseButtonDown())
            {
                PointF mousePos = new PointF(Inputs.Inputs.mouse.GetMouseWorldPosition().X, Inputs.Inputs.mouse.GetMouseWorldPosition().Y);
                float screenHeight = Graphics.Graphics.screen.Height;

                UIInventorySlotComponent fromSlot = (FromSlotId < Slots.Count) ? (UIInventorySlotComponent)Slots[FromSlotId] : null;
                if (fromSlot == null)
                {
                    FromSlotId = 0;
                    IsRightDrag = false;
                    return;
                }

                foreach (var child in Slots)
                {
                    if (child is UIInventorySlotComponent toSlot && child != fromSlot)
                    {
                        if (toSlot.children[0] is UIButtonIconComponent button && button.IsOnHover)
                        {
                            ToSlotId = Slots.IndexOf(toSlot);

                            if (Items != null && ToSlotId != FromSlotId && ToSlotId >= 0)
                            {
                                Item draggedItem = fromSlot.Item;

                                if (fromSlot.Item != null)
                                {

                                    Console.WriteLine("drop");

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
                                                    Items[ToSlotId] = toSlot.Item;
                                                    Items[FromSlotId] = fromSlot.Item;
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
                                                    Items[FromSlotId] = fromSlot.Item;
                                                    Items[ToSlotId] = draggedItem;
                                                }
                                                else
                                                {
                                                    Items[ToSlotId] = fromSlot.Item;
                                                    Items[FromSlotId] = null;
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
                                                    Items[ToSlotId] = toSlot.Item;
                                                    Items[FromSlotId] = null;
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

                FromSlotId = 0;
                ToSlotId = 0;
                IsRightDrag = false;
            }
        }

        public void SwapItems(UIInventorySlotComponent toSlot, UIInventorySlotComponent fromSlot, Item draggedItem)
        {
            Item tempItem = toSlot.Item;
            toSlot.SetItem(draggedItem);
            fromSlot.SetItem(tempItem);
            Items[ToSlotId] = toSlot.Item;
            Items[FromSlotId] = tempItem;
        }
    }
}
