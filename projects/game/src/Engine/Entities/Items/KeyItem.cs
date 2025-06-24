using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class KeyItem : Item
    {

        public KeyItem(ItemKey itemKey) : base()
        {
            Type = ItemLib.ItemTypes.KEY;
            Value = 10;
            Name = "Key";
            Description = "desc";
            Rarity = ItemRarity.COMMON;
        }
    }
}
