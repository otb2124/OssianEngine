using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Entities.EquipmentSlot;

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
        public EquatableKey ItemKey;
        public ItemLib.ItemTypes Type;
        public int Value;
        public string Name;
        public string Description;
        public ItemRarity Rarity;
        public bool Stackable = false;
        public int Count = 1;

        public Item(ItemLib.ItemTypes type, int value, string name, string description, ItemRarity rarity)
        {
            Value = value;
            Name = name;
            Description = description;
            Rarity = rarity;
            Type = type;
        }

        public Item(EquatableKey itemKey)
        {
            ItemKey = itemKey;
            Type = ItemFactory.GetItemType(itemKey);

            SetItem();
        }

        public virtual void SetItem()
        {
            
        }

        public bool CanEquipTo(EquipmentSlots slot)
        {
            return true;
        }
    }
}
