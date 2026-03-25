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
            TORCH,

            FIREBALL_SPELL,

            BOW
        }

        public enum Chestplates
        {
            NAKED_TORSO,
            IRON_CHESTPLATE,
        }

        public enum Helmets
        {
            IRON_HELMET,
        }

        public enum Boots
        {
            IRON_BOOTS,
        }

        public enum Gloves
        {
            IRON_GLOVES,
        }

        public enum Necklaces
        {
            IRON_NECKLACE,
        }

        public enum Capes
        {
            LEATHER_CAPE,
        }

        public enum Belts
        {
            IRON_BELT,
        }

        public enum Rings
        {
            IRON_RING,
        }

        public enum Pets
        {
            CALL_DOG,
        }

        public enum LightPets
        {
            CALL_FIREFLY,
        }

        public enum Containments
        {
            BACKPACK,
        }

        public enum Consumables
        {
            HEALTH_POTION,
            CAKE,
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
            NOTE,
            STONE,
            HEAD,
        }

        public enum Currencies
        {
            GOLD_COIN
        }

        public enum ItemTypes
        {
            ANY,
            //EQUPMENT
            //WEAPON
            WEAPON,

            //ARMOR
            CHESTPLATE,
            HELMET,
            GLOVES,
            BOOTS,

            //ACCESSORIES
            NECKLACE,
            CAPE,
            BELT,
            RING,
            PET,
            PET_LIGHT,
            CONTAINMENT,

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
