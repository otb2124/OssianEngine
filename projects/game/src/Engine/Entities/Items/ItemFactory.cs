 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Resources.StaticSpriteFactory;
using Utils;

namespace Entities
{

    public enum Items
    {
        //EQUPMENT
            //WEAPON
            SWORD,

            //ARMOR
            CHESTPLATE,
            HELMET,
            BOOTS,
            GLOVES,

            //ACCESSORIES
            //NECKLACE
            NECKLACE,
            //BELT
            BELT,
            //RING
            RING,

            //PET CALLARS
            CALL_DOG,
            CALL_FIREFLY,

            //CONTAINMENT
            BACKPACK,

        //UNUEQIPABLE
            //CONSUMABLE
            HEALTH_POTION,

            //MATERIAL
            SWORD_HILT,

            //KEY
            GOLDEN_KEY,

            //QUEST_ITEM
            NOTE,

            //CURRENCY
            COIN,
    }

    public static class ItemFactory
    {

        public static readonly Dictionary<Items, Item> itemMappings = new()
        {
            { Items.SWORD,          new Item(Items.SWORD, 400, "Terrablade", "A sword", ItemRarity.LEGENDARY) },
            { Items.CHESTPLATE,     new Item(Items.CHESTPLATE, 500, "Chestplate", "desc", ItemRarity.EPIC) },
            { Items.HELMET,         new Item(Items.HELMET, 300, "Helmet", "desc", ItemRarity.COMMON) },
            { Items.BOOTS,          new Item(Items.BOOTS, 200, "Boots", "desc", ItemRarity.COMMON) },
            { Items.GLOVES,         new Item(Items.GLOVES, 100, "Gloves", "desc", ItemRarity.COMMON) },
            { Items.NECKLACE,       new Item(Items.NECKLACE,1000, "Necklace", "desc", ItemRarity.COMMON) },
            { Items.BELT,           new Item(Items.BELT, 900, "Belt", "desc", ItemRarity.COMMON) },
            { Items.RING,           new Item(Items.RING, 800, "Ring", "desc", ItemRarity.COMMON) },
            { Items.CALL_DOG,       new Item(Items.CALL_DOG, 5000, "Bone", "desc", ItemRarity.COMMON) },
            { Items.CALL_FIREFLY,   new Item(Items.CALL_FIREFLY, 10000, "Firefly in a Jar", "desc", ItemRarity.COMMON) },
            { Items.BACKPACK,       new Item(Items.BACKPACK, 1000, "Backpack", "desc", ItemRarity.COMMON) },
            { Items.HEALTH_POTION,  new Item(Items.HEALTH_POTION, 50, "Health Potion", "desc", ItemRarity.COMMON) },
            { Items.SWORD_HILT,     new Item(Items.SWORD_HILT, 50, "Sword Hilt", "desc", ItemRarity.COMMON) },
            { Items.GOLDEN_KEY,     new Item(Items.GOLDEN_KEY, 0, "Golden Key", "desc", ItemRarity.COMMON) },
            { Items.NOTE,           new Item(Items.NOTE, -1, "Richard's Note", "desc", ItemRarity.COMMON) },
            { Items.COIN,           new Item(Items.COIN, 1, "Gold Coin", "A gold coin", ItemRarity.COMMON) },
        };
    }
}
