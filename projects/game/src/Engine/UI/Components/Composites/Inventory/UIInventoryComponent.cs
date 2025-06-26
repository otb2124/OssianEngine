using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UI
{
    public class UIInventoryComponent : UIComponent
    {

        public UIInventoryComponent(int id, Vector2 pos, LivingEntity ent) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY;

            Inventory inventory = ent.sManager.inventory;

            children = new UIComponent[0];

            if (inventory.SlotsAmount > 0 )
            {
                children = new UIComponent[inventory.SlotsAmount];

                SetInventory(inventory);
            }
        }

        public void SetInventory(Inventory inventory)
        {
            for (global::System.Int32 i = 0; i < inventory.SlotsAmount; i++)
            {
                children[i] = new UIInventorySlotComponent(-1, new Vector2(Position.X + 64 * i, Position.Y));

                if (inventory.Items.Count > i)
                {
                    ((UIInventorySlotComponent)children[i]).SetItem(inventory.Items[i]);
                }
            }
        }

        public override void Update()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Update();
                }
            }
        }

        public override void Draw()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Draw();
                }
            }
        }
    }
}
