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


        public static EquipmentSlot.EquipmentSlots HandToSlot(WeaponHands hand) =>
            hand == WeaponHands.LEFT ? EquipmentSlot.EquipmentSlots.WEAPON_L : EquipmentSlot.EquipmentSlots.WEAPON_R;

        public static WeaponEquipment CreateBareHands() =>
            (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.BARE_HAND));

        public static EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlots type, EquipmentSlot[] equipmentSlots) =>
            Array.Find(equipmentSlots, slot => slot.Type == type);

        public static Type ItemToType(Item item)
        {
            return item.GetType();
        }

        public static Type ItemKeyToType(ItemKey itemKey)
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

        public static EquipmentSlot.EquipmentSlots ItemkeyToEquipmentSlot(ItemKey key, EquipmentSlot[] slots)
        {
            switch (ItemFactory.GetItemType(key))
            {
                case ItemTypes.WEAPON:
                    return GetEmptySlotOutOfPair(new EquipmentSlot[] { GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L, slots), GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_R, slots) }).Type;
                case ItemTypes.CHESTPLATE:
                    return EquipmentSlot.EquipmentSlots.CHESTPLATE;
                case ItemTypes.HELMET:
                    return EquipmentSlot.EquipmentSlots.HELMET;
                case ItemTypes.BOOTS:
                    return EquipmentSlot.EquipmentSlots.BOOTS;
                case ItemTypes.GLOVES:
                    return EquipmentSlot.EquipmentSlots.GLOVES;
                case ItemTypes.NECKLACE:
                    return EquipmentSlot.EquipmentSlots.NECKLACE;
                case ItemTypes.CAPE:
                    return EquipmentSlot.EquipmentSlots.CAPE;
                case ItemTypes.BELT:
                    return EquipmentSlot.EquipmentSlots.BELT;
                case ItemTypes.RING:
                    return GetEmptySlotOutOfPair(new EquipmentSlot[] { GetEquipmentSlot(EquipmentSlot.EquipmentSlots.RING_L, slots), GetEquipmentSlot(EquipmentSlot.EquipmentSlots.RING_R, slots) }).Type;
                case ItemTypes.PET:
                    return EquipmentSlot.EquipmentSlots.PET;
                case ItemTypes.PET_LIGHT:
                    return EquipmentSlot.EquipmentSlots.PET_LIGHT;
                case ItemTypes.CONTAINMENT:
                    return EquipmentSlot.EquipmentSlots.CONTAINMENT;
                default:
                    throw new ArgumentException($"ItemKey {key} does not correspond to an equipment slot (type: {ItemFactory.GetItemType(key)})");
            }
        }
    }
}
