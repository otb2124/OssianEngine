using System;
using Utils;
using static Entities.ItemLib;

namespace Entities
{
    public static class EquipmentHelper
    {
        public static WeaponEquipment CreateBareHands() =>
            (WeaponEquipment)ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.BARE_HAND));

        public static EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlots type, EquipmentSlot[] equipmentSlots) =>
            Array.Find(equipmentSlots, slot => slot.EquipmentSlotType == type);

        public static EquipmentSlot.EquipmentSlots ItemkeyToEquipmentSlot(EquatableKey key, EquipmentSlot[] slots)
        {
            switch (ItemFactory.GetItemType(key))
            {
                case ItemTypes.WEAPON: return EquipmentSlot.EquipmentSlots.WEAPON;
                case ItemTypes.CHESTPLATE: return EquipmentSlot.EquipmentSlots.TORSO;
                case ItemTypes.HELMET: return EquipmentSlot.EquipmentSlots.HEAD;
                case ItemTypes.BOOTS: return EquipmentSlot.EquipmentSlots.LEGS;
                case ItemTypes.GLOVES: return EquipmentSlot.EquipmentSlots.HANDS;
                case ItemTypes.NECKLACE: return EquipmentSlot.EquipmentSlots.NECKLACE;
                case ItemTypes.CAPE: return EquipmentSlot.EquipmentSlots.CAPE;
                case ItemTypes.BELT: return EquipmentSlot.EquipmentSlots.BELT;
                case ItemTypes.RING:
                    // Still two ring slots — prefer L if empty, else R
                    var ringL = GetEquipmentSlot(EquipmentSlot.EquipmentSlots.RING_0, slots);
                    return (ringL?.Equipment == null)
                        ? EquipmentSlot.EquipmentSlots.RING_0
                        : EquipmentSlot.EquipmentSlots.RING_1;
                case ItemTypes.PET: return EquipmentSlot.EquipmentSlots.PET_0;
                case ItemTypes.PET_LIGHT: return EquipmentSlot.EquipmentSlots.PET_1;
                case ItemTypes.CONTAINMENT: return EquipmentSlot.EquipmentSlots.CONTAINMENT;
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