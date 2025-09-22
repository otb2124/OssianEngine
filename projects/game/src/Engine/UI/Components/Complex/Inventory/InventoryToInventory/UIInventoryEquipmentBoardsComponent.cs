using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIInventoryEquipmentBoardsComponent : UIComponent
    {

        public UIInventorySlotDragNDropService DropManager;

        public Inventory Inventory;
        public EquipmentManager EquipmentManager;
        public BattleBodyManager BattleBodyManager;

        public UIInventoryEquipmentBoardsComponent(int id, Vector2 pos, Inventory inv, EquipmentManager equipmentManager, BattleBodyManager battleBodyManager) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_TO_EQUIPMENT;

            children = new UIComponent[2];

            Inventory = inv;
            EquipmentManager = equipmentManager;
            BattleBodyManager = battleBodyManager;

            children[0] = new UIInventoryComponent(-1, new Vector2(100, 500), Inventory);
            children[1] = new UIInventoryComponent(-1, new Vector2(500, 500), EquipmentManager.Equipments);

            DropManager = new UIInventorySlotDragNDropService(new List<UIInventoryItemListModel> 
            {
                new UIInventoryItemListModel(Inventory), 
                new UIInventoryItemListModel(EquipmentManager.Equipments) 
            });

            DropManager.Slots = new List<UIComponent>();
            DropManager.AddSlots(children[0].children[0].children);
            DropManager.AddSlots(children[1].children[0].children);
        }


        public override void Update()
        {
            if (DropManager.WasDropPerformed)
            {
                Inventory.Items = DropManager.ItemLists[0].ToInventory().Items;
                EquipmentManager.Equipments.EquipmentSlots = DropManager.ItemLists[1].ToEquipments().EquipmentSlots;

                if(DropManager.WeaponChanged)
                {
                    //EquipmentManager.Equipments.TogglingWeaponSlots[0].Equipment = (WeaponEquipment)DropManager.ItemLists[1].ToEquipments().EquipmentSlots[0].Equipment;
                    //EquipmentManager.Equipments.TogglingWeaponSlots[1].Equipment = (WeaponEquipment)DropManager.ItemLists[1].ToEquipments().EquipmentSlots[1].Equipment;
                    EquipmentManager.Equipments.TogglingWeaponSlots[0].Equipment = EquipmentHelper.CreateBareHands();
                    EquipmentManager.Equipments.TogglingWeaponSlots[1].Equipment = EquipmentHelper.CreateBareHands();
                    EquipmentManager.WeaponInOutToggler.LeftWeaponIn = EquipmentHelper.CreateBareHands();
                    EquipmentManager.WeaponInOutToggler.RightWeaponIn = EquipmentHelper.CreateBareHands();

                    if(EquipmentManager.WeaponInOutToggler.IsWeaponOut)
                    {
                        EquipmentManager.ToggleWeaponInOut(BattleBodyManager);
                    }

                    EquipmentManager.SetWeapon(BattleBodyManager, (WeaponEquipment)DropManager.ItemLists[1].ToEquipments().EquipmentSlots[0].Equipment, WeaponHands.LEFT);
                    EquipmentManager.SetWeapon(BattleBodyManager, (WeaponEquipment)DropManager.ItemLists[1].ToEquipments().EquipmentSlots[1].Equipment, WeaponHands.RIGHT);
                    
                    Console.WriteLine("weapon change");
                }
                
                Console.WriteLine("swap");
            }

            foreach (var item in children)
            {
                if(children != null)
                {
                    if(item.WasRefreshedFlag)
                    {
                        DropManager.Refresh();
                    }

                    item.Update();
                }
            }

            DropManager.Update();
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
