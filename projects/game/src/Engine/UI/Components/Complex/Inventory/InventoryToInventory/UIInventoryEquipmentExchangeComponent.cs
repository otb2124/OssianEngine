using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIInventoryEquipmentExchangeComponent : UIComponent
    {

        public UIInventoryDragNDropService DragNDropService;

        public Inventory Inventory;
        public EquipmentManager EquipmentManager;
        public BattleBodyManager BattleBodyManager;

        public UIInventoryEquipmentExchangeComponent(int id, Vector2 pos, Inventory inv, EquipmentManager equipmentManager, BattleBodyManager battleBodyManager) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_TO_EQUIPMENT;

            children = new UIComponent[2];

            Inventory = inv;
            EquipmentManager = equipmentManager;
            BattleBodyManager = battleBodyManager;

            children[0] = new UIInventoryComponent(-1, new Vector2(100, 500), Inventory);
            children[1] = new UIInventoryComponent(-1, new Vector2(500, 500), EquipmentManager.Equipments);

            DragNDropService = new UIInventoryDragNDropService(new List<UIInventoryComponent> { (UIInventoryComponent)children[0], (UIInventoryComponent)children[1] });
        }


        public override void Update()
        {
            if (DragNDropService.WasDropPerformed)
            {
                ReflectOnModels();
            }

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null)
                {
                    if (children[i].WasRefreshedFlag)
                    {
                        DragNDropService.Refresh();
                    }

                    children[i].Update();

                    if (children[i] is UIInventoryComponent inventoryComponent)
                    {
                        if (inventoryComponent.WasSortedFlag)
                        {
                            DragNDropService.UpdateItemList(i, inventoryComponent.Items);
                        }
                    }
                }
            }


            DragNDropService.Update();
        }

        public void ReflectOnModels()
        {
            Inventory.Items = DragNDropService.InventoryLists[0].Items;

            if (children[0] is UIInventoryComponent inventoryComponent)
            {
                inventoryComponent.SortingService.UpdateOriginalItemList(DragNDropService.InventoryLists[0].Items);
                Inventory.Items = inventoryComponent.SortingService.OriginalItemList;
            }

            EquipmentManager.Equipments.EquipmentSlots = DragNDropService.InventoryLists[1].ToEquipments().EquipmentSlots;

            if (DragNDropService.WeaponChanged)
            {
                //EquipmentManager.Equipments.TogglingWeaponSlots[0].Equipment = (WeaponEquipment)DragNDropService.UIInventoryComponents[1].ToEquipments().EquipmentSlots[0].Equipment;
                //EquipmentManager.Equipments.TogglingWeaponSlots[1].Equipment = (WeaponEquipment)DragNDropService.UIInventoryComponents[1].ToEquipments().EquipmentSlots[1].Equipment;
                EquipmentManager.Equipments.TogglingWeaponSlots[0].Equipment = EquipmentHelper.CreateBareHands();
                EquipmentManager.Equipments.TogglingWeaponSlots[1].Equipment = EquipmentHelper.CreateBareHands();
                EquipmentManager.WeaponInOutToggler.LeftWeaponIn = EquipmentHelper.CreateBareHands();
                EquipmentManager.WeaponInOutToggler.RightWeaponIn = EquipmentHelper.CreateBareHands();

                if (EquipmentManager.WeaponInOutToggler.IsWeaponOut)
                {
                    EquipmentManager.ToggleWeaponInOut(BattleBodyManager);
                }

                EquipmentManager.SetWeapon(BattleBodyManager, (WeaponEquipment)DragNDropService.InventoryLists[1].ToEquipments().EquipmentSlots[0].Equipment, WeaponHands.LEFT);
                EquipmentManager.SetWeapon(BattleBodyManager, (WeaponEquipment)DragNDropService.InventoryLists[1].ToEquipments().EquipmentSlots[1].Equipment, WeaponHands.RIGHT);
            }

        }

        public override void Refresh()
        {
            foreach (var item in children)
            {
                if (children != null)
                {
                    item.Refresh();
                }
            }

            base.Refresh();
        }

        public override void Draw()
        {
            foreach (var item in children)
            {
                if (children != null)
                {
                    item.Draw();
                }
            }
        }
    }
}
