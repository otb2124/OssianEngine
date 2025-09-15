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

            //DropManager = new InventorySlotDragNDropManager(inv.Items);
        }


        public override void Update()
        {
            foreach (var item in children)
            {
                if(children != null)
                {
                    item.Update();
                }
            }
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
