using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MaterialItem : Item
    {

        public MaterialItem(ItemKey itemKey) : base(itemKey)
        {
            Stackable = true;
        }


        public override void SetItem()
        {
            switch (ItemKey.EnumValue)
            {
                case ItemLib.Materials.SWORD_HILT:
                    Name = "Sword Hilt";
                    Description = "A sword hilt";
                    Value = 2;
                    Rarity = ItemRarity.COMMON;
                    break;
            }
        }
    }
}
