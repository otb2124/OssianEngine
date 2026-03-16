using System;
using System.Collections.Generic;
using Utils;

namespace Entities
{
    public class Equipments
    {
        public EquipmentSlot[] EquipmentSlots;

        // Single toggling slot for weapon in/out
        public EquipmentSlot TogglingWeaponSlot;

        public Equipments()
        {
            TogglingWeaponSlot = new EquipmentSlot(EquipmentSlot.EquipmentSlotTypes.WEAPON);

            EquipmentSlots = new EquipmentSlot[]
            {
                new(EquipmentSlot.EquipmentSlotTypes.WEAPON),
                new(EquipmentSlot.EquipmentSlotTypes.HELMET),
                new(EquipmentSlot.EquipmentSlotTypes.CHESTPLATE),
                new(EquipmentSlot.EquipmentSlotTypes.BOOTS),
                new(EquipmentSlot.EquipmentSlotTypes.GLOVES),
                new(EquipmentSlot.EquipmentSlotTypes.NECKLACE),
                new(EquipmentSlot.EquipmentSlotTypes.CAPE),
                new(EquipmentSlot.EquipmentSlotTypes.BELT),
                new(EquipmentSlot.EquipmentSlotTypes.RING_L),
                new(EquipmentSlot.EquipmentSlotTypes.RING_R),
                new(EquipmentSlot.EquipmentSlotTypes.PET),
                new(EquipmentSlot.EquipmentSlotTypes.PET_LIGHT),
                new(EquipmentSlot.EquipmentSlotTypes.CONTAINMENT)
            };
        }

        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes type) =>
            Array.Find(EquipmentSlots, slot => slot.EquipmentSlotType == type);

        public void SetEquipment(EquipmentSlot.EquipmentSlotTypes slotType, Equipment item) =>
            EquipmentHelper.GetEquipmentSlot(slotType, EquipmentSlots).Equipment = item;

        public void SetEquipment(EquipmentSlot.EquipmentSlotTypes slotType, Item item) =>
            SetEquipment(slotType, (Equipment)item);

        public void SetEquipment(EquipmentSlot.EquipmentSlotTypes slotType, EquatableKey itemKey) =>
            SetEquipment(slotType, ItemFactory.CreateItem(itemKey));

        public void SetEquipment(Item item) =>
            SetEquipment(EquipmentHelper.ItemkeyToEquipmentSlot(item.ItemKey, EquipmentSlots), item);

        public void SetEquipment(EquatableKey itemKey) =>
            SetEquipment(ItemFactory.CreateItem(itemKey));

        public Inventory ToInventory()
        {
            Inventory inventory = new Inventory();
            inventory.Init(EquipmentSlots.Length);
            for (int i = 0; i < EquipmentSlots.Length; i++)
                inventory.Items[i] = EquipmentSlots[i].Equipment;
            return inventory;
        }

        public List<Item> ToItemList()
        {
            var list = new List<Item>();
            foreach (var slot in EquipmentSlots)
                list.Add(slot.Equipment);
            return list;
        }
    }
}