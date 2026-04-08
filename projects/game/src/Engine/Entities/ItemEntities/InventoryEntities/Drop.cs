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

        public Drop(EquatableKey itemKey, float dropChance)
        {
            DropChance = dropChance;
            Item = ItemFactory.CreateItemFromConfig(itemKey);
        }

        public bool TryDrop()
        {
            float random = RandomHelper.RandomFloating(0, 1);
            return random <= DropChance;
        }
    }
}
