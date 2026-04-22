using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DropInventory
    {


        public List<Drop> Items;

        public DropInventory()
        {
            Items = new List<Drop>();
        }

        public DropInventory(Drop[] drops)
        {
            Items = new List<Drop>();
            foreach (var drop in drops)
            {
                AddDrop(drop);
            }
        }

        public void AddDrop(Drop drop)
        {
            Items.Add(drop);
        }


        public List<Item> TryDrop()
        {
            List<Item> droppedItems = new List<Item>();

            foreach (var drop in Items)
            {
                if(drop.TryDrop())
                {
                    droppedItems.Add(drop.Item);
                }
            }

            return droppedItems;
        }

        public bool IsEmpty()
        {
            return Items.Count == 0;
        }
    }
}
