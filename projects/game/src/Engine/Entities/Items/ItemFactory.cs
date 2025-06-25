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
                case ItemTypes.ARMOR:
                    item = new ArmorEquipment(itemKey);
                    break;
                case ItemTypes.ACCESSORY:
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
                nameof(Armors) => ItemTypes.ARMOR,
                nameof(Accessories) => ItemTypes.ACCESSORY,
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
