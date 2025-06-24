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


        public void AddItem(ItemKey itemKey)
        {
            Items.Add(ItemFactory.itemMappings[itemKey]);
        }
    }
}
