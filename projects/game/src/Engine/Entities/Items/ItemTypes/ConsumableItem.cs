using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ConsumableItem : Item
    {

        public ConsumableItem(ItemKey itemKey) : base(itemKey)
        {

        }


        public override void SetItem()
        {
            switch(ItemKey.EnumValue)
            {
                case ItemLib.Consumables.HEALTH_POTION:
                    Name = "Health Potion";
                    Description = "A health potion";
                    Value = 10;
                    Rarity = ItemRarity.COMMON;
                    break;
            }    
        }
    }
}
