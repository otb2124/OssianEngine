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

        public UIInventoryComponent(int id, Vector2 pos, StatsEntity ent) : base(id)
        {
            Position = new Vector2(pos.X, pos.Y);

            type = UIComponentTypes.INVENTORY;

            children = new UIComponent[2];
            children[0] = new UIInventorySlotBoardComponent(id, pos, ent);
        }

        public override void Update()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    if(children[i] != null)
                    {
                        children[i].Update();
                    }
                }
            }
        }

        public override void Draw()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i] != null)
                    {
                        children[i].Draw();
                    }
                }
            }
        }
    }
}
