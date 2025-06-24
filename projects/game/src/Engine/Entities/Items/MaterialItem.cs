using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MaterialItem : Item
    {

        public MaterialItem(ItemKey itemKey) : base(ItemLib.ItemTypes.MATERIAL, value, name, description, rarity)
        {
            Type = ItemLib.ItemTypes.WEAPON;
            Value = 10;
            Name = "Sword";
            Description = "desc";
            Rarity = ItemRarity.COMMON;
        }
    }
}
