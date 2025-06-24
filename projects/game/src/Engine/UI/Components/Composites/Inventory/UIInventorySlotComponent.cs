using Entities;
using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Resources.StaticSpriteFactory;
using Utils;

namespace UI
{
    public class UIInventorySlotComponent : UIComponent
    {

        public Item Item;

        public UIInventorySlotComponent(int id, Vector2 pos) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_SLOT;

            children = new UIComponent[2];

            SpriteData emptySlot = new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(0, 64, 64, 64), 0);

            children[0] = new UIButtonIconComponent(-1, 15, Position, emptySlot);
            children[1] = new UIIconComponent(-1, spriteData, Position, new Vector2(1, 1));
        }

        public void SetItem(Item item)
        {
            this.Item = item;
            children[1] = new UIIconComponent(-1, StaticSpriteFactory.GetItemUISprite(item), new Vector2(Position.X, Position.Y), new Vector2(1, 1));
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