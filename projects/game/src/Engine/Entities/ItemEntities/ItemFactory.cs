 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Resources.StaticSpriteFactory;
using Utils;
using static Entities.ItemLib;

namespace Entities
{

    public static class ItemFactory
    {


        public static Item CreateItem(ItemKey itemKey)
        {
            Item item = new Item(GetItemType(itemKey), 0, "empty", "empty", ItemRarity.TRASH);

            switch(GetItemType(itemKey))
            {
                case ItemTypes.WEAPON:
                    item = new WeaponEquipment(itemKey);
                    break;
                case ItemTypes.CHESTPLATE:
                case ItemTypes.HELMET:
                case ItemTypes.BOOTS:
                case ItemTypes.GLOVES:
                    item = new ArmorEquipment(itemKey);
                    break;
                case ItemTypes.NECKLACE:
                case ItemTypes.CAPE:
                case ItemTypes.BELT:
                case ItemTypes.RING:
                case ItemTypes.PET:
                case ItemTypes.PET_LIGHT:
                case ItemTypes.CONTAINMENT:
                    item = new AccessoryEquipment(itemKey);
                    break;
                case ItemTypes.CONSUMABLE:
                    item = new ConsumableItem(itemKey);
                    break;
                case ItemTypes.MATERIAL:
                    item = new MaterialItem(itemKey);
                    break;
                case ItemTypes.KEY:
                    item = new KeyItem(itemKey);
                    break;
                case ItemTypes.QUEST_ITEM:
                    item = new QuestItem(itemKey);
                    break;
                case ItemTypes.CURRENCY:
                    item = new CurrencyItem(itemKey);
                    break;
            }

            return item;
        }

        public static ItemTypes GetItemType(ItemKey key)
        {
            return key.EnumType.Name switch
            {
                nameof(Weapons) => ItemTypes.WEAPON,
                nameof(Chestplates) => ItemTypes.CHESTPLATE,
                nameof(Helmets) => ItemTypes.HELMET,
                nameof(Gloves) => ItemTypes.GLOVES,
                nameof(Boots) => ItemTypes.BOOTS,
                nameof(Necklaces) => ItemTypes.NECKLACE,
                nameof(Capes) => ItemTypes.CAPE,
                nameof(Belts) => ItemTypes.BELT,
                nameof(Rings) => ItemTypes.RING,
                nameof(Pets) => ItemTypes.PET,
                nameof(LightPets) => ItemTypes.PET_LIGHT,
                nameof(Containments) => ItemTypes.CONTAINMENT,
                nameof(Consumables) => ItemTypes.CONSUMABLE,
                nameof(Materials) => ItemTypes.MATERIAL,
                nameof(Keys) => ItemTypes.KEY,
                nameof(QuestItems) => ItemTypes.QUEST_ITEM,
                nameof(Currencies) => ItemTypes.CURRENCY,
                _ => throw new ArgumentException($"Unknown enum type: {key.EnumType.Name}")
            };
        }
    }
}
