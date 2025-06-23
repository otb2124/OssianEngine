using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{

    public enum ItemRarity
    {
        TRASH,
        COMMON,
        UNCOMMON,
        RARE,
        UNIQUE,
        EPIC,
        MYTHIC,
        LEGENDARY
    }

    public class Item
    {

        public Items Type;
        public int Value;
        public string Name;
        public string Description;
        public ItemRarity Rarity;

        public Item(Items type, int value, string name, string description, ItemRarity rarity)
        {
            Value = value;
            Name = name;
            Description = description;
            Rarity = rarity;
            Type = type;
        }
    }
}
