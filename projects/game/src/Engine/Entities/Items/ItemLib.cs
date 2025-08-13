using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class ItemLib
    {
        public enum Weapons
        {
            BARE_HAND,
            TERRABLADE,
            KNIFE,
        }

        public enum Armors
        {
            NAKED_TORSO,
            IRON_CHESTPLATE,
            IRON_HELMET,
            IRON_BOOTS,
            IRON_GLOVES,
        }

        public enum Accessories
        {
            IRON_NECKLACE,
            LEATHER_CAPE,
            IRON_BELT,
            IRON_RING,
            CALL_DOG,
            CALL_FIREFLY,
            BACKPACK,
        }

        public enum Consumables
        {
            HEALTH_POTION,
        }

        public enum Materials
        {
            SWORD_HILT
        }

        public enum Keys
        {
            GOLDEN_KEY
        }

        public enum QuestItems
        {
            NOTE
        }

        public enum Currencies
        {
            GOLD_COIN
        }


        public enum ItemTypes
        {
            //EQUPMENT
            //WEAPON
            WEAPON,

            //ARMOR
            ARMOR,

            //ACCESSORIES
            ACCESSORY,

            //UNUEQIPABLE
            //CONSUMABLE
            CONSUMABLE,

            //MATERIAL
            MATERIAL,

            //KEY
            KEY,

            //QUEST_ITEM
            QUEST_ITEM,

            //CURRENCY
            CURRENCY,
        }
    }

    
}
