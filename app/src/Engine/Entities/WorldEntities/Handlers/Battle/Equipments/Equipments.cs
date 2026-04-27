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
            TogglingWeaponSlot = new EquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON);

            EquipmentSlots = new EquipmentSlot[]
            {

            };
        }

        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlots type) =>
            Array.Find(EquipmentSlots, slot => slot.EquipmentSlotType == type);

        public void SetEquipment(EquipmentSlot.EquipmentSlots slotType, Equipment item) =>
            EquipmentHelper.GetEquipmentSlot(slotType, EquipmentSlots).Equipment = item;

        public void SetEquipment(EquipmentSlot.EquipmentSlots slotType, Item item) =>
            SetEquipment(slotType, (Equipment)item);

        public void SetEquipment(EquipmentSlot.EquipmentSlots slotType, EquatableKey itemKey) =>
            SetEquipment(slotType, ItemFactory.CreateItemFromConfig(itemKey));

        public void SetEquipment(Item item) =>
            SetEquipment(EquipmentHelper.ItemkeyToEquipmentSlot(item.ItemKey, EquipmentSlots), item);

        public void SetEquipment(EquatableKey itemKey) =>
            SetEquipment(ItemFactory.CreateItemFromConfig(itemKey));

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