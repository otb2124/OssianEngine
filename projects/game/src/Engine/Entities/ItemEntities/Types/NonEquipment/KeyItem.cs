using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class KeyItem : Item
    {

        public KeyItem(EquatableKey itemKey) : base(itemKey)
        {
        }



        public override void SetItem()
        {
            switch (ItemKey.EnumValue)
            {
                case ItemLib.Keys.GOLDEN_KEY:
                    Name = "Golden Key";
                    Description = "A golden key";
                    Value = 1;
                    Rarity = ItemRarity.COMMON;
                    Stackable = true;
                    break;
            }
        }
    }
}
