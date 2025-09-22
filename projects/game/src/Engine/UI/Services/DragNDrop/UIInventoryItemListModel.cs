using Entities;
using System.Collections.Generic;

namespace UI
{
    public class UIInventoryItemListModel
    {
        public enum UIInventoryItemListTypes
        {
            INVENTORY,
            EQUIPMENT,
            TRADE_BUFFER
        }

        public List<Item> Items;
        public UIInventoryItemListTypes UIInventoryItemListType;

        public UIInventoryItemListModel(Inventory inv) 
        {
            UIInventoryItemListType = UIInventoryItemListTypes.INVENTORY;
            Items = inv.Items;
        }

        public UIInventoryItemListModel(Equipments eqs)
        {
            UIInventoryItemListType = UIInventoryItemListTypes.EQUIPMENT;
            Items = eqs.ToItemList();
        }


        public Inventory ToInventory()
        {
            Inventory inventory = new Inventory();

            inventory.Init(Items.Count);

            for (int i = 0; i < Items.Count; i++)
            {
                inventory.Items[i] = Items[i];
            }

            return inventory;
        }


        public Equipments ToEquipments()
        {
            Equipments equipments = new Equipments();

            for (int i = 0; i < equipments.EquipmentSlots.Length; i++)
            {
                if (Items[i] is Equipment eq)
                {
                    equipments.EquipmentSlots[i].Equipment = eq;
                }
            }

            return equipments;
        }
    }
}
