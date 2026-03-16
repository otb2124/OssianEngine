using Entities;
using Microsoft.Xna.Framework;
using Resources;
using static Resources.StaticSpriteFactory;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace UI
{
    public class UIInventorySlotComponent : UIComponent
    {
        public Item Item;
        public bool IsHovered;
        public bool IsRightClicked;
        public bool IsLeftClicked;
        public EquipmentSlot.EquipmentSlotTypes EquipmentSlotType;

        public UIInventorySlotComponent(int id, Vector2 pos,
            EquipmentSlot.EquipmentSlotTypes slotType = EquipmentSlot.EquipmentSlotTypes.NONE) : base(id)
        {
            Position = pos;
            type = UIComponentTypes.INVENTORY_SLOT;
            EquipmentSlotType = slotType;

            children = new UIComponent[3];
            children[0] = new UIButtonIconComponent(-1, 15, Position,
                new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(0, 64, 64, 64), 0),
                new Vector2(0.75f, 0.75f));
            children[1] = null;
            children[2] = null;
        }

        public void SetItem(Item item)
        {
            Item = item;
            if (item == null)
            {
                children[1] = null;
                children[2] = null;
                return;
            }
            children[1] = new UIIconComponent(-1, GetItemUISprite(item),
                new Vector2(Position.X, Position.Y), new Vector2(0.75f, 0.75f));
            children[2] = (item.Stackable && item.Count > 1)
                ? new UITextStringComponent(-1, Position, item.Count.ToString(), 0, Vector2.One, Color.White)
                : null;
        }

        public override void Update()
        {
            IsRightClicked = false;
            IsLeftClicked = false;
            IsHovered = false;

            for (int i = 0; i < children.Length; i++)
                children[i]?.Update();

            if (children[0] is UIButtonIconComponent btn && btn.IsOnHover)
            {
                IsHovered = true;
                if (Inputs.Inputs.mouse.IsRightMouseButtonPressed())
                    IsRightClicked = true;
                if (Inputs.Inputs.mouse.IsLeftMouseButtonPressed())
                    IsLeftClicked = true;
            }
        }

        public override void Draw()
        {
            for (int i = 0; i < children.Length; i++)
                children[i]?.Draw();
        }
    }
}