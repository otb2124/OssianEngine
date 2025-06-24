using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class QuestItem : Item
    {
        public QuestItem(int value, string name, string description, ItemRarity rarity) : base(ItemLib.ItemTypes.QUEST_ITEM, value, name, description, rarity)
        {

        }
    }
}
