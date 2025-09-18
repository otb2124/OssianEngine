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
using System.Drawing;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace UI
{
    public class UIInventorySlotComponent : UIComponent
    {

        public Item Item;
        public bool IsLeftDragging;
        public bool IsRightDragging;

        public EquipmentSlot.EquipmentSlotTypes EquipmentSlotType;

        public UIInventorySlotComponent(int id, Vector2 pos, EquipmentSlot.EquipmentSlotTypes slotType = EquipmentSlot.EquipmentSlotTypes.NONE) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY_SLOT;

            children = new UIComponent[3];

            SpriteData emptySlot = new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(0, 64, 64, 64), 0);

            children[0] = new UIButtonIconComponent(-1, 15, Position, emptySlot, new Vector2(0.75f, 0.75f));
            children[1] = new UIIconComponent(-1, spriteData, Position, new Vector2(0.5f, 0.5f));
            children[2] = new UITextStringComponent(-1, Position, "", 0, Vector2.One);

            IsLeftDragging = false;
            IsRightDragging = false;

            EquipmentSlotType = slotType;
        }

        public void SetItem(Item item)
        {
            Item = item;

            if (Item != null)
            {
                if (Item.Count > 1)
                {
                    children[1] = new UIIconComponent(-1, GetItemUISprite(Item), new Vector2(Position.X, Position.Y), new Vector2(0.75f, 0.75f));
                    children[2] = new UITextStringComponent(-1, Position, Item.Count + "", 0, Vector2.One);
                }
                else
                {
                    children[2] = null;

                    if (Item.Count > 0)
                    {
                        children[1] = new UIIconComponent(-1, GetItemUISprite(Item), new Vector2(Position.X, Position.Y), new Vector2(0.75f, 0.75f));
                    }
                    else
                    {
                        children[1] = null;
                    }
                }
            }
            else
            {
                children[1] = null;
                children[2] = null;
            }
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

            //check for drag start
            if (Item != null && children[0] is UIButtonIconComponent button)
            {
                if (button.IsOnHover)
                {
                    if (Inputs.Inputs.mouse.IsLeftMouseButtonDown())
                    {
                        IsLeftDragging = true;
                        IsRightDragging = false;
                    }
                    else if (Inputs.Inputs.mouse.IsRightMouseButtonDown())
                    {
                        IsRightDragging = true;
                        IsLeftDragging = false;
                    }
                }
                else
                {
                    IsLeftDragging = false;
                    IsRightDragging = false;
                }
            }
            else
            {
                IsLeftDragging = false;
                IsRightDragging = false;
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