using System;

namespace Entities
{

    public class Equipments
    {

        public EquipmentSlot[] EquipmentSlots;

        public Equipments()
        {
            EquipmentSlots = new EquipmentSlot[]
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
            Array.Find(EquipmentSlots, slot => slot.Type == type);

        public void SetEquipment(EquipmentSlot.EquipmentSlots slotType, Equipment item) =>
            EquipmentHelper.GetEquipmentSlot(slotType, EquipmentSlots).Equipment = item;

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
            SetEquipment(EquipmentHelper.ItemkeyToEquipmentSlot(item.ItemKey, EquipmentSlots), item);
        }

        public void SetEquipment(ItemKey itemKey)
        {
            Item item = ItemFactory.CreateItem(itemKey);
            SetEquipment(item);
        }

        public Inventory ToInventory()
        {
            Inventory inventory = new Inventory();

            inventory.Init(14); //all possible slots

            for (int i = 0; i < EquipmentSlots.Length; i++)
            {
                inventory.Items[i] = EquipmentSlots[i].Equipment;
            }

            return inventory;
        }
    }
}
