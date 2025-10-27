using Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace UI
{
    public class UIInventoryDragNDropService
    {

        public int FromSlotId;
        public int ToSlotId;

        public bool IsRightDrag;

        public List<Item> AllItems;
        public List<UIInventoryItemListModel> InventoryList;

        public List<UIComponent> AllSlots;
        public List<List<UIComponent>> SlotboardList;

        //flags
        public bool WasRefreshedFlag;
        public bool WasDropPerformed;

        public bool WeaponChanged = false;


        public UIInventoryDragNDropService(List<UIInventoryComponent> uiInventoryComponents)
        {
            Refresh();

            AllItems = new List<Item>();
            InventoryList = new List<UIInventoryItemListModel>();

            AllSlots = new List<UIComponent>();
            SlotboardList = new List<List<UIComponent>>();

            foreach (UIInventoryComponent uiInventoryComponent in uiInventoryComponents)
            {
                List<Item> paddedItems = new List<Item>(uiInventoryComponent.Items);
                while (paddedItems.Count < 40)
                {
                    paddedItems.Add(null);
                }

                foreach (Item item in paddedItems)
                {
                    AllItems.Add(item);
                }

                InventoryList.Add(new UIInventoryItemListModel(paddedItems, uiInventoryComponent.InventoryType));

                List<UIComponent> slotsToAdd = new List<UIComponent>();
                foreach (UIComponent slot in uiInventoryComponent.children[0].children)
                {
                    slotsToAdd.Add(slot);
                }
                while (slotsToAdd.Count < 40)
                {
                    slotsToAdd.Add(null);
                }

                foreach (UIComponent slot in slotsToAdd)
                {
                    AllSlots.Add(slot);
                }
                SlotboardList.Add(slotsToAdd);
            }
        }

        public void UpdateItemList(int id, List<Item> itemList)
        {
            // Pad itemList to 40 items
            List<Item> paddedItems = new List<Item>(itemList ?? new List<Item>());
            while (paddedItems.Count < 40)
            {
                paddedItems.Add(null);
            }

            // Update InventoryList
            InventoryList[id].Items = paddedItems;

            // Update AllItems
            int listStartIndex = 0;
            for (int i = 0; i < id; i++)
            {
                listStartIndex += InventoryList[i].Items.Count;
            }

            for (int j = 0; j < paddedItems.Count && listStartIndex + j < AllItems.Count; j++)
            {
                AllItems[listStartIndex + j] = paddedItems[j];
            }
        }

        public void UpdateSlots(int id, List<UIComponent> slots)
        {
            // Pad slots to 40
            List<UIComponent> paddedSlots = new List<UIComponent>(slots ?? new List<UIComponent>());
            while (paddedSlots.Count < 40)
            {
                paddedSlots.Add(null);
            }

            // Update SlotboardList
            SlotboardList[id] = paddedSlots;

            // Update AllSlots
            int listStartIndex = 0;
            for (int i = 0; i < id; i++)
            {
                listStartIndex += SlotboardList[i].Count;
            }

            for (int j = 0; j < paddedSlots.Count && listStartIndex + j < AllSlots.Count; j++)
            {
                AllSlots[listStartIndex + j] = paddedSlots[j];
            }
        }

        public void Update()
        {
            if (WasDropPerformed)
            {
                foreach (var child in AllSlots)
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
                foreach (var child in AllSlots)
                {
                    if (child is UIInventorySlotComponent fromSlot && (fromSlot.IsLeftDragging || fromSlot.IsRightDragging))
                    {
                        Console.WriteLine("dragged");

                        FromSlotId = AllSlots.IndexOf(fromSlot);
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

                UIInventorySlotComponent fromSlot = (FromSlotId < AllSlots.Count) ? (UIInventorySlotComponent)AllSlots[FromSlotId] : null;
                if (fromSlot == null)
                {
                    Refresh();
                    return;
                }

                foreach (var child in AllSlots)
                {
                    if (child is UIInventorySlotComponent toSlot && child != fromSlot)
                    {
                        if (toSlot.children[0] is UIButtonIconComponent button && button.IsOnHover)
                        {
                            ToSlotId = AllSlots.IndexOf(toSlot);

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
            if (InventoryList[toListIndex].UIInventoryItemListType == UIInventoryTypes.EQUIPMENT)
            {
                if (draggedItem is Equipment eqFrom)
                {

                    int currentSlot = 0;
                    for (int i = 0; i < toListIndex; i++)
                    {
                        currentSlot += InventoryList[i].Items.Count;
                    }
                    int localToIndex = ToSlotId - currentSlot;

                    if (Equipment.EquipmentSlotTakeEquipmentSlotTypesMap.TryGetValue(((UIInventorySlotComponent)AllSlots[ToSlotId]).EquipmentSlotType, out var validSlotTakes) &&
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
                        Console.WriteLine($"Warning: Equipment with EquatableKey {eqFrom.ItemKey} and EquipmentSlotTake {eqFrom.EquipmentSlotTake} cannot be equipped in slot {localToIndex}");
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

            Console.WriteLine(FromSlotId);

            //if fromlist is equipment
            if (InventoryList[fromListIndex].UIInventoryItemListType == UIInventoryTypes.EQUIPMENT)
            {
                Console.WriteLine("equipmentChanged");
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
            for (int i = 0; i < InventoryList.Count; i++)
            {
                int listSize = InventoryList[i].Items.Count;

                Console.WriteLine(listSize + ", " + SlotboardList[i].Count);

                if (itemId >= currentSlot && itemId < currentSlot + listSize)
                {
                    return i;
                }
                currentSlot += listSize;
            }

            return -1;
        }

        public int GetListIndexForSlotId(int slotId)
        {
            if (slotId < 0 || slotId >= AllSlots.Count)
            {
                return -1;
            }

            int currentSlot = 0;
            for (int i = 0; i < SlotboardList.Count; i++)
            {
                int listSize = SlotboardList[i].Count;

                if (slotId >= currentSlot && slotId < currentSlot + listSize)
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
            for (int i = 0; i < InventoryList.Count; i++)
            {
                UIInventoryItemListModel itemList = InventoryList[i];
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
