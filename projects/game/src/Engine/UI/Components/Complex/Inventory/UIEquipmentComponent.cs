using Entities;
using Microsoft.Xna.Framework;

namespace UI
{
    // Shows the fixed equipment grid.
    // Always reads directly from EquipmentManager.Equipments.EquipmentSlots — no copies.
    public class UIEquipmentComponent : UIComponent
    {
        public EquipmentManager EquipmentManager;

        private UIInventorySlotBoardComponent Board => (UIInventorySlotBoardComponent)children[0];

        public UIInventorySlotComponent ClickedSlot { get; private set; }
        public bool SlotRightClicked { get; private set; }

        public UIEquipmentComponent(int id, Vector2 pos, EquipmentManager equipmentManager) : base(id)
        {
            Position = pos;
            type = UIComponentTypes.INVENTORY;
            EquipmentManager = equipmentManager;

            children = new UIComponent[1];
            children[0] = new UIInventorySlotBoardComponent(-1, pos,
                              EquipmentManager.Equipments.ToInventory().Items,
                              new int[][] { new int[] { -1 } });
        }

        public override void Update()
        {
            ClickedSlot = null;
            SlotRightClicked = false;

            for (int i = 0; i < children.Length; i++)
                children[i]?.Update();

            foreach (UIComponent child in Board.children)
            {
                if (child is UIInventorySlotComponent slot)
                {
                    if (slot.IsRightClicked || slot.IsLeftClicked)
                    {
                        ClickedSlot = slot;
                        SlotRightClicked = slot.IsRightClicked;
                        break;
                    }
                }
            }
        }

        // Sync slot visuals from the live EquipmentSlots array
        public void RefreshBoard()
        {
            var items = EquipmentManager.Equipments.ToInventory().Items;
            for (int i = 0; i < Board.children.Length; i++)
            {
                if (Board.children[i] is UIInventorySlotComponent slot)
                {
                    int slotId = i; // children[i] corresponds to EquipmentSlots[i]
                    slot.SetItem(slotId < items.Count ? items[slotId] : null);
                }
            }
        }

        public override void Draw()
        {
            for (int i = 0; i < children.Length; i++)
                children[i]?.Draw();
        }

        public override void Refresh()
        {
            foreach (var child in children)
                child?.Refresh();
        }
    }
}