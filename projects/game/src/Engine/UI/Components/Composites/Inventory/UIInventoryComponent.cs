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
            Position = new Vector2(pos.X, pos.Y);

            type = UIComponentTypes.INVENTORY;


            Vector2 inGameMenuSize = new Vector2(80, (64 + 12) * 6);
            Vector2 inGameMenuPos = new Vector2(0 + 10, Graphics.Graphics.screen.Height - inGameMenuSize.Y - 10);

            Vector2 frameSize = new Vector2((inGameMenuSize.X + 10 + 10 + 10 + 10 + 10)*3, Graphics.Graphics.screen.Height - (10 + 10));
            Vector2 framePos = new Vector2(inGameMenuPos.X + inGameMenuSize.X + 10, Graphics.Graphics.screen.Height - frameSize.Y - 10);

            Inventory inventory = ent.statsManager.inventory;

            children = new UIComponent[0];
            

            if (inventory.SlotsAmount > 0 )
            {
                children = new UIComponent[inventory.SlotsAmount + 1];
                children[0] = new UIFrameComponent(-1, framePos, frameSize);

                SetInventory(inventory);
            }
        }

        public void SetInventory(Inventory inventory)
        {
            int slotsCount = inventory.SlotsAmount;
            int slotsInRow = 7;
            int rowsCount = (int)Math.Ceiling((float)slotsCount / slotsInRow);

            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < slotsInRow && (row * slotsInRow + col) < slotsCount; col++)
                {
                    int index = row * slotsInRow + col + 1;
                    children[index] = new UIInventorySlotComponent(
                        -1,
                        new Vector2(
                            Position.X + (((64 * 0.75f) + 4) * col),
                            Position.Y - (((64 * 0.75f) + 4) * row)
                        )
                    );

                    if (index < inventory.Items.Count)
                    {
                        ((UIInventorySlotComponent)children[index]).SetItem(inventory.Items[index]);
                    }
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
