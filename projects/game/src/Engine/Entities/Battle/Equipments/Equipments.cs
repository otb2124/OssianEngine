using System;

namespace Entities
{

    public class Equipments
    {

        public EquipmentSlot[] Slots;

        public Equipments()
        {
            Slots = new EquipmentSlot[]
            {
                new(EquipmentSlot.EquipmentSlots.WEAPON_L),
                new(EquipmentSlot.EquipmentSlots.WEAPON_R),
                new(EquipmentSlot.EquipmentSlots.CHESTPLATE),
                new(EquipmentSlot.EquipmentSlots.HELMET),
                new(EquipmentSlot.EquipmentSlots.BOOTS),
                new(EquipmentSlot.EquipmentSlots.GLOVES),
                new(EquipmentSlot.EquipmentSlots.NECKLACE),
                new(EquipmentSlot.EquipmentSlots.CAPE),
                new(EquipmentSlot.EquipmentSlots.BELT),
                new(EquipmentSlot.EquipmentSlots.RING_L),
                new(EquipmentSlot.EquipmentSlots.RING_R),
                new(EquipmentSlot.EquipmentSlots.PET),
                new(EquipmentSlot.EquipmentSlots.PET_LIGHT),
                new(EquipmentSlot.EquipmentSlots.CONTAINMENT)
            };
        }


        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlots type) =>
            Array.Find(Slots, slot => slot.Type == type);

        public void SetEquipment(EquipmentSlot.EquipmentSlots slotType, Equipment item) =>
            GetEquipmentSlot(slotType).Equipment = item;

        public void SetEquipment(EquipmentSlot.EquipmentSlots slotType, Item item)
        {
            SetEquipment(slotType, (Equipment)item);
        }

        public void SetEquipment(EquipmentSlot.EquipmentSlots slotType, ItemKey itemKey)
        {
            Item item = ItemFactory.CreateItem(itemKey);
            SetEquipment(slotType, item);
        }

        public void SetEquipment(Item item)
        {
            SetEquipment(EquipmentHelper.ItemkeyToEquipmentSlot(item.ItemKey), item);
        }

        public void SetEquipment(ItemKey itemKey)
        {
            Item item = ItemFactory.CreateItem(itemKey);
            SetEquipment(item);
        }

        public ArmorEquipment GetCurrentArmor() =>
            (ArmorEquipment)GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment;
    }
}
