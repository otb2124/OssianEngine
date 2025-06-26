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
            foreach (var key in keys)
            {
                AddItem(ItemFactory.CreateItem(key));
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
                    Items.Add(item);
                }
            }
            else
            {
                Items.Add(item);
            }
        }

        public void AddInventory(Inventory inventory)
        {
            foreach (var item in inventory.Items)
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
