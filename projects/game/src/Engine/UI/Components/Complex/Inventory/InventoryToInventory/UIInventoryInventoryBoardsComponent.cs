using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIInventoryInventoryBoardsComponent : UIComponent
    {

        public UIInventoryDragNDropService DropManager;

        public Inventory Inventory0;
        public Inventory Inventory1;

        public UIInventoryInventoryBoardsComponent(int id, Vector2 pos, Inventory inv0, Inventory inv1) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_TO_INVENTORY;

            children = new UIComponent[2];

            Inventory0 = inv0;
            Inventory1 = inv1;

            children[0] = new UIInventoryComponent(-1, new Vector2(100, 500), Inventory0);
            children[1] = new UIInventoryComponent(-1, new Vector2(500, 500), Inventory1);

            DropManager = new UIInventoryDragNDropService(new List<UIInventoryComponent> { (UIInventoryComponent)children[0], (UIInventoryComponent)children[1] });
        }


        public override void Update()
        {
            if (DropManager.WasDropPerformed)
            {
                Inventory0.Items = DropManager.InventoryList[0].ToInventory().Items;
                Inventory1.Items = DropManager.InventoryList[1].ToInventory().Items;
            }

            foreach (var item in children)
            {
                if (children != null)
                {
                    if (item.WasRefreshedFlag)
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


        //to move to another trade menu component
        public override void Destroy()
        {
        }
    }
}
