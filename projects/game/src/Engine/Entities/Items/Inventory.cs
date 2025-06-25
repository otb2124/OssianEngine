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
                Items.Add(ItemFactory.CreateItem(key));
            }
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void AddInventory(Inventory inventory)
        {
            foreach (var item in inventory.Items)
            {
                AddItem(item);
            }
        }
    }
}
