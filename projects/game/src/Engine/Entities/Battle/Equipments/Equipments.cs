using System;
using System.Collections.Generic;

namespace Entities
{

    public class Equipments
    {

        public EquipmentSlot[] EquipmentSlots;
        public EquipmentSlot[] TogglingWeaponSlots;

        public Equipments()
        {
            TogglingWeaponSlots = new EquipmentSlot[]
            {
                new(EquipmentSlot.EquipmentSlotTypes.WEAPON_L),
                new(EquipmentSlot.EquipmentSlotTypes.WEAPON_R),
            };

            EquipmentSlots = new EquipmentSlot[]
            {
                new(EquipmentSlot.EquipmentSlotTypes.WEAPON_L),
                new(EquipmentSlot.EquipmentSlotTypes.WEAPON_R),
                new(EquipmentSlot.EquipmentSlotTypes.CHESTPLATE),
                new(EquipmentSlot.EquipmentSlotTypes.HELMET),
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

        public EquipmentSlot GetTogglingWeaponSlot(EquipmentSlot.EquipmentSlotTypes type) =>
            Array.Find(TogglingWeaponSlots, slot => slot.EquipmentSlotType == type);

        public void SetEquipment(EquipmentSlot.EquipmentSlotTypes slotType, Equipment item) =>
            EquipmentHelper.GetEquipmentSlot(slotType, EquipmentSlots).Equipment = item;

        public void SetTogglingWeaponEquipment(EquipmentSlot.EquipmentSlotTypes slotType, Equipment item) =>
            EquipmentHelper.GetEquipmentSlot(slotType, TogglingWeaponSlots).Equipment = item;

        public void SetEquipment(EquipmentSlot.EquipmentSlotTypes slotType, Item item)
        {
            SetEquipment(slotType, (Equipment)item);
        }

        public void SetEquipment(EquipmentSlot.EquipmentSlotTypes slotType, ItemKey itemKey)
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

        public List<Item> ToItemList()
        {
            List<Item> itemList = new List<Item>();

            for (int i = 0; i < EquipmentSlots.Length; i++)
            {
                itemList.Add(EquipmentSlots[i].Equipment);
            }

            return itemList;
        }
    }
}
