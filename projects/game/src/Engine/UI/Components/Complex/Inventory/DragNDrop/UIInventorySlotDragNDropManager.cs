using Entities;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace UI
{
    public class UIInventorySlotDragNDropManager
    {

        public int FromSlotId;
        public int ToSlotId;
        public List<UIInventoryItemList> ItemLists;

        private List<Item> Items;

        public bool IsRightDrag;

        public List<UIComponent> Slots;

        //flags
        public bool WasRefreshedFlag;
        public bool WasDropPerformed;

        public bool WeaponChanged = false;

        public UIInventorySlotDragNDropManager(List<UIInventoryItemList> itemLists)
        {
            Refresh();

            IsRightDrag = false;

            ItemLists = new List<UIInventoryItemList>();
            ItemLists.AddRange(itemLists);

            Items = new List<Item>();
            foreach (UIInventoryItemList list in itemLists)
            {
                foreach (Item item in list.Items)
                {
                    Items.Add(item);
                }
            }

            FromSlotId = -1;
            ToSlotId = -1;

            WasRefreshedFlag = false;
            WasDropPerformed = false;

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
            FromSlotId = -1;
            ToSlotId = -1;
            IsRightDrag = false;
            WasRefreshedFlag = true;
            WasDropPerformed = true;
        }

        public void AddItems(List<Item> newItems)
        {
            Items.AddRange(newItems);
        }

        public void Update()
        {
            WasDropPerformed = false;
            WasRefreshedFlag = false;
            WeaponChanged = false;

            if (WasDropPerformed)
                return;


            //dragging slot
            if (FromSlotId == -1)
            {
                foreach (var child in Slots)
                {
                    if (child is UIInventorySlotComponent fromSlot && (fromSlot.IsLeftDragging || fromSlot.IsRightDragging))
                    {
                        Console.WriteLine("dragged");

                        FromSlotId = Slots.IndexOf(fromSlot);
                        IsRightDrag = fromSlot.IsRightDragging;

                        break;
                    }
                }
            }

            //drop
            if (FromSlotId != -1 && !Inputs.Inputs.mouse.IsLeftMouseButtonDown() && !Inputs.Inputs.mouse.IsRightMouseButtonDown())
            {
                PointF mousePos = new PointF(Inputs.Inputs.mouse.GetMouseWorldPosition().X, Inputs.Inputs.mouse.GetMouseWorldPosition().Y);
                float screenHeight = Graphics.Graphics.screen.Height;

                UIInventorySlotComponent fromSlot = (FromSlotId < Slots.Count) ? (UIInventorySlotComponent)Slots[FromSlotId] : null;
                if (fromSlot == null)
                {
                    Refresh();
                    return;
                }

                foreach (var child in Slots)
                {
                    if (child is UIInventorySlotComponent toSlot && child != fromSlot)
                    {
                        if (toSlot.children[0] is UIButtonIconComponent button && button.IsOnHover)
                        {
                            ToSlotId = Slots.IndexOf(toSlot);

                            if (Items != null && ToSlotId != FromSlotId && ToSlotId >= -1)
                            {
                                Item draggedItem = fromSlot.Item;

                                if (fromSlot.Item != null)
                                {
                                    Console.WriteLine("drop");


                                    //if equipment then check valid slot
                                    if(!IsValidEquipmentDrop(draggedItem))
                                    {
                                        Refresh();
                                        return;
                                    }


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


                                    WasDropPerformed = true;
                                    UpdateLists();
                                }



                                Refresh();
                            }
                            break;
                        }
                    }
                }

                Refresh();
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


        public bool IsValidEquipmentDrop(Item draggedItem)
        {
            int toListIndex = GetListIndexForItemId(ToSlotId);

            //if tolist is equipment
            if (ItemLists[toListIndex].UIInventoryItemListType == UIInventoryItemList.UIInventoryItemListTypes.EQUIPMENT)
            {
                if (draggedItem is Equipment eqFrom)
                {
                    int currentSlot = 0;
                    for (int i = 0; i < toListIndex; i++)
                    {
                        currentSlot += ItemLists[i].Items.Count;
                    }
                    int localToIndex = ToSlotId - currentSlot;

                    if (Equipment.EquipmentSlotTakeIntEquipmentSlotTypesMap.TryGetValue(localToIndex, out var validSlotTakes) &&
                        Array.Exists(validSlotTakes, slotTake => slotTake == eqFrom.EquipmentSlotTake))
                    {
                        if(eqFrom.EquipmentSlotTake == Equipment.EquipmentSlotsTakes.WEAPON_SINGLE || eqFrom.EquipmentSlotTake == Equipment.EquipmentSlotsTakes.WEAPON_DOUBLE)
                        {
                            WeaponChanged = true;
                        }    
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Equipment with ItemKey {eqFrom.ItemKey} and EquipmentSlotTake {eqFrom.EquipmentSlotTake} cannot be equipped in slot {localToIndex}");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Warning: Cannot equip non-equipment item to equipment slot");
                    return false;
                }
            }

            int fromListIndex = GetListIndexForItemId(FromSlotId);

            //if fromlist is equipment
            if (ItemLists[fromListIndex].UIInventoryItemListType == UIInventoryItemList.UIInventoryItemListTypes.EQUIPMENT)
            {
                WeaponChanged = true;
            }


            return true;
        }

        public int GetListIndexForItemId(int itemId)
        {
            if (itemId < 0 || itemId >= Items.Count)
            {
                return -1;
            }

            int currentSlot = 0;
            for (int i = 0; i < ItemLists.Count; i++)
            {
                int listSize = ItemLists[i].Items.Count;
                if (itemId >= currentSlot && itemId < currentSlot + listSize)
                {
                    return i;
                }
                currentSlot += listSize;
            }

            return -1;
        }


        public void UpdateLists()
        {
            int currentIndex = 0;
            for (int i = 0; i < ItemLists.Count; i++)
            {
                UIInventoryItemList itemList = ItemLists[i];
                for (int j = 0; j < itemList.Items.Count; j++)
                {
                    if (currentIndex < Items.Count)
                    {
                        itemList.Items[j] = Items[currentIndex];
                        currentIndex++;
                    }
                    else
                    {
                        itemList.Items[j] = null;
                    }
                }
            }
        }
    }
}
