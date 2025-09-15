using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIInventoryToEquipmentComponent : UIComponent
    {

        InventorySlotDragNDropManager DropManager;

        public UIInventoryToEquipmentComponent(int id, Vector2 pos, Inventory inv, Equipments equipments) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_TO_EQUIPMENT;

            children = new UIComponent[2];

            children[0] = new UIInventoryComponent(-1, new Vector2(100, 500), inv);
            children[1] = new UIInventoryComponent(-1, new Vector2(500, 500), equipments);

            DropManager = new InventorySlotDragNDropManager(new List<List<Item>> { inv.Items, equipments.ToItemList() });

            DropManager.Slots = new List<UIComponent>();
            DropManager.AddSlots(children[0].children[0].children);
            DropManager.AddSlots(children[1].children[0].children);
        }


        public override void Update()
        {
            if (DropManager.WasRefreshedFlag)
            {
                //Refresh();
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
