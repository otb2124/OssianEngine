using Entities;
using System.Collections.Generic;

namespace UI
{
    public class UIInventoryItemListModel
    {

        public List<Item> Items;
        public UIInventoryTypes UIInventoryItemListType;
        public bool IsNonSortedInventory;


        public UIInventoryItemListModel(List<Item> itemList, UIInventoryTypes inventoryTypes)
        {
            UIInventoryItemListType = inventoryTypes;
            Items = itemList;
        }

        public UIInventoryItemListModel(Inventory inv) 
        {
            UIInventoryItemListType = UIInventoryTypes.INVENTORY;
            Items = inv.Items;
        }

        public UIInventoryItemListModel(Equipments eqs)
        {
            UIInventoryItemListType = UIInventoryTypes.EQUIPMENT;
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
