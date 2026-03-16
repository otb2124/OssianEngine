using System;
using Utils;
using static Entities.ItemLib;

namespace Entities
{
    public static class EquipmentHelper
    {
        public static WeaponEquipment CreateBareHands() =>
            (WeaponEquipment)ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.BARE_HAND));

        public static EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes type, EquipmentSlot[] equipmentSlots) =>
            Array.Find(equipmentSlots, slot => slot.EquipmentSlotType == type);

        public static EquipmentSlot.EquipmentSlotTypes ItemkeyToEquipmentSlot(EquatableKey key, EquipmentSlot[] slots)
        {
            switch (ItemFactory.GetItemType(key))
            {
                case ItemTypes.WEAPON: return EquipmentSlot.EquipmentSlotTypes.WEAPON;
                case ItemTypes.CHESTPLATE: return EquipmentSlot.EquipmentSlotTypes.CHESTPLATE;
                case ItemTypes.HELMET: return EquipmentSlot.EquipmentSlotTypes.HELMET;
                case ItemTypes.BOOTS: return EquipmentSlot.EquipmentSlotTypes.BOOTS;
                case ItemTypes.GLOVES: return EquipmentSlot.EquipmentSlotTypes.GLOVES;
                case ItemTypes.NECKLACE: return EquipmentSlot.EquipmentSlotTypes.NECKLACE;
                case ItemTypes.CAPE: return EquipmentSlot.EquipmentSlotTypes.CAPE;
                case ItemTypes.BELT: return EquipmentSlot.EquipmentSlotTypes.BELT;
                case ItemTypes.RING:
                    // Still two ring slots — prefer L if empty, else R
                    var ringL = GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes.RING_L, slots);
                    return (ringL?.Equipment == null)
                        ? EquipmentSlot.EquipmentSlotTypes.RING_L
                        : EquipmentSlot.EquipmentSlotTypes.RING_R;
                case ItemTypes.PET: return EquipmentSlot.EquipmentSlotTypes.PET;
                case ItemTypes.PET_LIGHT: return EquipmentSlot.EquipmentSlotTypes.PET_LIGHT;
                case ItemTypes.CONTAINMENT: return EquipmentSlot.EquipmentSlotTypes.CONTAINMENT;
                default:
                    throw new ArgumentException(
                        $"Key {key} does not map to an equipment slot (type: {ItemFactory.GetItemType(key)})");
            }
        }

        public static Type ItemToType(Item item) => item.GetType();

        public static Type ItemKeyToType(EquatableKey itemKey) =>
            ItemFactory.CreateItem(itemKey).GetType();
    }
}