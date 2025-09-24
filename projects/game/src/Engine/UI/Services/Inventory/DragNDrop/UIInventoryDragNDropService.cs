using Entities;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace UI
{
    public class UIInventoryDragNDropService
    {

        public int FromSlotId;
        public int ToSlotId;

        public bool IsRightDrag;

        public List<Item> AllItems;
        public List<UIInventoryItemListModel> InventoryLists;

        public List<UIComponent> Slots;

        //flags
        public bool WasRefreshedFlag;
        public bool WasDropPerformed;

        public bool WeaponChanged = false;

        public UIInventoryDragNDropService(List<UIInventoryComponent> uiInventoryComponents)
        {
            Refresh();

            AllItems = new List<Item>();
            InventoryLists = new List<UIInventoryItemListModel>();
            Slots = new List<UIComponent>();

            foreach (UIInventoryComponent uiInventoryComponent in uiInventoryComponents)
            {
                foreach (Item item in uiInventoryComponent.Items)
                {
                    AllItems.Add(item);
                }

                InventoryLists.Add(new UIInventoryItemListModel(uiInventoryComponent.Items, uiInventoryComponent.InventoryType));
                Slots.AddRange(uiInventoryComponent.children[0].children);
            }
        }

        public void Update()
        {
            if (WasDropPerformed)
            {
                foreach (var child in Slots)
                {
                    if (child is UIInventorySlotComponent fromSlot && (fromSlot.IsLeftDragging || fromSlot.IsRightDragging))
                    {
                        fromSlot.IsLeftDragging = false;
                        fromSlot.IsRightDragging = false;
                    }
                }
            }

            WasRefreshedFlag = false;
            WasDropPerformed = false;
            WeaponChanged = false;
            


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

                            if (AllItems != null && ToSlotId != FromSlotId && ToSlotId >= -1)
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
                                                    AllItems[ToSlotId] = toSlot.Item;
                                                    AllItems[FromSlotId] = fromSlot.Item;
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
                                                    AllItems[FromSlotId] = fromSlot.Item;
                                                    AllItems[ToSlotId] = draggedItem;
                                                }
                                                else
                                                {
                                                    AllItems[ToSlotId] = fromSlot.Item;
                                                    AllItems[FromSlotId] = null;
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
                                                    AllItems[ToSlotId] = toSlot.Item;
                                                    AllItems[FromSlotId] = null;
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
            AllItems[ToSlotId] = toSlot.Item;
            AllItems[FromSlotId] = tempItem;
        }


        public bool IsValidEquipmentDrop(Item draggedItem)
        {
            int toListIndex = GetListIndexForItemId(ToSlotId);

            //if tolist is equipment
            if (InventoryLists[toListIndex].UIInventoryItemListType == UIInventoryTypes.EQUIPMENT)
            {
                if (draggedItem is Equipment eqFrom)
                {

                    int currentSlot = 0;
                    for (int i = 0; i < toListIndex; i++)
                    {
                        currentSlot += InventoryLists[i].Items.Count;
                    }
                    int localToIndex = ToSlotId - currentSlot;

                    if (Equipment.EquipmentSlotTakeEquipmentSlotTypesMap.TryGetValue(((UIInventorySlotComponent)Slots[ToSlotId]).EquipmentSlotType, out var validSlotTakes) &&
                        Array.Exists(validSlotTakes, slotTake => slotTake == eqFrom.EquipmentSlotTake))
                    {
                        if (eqFrom.EquipmentSlotTake == Equipment.EquipmentSlotsTakes.WEAPON_SINGLE || eqFrom.EquipmentSlotTake == Equipment.EquipmentSlotsTakes.WEAPON_DOUBLE)
                        {
                            Console.WriteLine("weaponChanged");
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
            if (InventoryLists[fromListIndex].UIInventoryItemListType == UIInventoryTypes.EQUIPMENT)
            {
                Console.WriteLine("weaponChanged");
                WeaponChanged = true;
            }


            return true;
        }

        public int GetListIndexForItemId(int itemId)
        {
            if (itemId < 0 || itemId >= AllItems.Count)
            {
                return -1;
            }

            int currentSlot = 0;
            for (int i = 0; i < InventoryLists.Count; i++)
            {
                int listSize = InventoryLists[i].Items.Count;
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
            for (int i = 0; i < InventoryLists.Count; i++)
            {
                UIInventoryItemListModel itemList = InventoryLists[i];
                for (int j = 0; j < itemList.Items.Count; j++)
                {
                    if (currentIndex < AllItems.Count)
                    {
                        itemList.Items[j] = AllItems[currentIndex];
                        currentIndex++;
                    }
                    else
                    {
                        itemList.Items[j] = null;
                    }
                }
            }
        }



        public UIInventoryItemListModel GetItemList(int listId)
        {
            return InventoryLists[listId];
        }


        public void Refresh()
        {
            FromSlotId = -1;
            ToSlotId = -1;
            IsRightDrag = false;
            WasRefreshedFlag = true;
            WasDropPerformed = true;
        }
    }
}
