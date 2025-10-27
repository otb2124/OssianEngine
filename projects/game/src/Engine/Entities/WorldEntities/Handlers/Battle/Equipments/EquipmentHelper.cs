using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.ItemLib;

namespace Entities
{
    public static class EquipmentHelper
    {


        public static EquipmentSlot.EquipmentSlotTypes HandToSlot(WeaponHands hand) =>
            hand == WeaponHands.LEFT ? EquipmentSlot.EquipmentSlotTypes.WEAPON_L : EquipmentSlot.EquipmentSlotTypes.WEAPON_R;

        public static WeaponEquipment CreateBareHands() =>
            (WeaponEquipment)ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.BARE_HAND));

        public static EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes type, EquipmentSlot[] equipmentSlots) =>
            Array.Find(equipmentSlots, slot => slot.EquipmentSlotType == type);

        public static Type ItemToType(Item item)
        {
            return item.GetType();
        }

        public static Type ItemKeyToType(EquatableKey itemKey)
        {
            Item item = ItemFactory.CreateItem(itemKey);
            return item.GetType();
        }

        public static EquipmentSlot GetEmptySlotOutOfPair(EquipmentSlot[] pair)
        {
            if (pair[0].Equipment == null)
                return pair[0];
            else
                return pair[1];
        }

        public static EquipmentSlot.EquipmentSlotTypes ItemkeyToEquipmentSlot(EquatableKey key, EquipmentSlot[] slots)
        {
            switch (ItemFactory.GetItemType(key))
            {
                case ItemTypes.WEAPON:
                    return GetEmptySlotOutOfPair(new EquipmentSlot[] { GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes.WEAPON_L, slots), GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes.WEAPON_R, slots) }).EquipmentSlotType;
                case ItemTypes.CHESTPLATE:
                    return EquipmentSlot.EquipmentSlotTypes.CHESTPLATE;
                case ItemTypes.HELMET:
                    return EquipmentSlot.EquipmentSlotTypes.HELMET;
                case ItemTypes.BOOTS:
                    return EquipmentSlot.EquipmentSlotTypes.BOOTS;
                case ItemTypes.GLOVES:
                    return EquipmentSlot.EquipmentSlotTypes.GLOVES;
                case ItemTypes.NECKLACE:
                    return EquipmentSlot.EquipmentSlotTypes.NECKLACE;
                case ItemTypes.CAPE:
                    return EquipmentSlot.EquipmentSlotTypes.CAPE;
                case ItemTypes.BELT:
                    return EquipmentSlot.EquipmentSlotTypes.BELT;
                case ItemTypes.RING:
                    return GetEmptySlotOutOfPair(new EquipmentSlot[] { GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes.RING_L, slots), GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes.RING_R, slots) }).EquipmentSlotType;
                case ItemTypes.PET:
                    return EquipmentSlot.EquipmentSlotTypes.PET;
                case ItemTypes.PET_LIGHT:
                    return EquipmentSlot.EquipmentSlotTypes.PET_LIGHT;
                case ItemTypes.CONTAINMENT:
                    return EquipmentSlot.EquipmentSlotTypes.CONTAINMENT;
                default:
                    throw new ArgumentException($"EquatableKey {key} does not correspond to an equipment slot (type: {ItemFactory.GetItemType(key)})");
            }
        }
    }
}
