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
        private static readonly Dictionary<ItemTypes, Type> ItemTypeMap = new Dictionary<ItemTypes, Type>
        {
            { ItemTypes.WEAPON,         typeof(WeaponEquipment) },
            { ItemTypes.CHESTPLATE,     typeof(ArmorEquipment) },
            { ItemTypes.HELMET,         typeof(ArmorEquipment) },
            { ItemTypes.BOOTS,          typeof(ArmorEquipment) },
            { ItemTypes.GLOVES,         typeof(ArmorEquipment) },
            { ItemTypes.NECKLACE,       typeof(AccessoryEquipment) },
            { ItemTypes.CAPE,           typeof(AccessoryEquipment) },
            { ItemTypes.BELT,           typeof(AccessoryEquipment) },
            { ItemTypes.RING,           typeof(AccessoryEquipment) },
            { ItemTypes.PET,            typeof(AccessoryEquipment) },
            { ItemTypes.PET_LIGHT,      typeof(AccessoryEquipment) },
            { ItemTypes.CONTAINMENT,    typeof(AccessoryEquipment) },
            { ItemTypes.CONSUMABLE,     typeof(ConsumableItem) },
            { ItemTypes.MATERIAL,       typeof(MaterialItem) },
            { ItemTypes.KEY,            typeof(KeyItem) },
            { ItemTypes.QUEST_ITEM,     typeof(QuestItem) },
            { ItemTypes.CURRENCY,       typeof(CurrencyItem) },
        };

        public static Item CreateItemFromConfig(EquatableKey itemKey)
        {
            if (itemKey == null)
                throw new ArgumentNullException(nameof(itemKey));

            return Resources.ResourceLoader.ItemResources[itemKey].ToItem();
        }

        public static Item CreateItem(EquatableKey itemKey)
        {
            if (itemKey == null)
                throw new ArgumentNullException(nameof(itemKey));

            ItemTypes itemType = GetItemType(itemKey);

            if (ItemTypeMap.TryGetValue(itemType, out Type concreteType))
            {
                return (Item)Activator.CreateInstance(concreteType, itemKey);
            }
            return new Item(itemKey);
        }

        public static ItemTypes GetItemType(EquatableKey key)
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

        public static EquatableKey GetItemKeyFromString(string keyString)
        {
            if (string.IsNullOrWhiteSpace(keyString))
                throw new ArgumentException("Key string cannot be null or empty", nameof(keyString));

            var parts = keyString.Split('.');
            if (parts.Length != 2)
                throw new ArgumentException($"Invalid key format. Expected 'Type.Value', got: {keyString}");

            string enumTypeName = parts[0].Trim();
            string enumValueName = parts[1].Trim();

            // Find the enum type by name (Weapons, Belts, Necklaces, etc.)
            Type enumType = FindEnumType(enumTypeName);
            if (enumType == null)
                throw new ArgumentException($"Enum type not found: {enumTypeName}");

            // Parse the enum value
            if (!Enum.TryParse(enumType, enumValueName, true, out object enumValue))
                throw new ArgumentException($"Enum value '{enumValueName}' not found in {enumTypeName}");

            return new EquatableKey(enumValue);
        }


        private static Type FindEnumType(string typeName)
        {
            return typeName.ToLower() switch
            {
                "weapons" => typeof(Weapons),
                "chestplates" => typeof(Chestplates),
                "helmets" => typeof(Helmets),
                "boots" => typeof(Boots),
                "gloves" => typeof(Gloves),
                "necklaces" => typeof(Necklaces),
                "capes" => typeof(Capes),
                "belts" => typeof(Belts),
                "rings" => typeof(Rings),
                "pets" => typeof(Pets),
                "lightpets" => typeof(LightPets),
                "containments" => typeof(Containments),
                "consumables" => typeof(Consumables),
                "materials" => typeof(Materials),
                "keys" => typeof(Keys),
                "questitems" => typeof(QuestItems),
                "currencies" => typeof(Currencies),
                _ => null
            };
        }
    }




}
