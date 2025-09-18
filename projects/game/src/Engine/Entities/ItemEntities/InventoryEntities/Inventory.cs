using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Inventory
    {

        public List<Item> Items;
        public int SlotsAmount;

        public Inventory() 
        {
            Items = new List<Item>();
        }

        public Inventory(ItemKey[] keys)
        {
            Items = new List<Item>();
            Init(keys.Length);
            foreach (ItemKey key in keys)
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
            SlotsAmount = slotsAmount;

            for (int i = 0; i < SlotsAmount; i++)
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

        public void AddInventory(Inventory inventoryToAdd)
        {
            foreach (Item item in inventoryToAdd.Items)
            {
                AddItem(item);
            }
        }


        public Item GetItemWithItemKey(ItemKey key)
        {
            return Items?.FirstOrDefault(item => item?.ItemKey == key);
        }
    }
}
