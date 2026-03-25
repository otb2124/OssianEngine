using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class Inventory
    {

        public List<Item> Items;

        public Inventory() 
        {
            Items = new List<Item>();
        }

        public Inventory(EquatableKey[] keys)
        {
            Items = new List<Item>();
            Init(keys.Length);
            foreach (EquatableKey key in keys)
            {
                AddItem(ItemFactory.CreateItem(key));
            }
        }

        public Inventory(Item[] items)
        {
            Items = new List<Item>();
            Init(items.Length);
            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public void Init(int slotsAmount)
        {
            for (int i = 0; i < slotsAmount; i++)
            {
                Items.Add(null);
            }
        }

        public void AddItem(Item item)
        {
            Item existing = GetItemWithItemKey(item.ItemKey);

            if(item.Stackable)
            {
                if (existing != null)
                {
                    existing.Count++;
                }
                else
                {
                    for (global::System.Int32 i = 0; i < Items.Count; i++)
                    {
                        if (Items[i] == null)
                        {
                            Items[i] = item;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (global::System.Int32 i = 0; i < Items.Count; i++)
                {
                    if (Items[i] == null)
                    {
                        Items[i] = item;
                        break;
                    }
                }
            }
        }

        public void RemoveItem(Item item)
        {
            if (item == null) return;

            Item existing = GetItemWithItemKey(item.ItemKey);

            if (existing == null)
                return; //Item not found in inventory

            if (item.Stackable && existing.Count > 1)
            {
                //Just decrease stack count
                existing.Count--;
            }
            else
            {
                //Remove the entire item from its slot (set to null)
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i] != null && Items[i].ItemKey == item.ItemKey)
                    {
                        Items[i] = null;
                        break;
                    }
                }
            }
        }

        public void AddInventory(Inventory inventoryToAdd)
        {
            foreach (Item item in inventoryToAdd.Items)
            {
                AddItem(item);
            }
        }


        public Item GetItemWithItemKey(EquatableKey key)
        {
            return Items?.FirstOrDefault(item => item?.ItemKey == key);
        }


        public override string ToString()
        {
            string ToArrayString = "";

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] != null)
                {
                    ToArrayString += Items[i].ItemKey;
                }
                else
                {
                    ToArrayString += "null";
                }

                if (i < Items.Count - 1)
                {
                    ToArrayString += ", ";
                }
            }

            return ToArrayString;
        }
    }
}
