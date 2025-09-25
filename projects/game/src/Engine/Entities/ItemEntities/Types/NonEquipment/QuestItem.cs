using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class QuestItem : Item
    {
        public QuestItem(ItemKey itemKey) : base(itemKey)
        {
        }


        public override void SetItem()
        {
            switch (ItemKey.EnumValue)
            {
                case ItemLib.QuestItems.NOTE:
                    Name = "Richard's Note";
                    Description = "A note from richard";
                    Value = 0;
                    Rarity = ItemRarity.COMMON;
                    Stackable = false;
                    break;

                case ItemLib.QuestItems.STONE:
                    Name = "Richard's Note";
                    Description = "A note from richard";
                    Value = 0;
                    Rarity = ItemRarity.COMMON;
                    Stackable = false;
                    break;

                case ItemLib.QuestItems.HEAD:
                    Name = "Richard's Note";
                    Description = "A note from richard";
                    Value = 0;
                    Rarity = ItemRarity.COMMON;
                    Stackable = false;
                    break;
            }
        }
    }
}
