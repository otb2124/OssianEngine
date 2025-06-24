using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities 
{
    public class CurrencyItem : Item
    {

        public CurrencyItem(int value, string name, string description, ItemRarity rarity) : base(ItemLib.ItemTypes.CURRENCY, value, name, description, rarity)
        {

        }
    }
}
