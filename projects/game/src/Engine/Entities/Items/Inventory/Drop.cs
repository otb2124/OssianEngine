using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class Drop
    {

        public float DropChance;
        public Item Item;

        public Drop(ItemKey itemKey, float dropChance)
        {
            DropChance = dropChance;
            Item = ItemFactory.CreateItem(itemKey);
        }

        public bool TryDrop()
        {
            float random = RandomHelper.RandomFloating(0, 1);
            return random <= DropChance;
        }
    }
}
