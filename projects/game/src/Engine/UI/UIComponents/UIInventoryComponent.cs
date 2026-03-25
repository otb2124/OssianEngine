using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Resources;
using Myra.Graphics2D;
using static Entities.ItemLib;
using Entities;

namespace UI
{
    public class UIInventoryComponent : UIComponent
    {
        private const int Columns = 5;
        private const int SlotSize = 56;
        private const int SlotGap = 4;

        private Panel _grid;
        private List<SlotEntry> _slotEntries = new List<SlotEntry>();
        private ItemTypes _currentFilter = ItemTypes.ANY;

        public UIInventoryComponent()
        {
            SetTemplate(UITemplates.INVENTORY);
        }

        public override void Init()
        {
            _grid = UI.UIManager.UIDesktop.FindById("inventoryGrid") as Panel;

            // sort tabs
            WireTab("tabAll", ItemTypes.ANY);
            WireTab("tabWeapons", ItemTypes.WEAPON);
            WireTab("tabArmor", ItemTypes.CHESTPLATE); //armor
            WireTab("tabAccessories", ItemTypes.NECKLACE); //accessories
            WireTab("tabMaterials", ItemTypes.MATERIAL);
            WireTab("tabConsumables", ItemTypes.CONSUMABLE);
            WireTab("tabKeys", ItemTypes.KEY);
            WireTab("tabQuestItems", ItemTypes.QUEST_ITEM);
            WireTab("tabCurrencies", ItemTypes.CURRENCY);

            // close
            var btnClose = UI.UIManager.UIDesktop.FindById("btnCloseInventory") as TextButton;
            if (btnClose != null)
                btnClose.TouchUp += (s, e) => UI.UIManager.ExecuteAction("ingame.inventory");

            RefreshGrid();
            base.Init();
        }

        private void WireTab(string id, ItemTypes filter)
        {
            var btn = UI.UIManager.UIDesktop.FindById(id) as TextButton;
            if (btn == null) return;
            btn.TouchUp += (s, e) =>
            {
                _currentFilter = filter;
                RefreshGrid();
            };
        }

        public void RefreshGrid()
        {
            _grid.Widgets.Clear();
            _slotEntries.Clear();

            UI.UIManager.UIDesktop.DragDropService.UnregisterInventorySlots(this);

            var filtered = _currentFilter == ItemTypes.ANY
                ? Entities.Entities.Player.Inventory.Items
                : Entities.Entities.Player.Inventory.Items.Where(i => i.Type == _currentFilter).ToList();

            // fill slots (always show full grid, empty slots too)
            int totalSlots = System.Math.Max(filtered.Count, 25);
            int rows = (int)System.Math.Ceiling(totalSlots / (float)Columns);

            for (int i = 0; i < rows * Columns; i++)
            {
                int col = i % Columns;
                int row = i / Columns;

                var item = i < filtered.Count ? filtered[i] : null;

                var slot = new ImageButton
                {
                    Width = SlotSize,
                    Height = SlotSize,
                    Left = col * (SlotSize + SlotGap) + 4,
                    Top = row * (SlotSize + SlotGap) + 4,
                    Background = new SolidBrush(new Color(30, 30, 40, 180)),
                    OverBackground = new SolidBrush(new Color(60, 60, 80, 200)),
                    PressedBackground = new SolidBrush(new Color(20, 20, 30, 220)),
                    Border = new SolidBrush(new Color(80, 80, 100, 160)),
                    BorderThickness = new Thickness(1),
                    //Image = StaticSpriteFactory.GetItemUISprite(item)
                };

                _grid.Widgets.Add(slot);

                // register with drag drop
                var capturedItem = item;
                UI.UIManager.UIDesktop.DragDropService
                    .RegisterInventorySlot(slot, _grid, () => capturedItem);
            }

            // update grid height
            _grid.Height = rows * (SlotSize + SlotGap) + 8;
        }

        public void PushItem(Item item)
        {
            Entities.Entities.Player.Inventory.AddItem(item);
            RefreshGrid();
        }

        public void RemoveItem(Item item)
        {
            Entities.Entities.Player.Inventory.RemoveItem(item);
            RefreshGrid();
        }
    }
}