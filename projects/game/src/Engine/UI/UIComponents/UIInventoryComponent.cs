using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Resources;
using Myra.Graphics2D;
using static Entities.ItemLib;
using Entities;
using System;
using Myra.Graphics2D.TextureAtlases;
using SharpDX.Direct3D9;

namespace UI
{
    public class UIInventoryComponent : UIComponent
    {
        public static readonly int Columns = 5;
        public static readonly int SlotSize = 48;
        public static readonly int SlotGap = 2;

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
            var btn = UI.UIManager.UIDesktop.FindById(id) as ImageButton;
            if (btn == null) return;
            btn.TouchUp += (s, e) =>
            {
                _currentFilter = filter;
                RefreshGrid();
            };

            UI.UIManager.UIDesktop.SetButtonImage(id, new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64+32+ 32 * 0, 64+64, 32, 32)));
        }

        public void RefreshGrid()
        {
            int scaledSlotSize = (int)(SlotSize * UIDesktop.UIScale);

            _grid.Widgets.Clear();
            _slotEntries.Clear();
            UI.UIManager.UIDesktop.DragDropService.UnregisterInventorySlots(this);

            // Get the correct list of items
            var itemsToShow = _currentFilter == ItemTypes.ANY
                ? Entities.Entities.Player.Inventory.Items
                    .Where(i => i != null)                    // remove any null gaps if they exist
                    .ToList()
                : Entities.Entities.Player.Inventory.Items
                    .Where(i => i != null && i.Type == _currentFilter)
                    .ToList();

            if (itemsToShow.Count == 0)
            {
                // Optional: show empty inventory message or just leave grid empty
                _grid.Height = 100; // or whatever minimum height you want
                return;
            }

            int columns = Columns; // 5
            int rows = (int)Math.Ceiling(itemsToShow.Count / (float)columns);

            for (int i = 0; i < itemsToShow.Count; i++)   // ← Only create slots for actual items!
            {
                int col = i % columns;
                int row = i / columns;

                var item = itemsToShow[i];

                var slot = new ImageButton
                {
                    Width = scaledSlotSize,
                    Height = scaledSlotSize,
                    Left = col * (scaledSlotSize + SlotGap),
                    Top = row * (scaledSlotSize + SlotGap),

                    Background = new SolidBrush(new Color(30, 30, 40, 180)),
                    OverBackground = new SolidBrush(new Color(60, 60, 80, 200)),
                    PressedBackground = new SolidBrush(new Color(20, 20, 30, 220)),
                    Border = new SolidBrush(new Color(80, 80, 100, 160)),
                    BorderThickness = new Thickness(1)
                };

                // Set item image correctly using Sprite / TextureRegion
                var sprite = StaticSpriteFactory.GetItemUISprite(item);
                if (sprite.SpriteSheet != SpriteSheets.NONE)
                {
                    var texture = ResourceLoader.spriteSheets[sprite.SpriteSheet].Texture;
                    slot.Image = new TextureRegion(texture, sprite.SrcRect);
                }

                //UI.UIManager.UIDesktop.ScaleWidgets(slot);

                _grid.Widgets.Add(slot);

                // Register for drag & drop
                var capturedItem = item; // avoid closure issue
                UI.UIManager.UIDesktop.DragDropService
                    .RegisterInventorySlot(slot, () => capturedItem);
            }

            // Update grid height to fit content exactly
            _grid.Height = rows * (scaledSlotSize + SlotGap) + 8;
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