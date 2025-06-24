using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ConsumableItem : Item
    {

        public ConsumableItem(int value, string name, string description, ItemRarity rarity) : base(ItemLib.ItemTypes.CONSUMABLE, value, name, description, rarity)
        {

        }
    }
}
