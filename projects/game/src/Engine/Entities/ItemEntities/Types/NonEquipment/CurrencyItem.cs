using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities 
{
    public class CurrencyItem : Item
    {

        public CurrencyItem(EquatableKey itemKey) : base(itemKey)
        {
        }



        public override void SetItem()
        {
            switch (ItemKey.EnumValue)
            {
                case ItemLib.Currencies.GOLD_COIN:
                    Name = "Gold Coin";
                    Description = "A gold coin";
                    Value = 1;
                    Rarity = ItemRarity.COMMON;
                    Stackable = true;
                    break;
            }
        }
    }
}
