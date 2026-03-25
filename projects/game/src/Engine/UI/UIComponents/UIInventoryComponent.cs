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

        private SortMode _currentSortMode = SortMode.None;
        private bool _isSortReversed = false;


        private enum SortMode
        {
            None,
            ByName,      // AZ
            ByValue,     // Value / Price
            ByRarity,
            ByNewest
        }



        public UIInventoryComponent()
        {
            SetTemplate(UITemplates.INVENTORY);
        }

        public override void Init()
        {
            _grid = UI.UIManager.UIDesktop.FindById("inventoryGrid") as Panel;

            //filter tabs
            WireTab("tabAll", ItemTypes.ANY);
            WireTab("tabWeapons", ItemTypes.WEAPON);
            WireTab("tabArmor", ItemTypes.CHESTPLATE); //armor
            WireTab("tabAccessories", ItemTypes.NECKLACE); //accessories
            WireTab("tabMaterials", ItemTypes.MATERIAL);
            WireTab("tabConsumables", ItemTypes.CONSUMABLE);
            WireTab("tabKeys", ItemTypes.KEY);
            WireTab("tabQuestItems", ItemTypes.QUEST_ITEM);
            WireTab("tabCurrencies", ItemTypes.CURRENCY);

            WireSortButton("sortAZ", SortMode.ByName);
            WireSortButton("sortVal", SortMode.ByValue);
            WireSortButton("sortRarity", SortMode.ByRarity);
            WireSortButton("sortNewest", SortMode.ByNewest);

            //sort tabs

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

            // Set correct icon based on ID
            Rectangle srcRect = id switch
            {
                "tabAll" =>         new Rectangle(64 + 32 + 32 * 0, 64 + 64, 32, 32),
                "tabWeapons" =>     new Rectangle(64 + 32 + 32 * 1, 64 + 64, 32, 32),
                "tabArmor" =>       new Rectangle(64 + 32 + 32 * 2, 64 + 64, 32, 32),
                "tabAccessories" => new Rectangle(64 + 32 + 32 * 3, 64 + 64, 32, 32),
                "tabMaterials" =>   new Rectangle(64 + 32 + 32 * 4, 64 + 64, 32, 32),
                "tabConsumables" => new Rectangle(64 + 32 + 32 * 5, 64 + 64, 32, 32),
                "tabKeys" =>        new Rectangle(64 + 32 + 32 * 6, 64 + 64, 32, 32),
                "tabQuestItems" =>  new Rectangle(64 + 32 + 32 * 7, 64 + 64, 32, 32),
                "tabCurrencies" =>  new Rectangle(64 + 32 + 32 * 8, 64 + 64, 32, 32),
                _ => new Rectangle(64 + 32, 64 + 64, 32, 32)  // fallback
            };

            UI.UIManager.UIDesktop.SetButtonImage(id, new StaticSprite(SpriteSheets.UI_ICONS, srcRect));
        }

        public void RefreshGrid()
        {
            _grid.Widgets.Clear();
            _slotEntries.Clear();
            UI.UIManager.UIDesktop.DragDropService.UnregisterInventorySlots(this);

            // Start with filtered items
            var itemsToShow = Entities.Entities.Player.Inventory.Items
                .Where(i => i != null)
                .ToList();

            // Apply current filter
            if (_currentFilter != ItemTypes.ANY)
            {
                itemsToShow = itemsToShow
                    .Where(i => i.Type == _currentFilter)
                    .ToList();
            }

            // === APPLY SORTING ===
            if (_currentSortMode != SortMode.None && itemsToShow.Count > 0)
            {
                switch (_currentSortMode)
                {
                    case SortMode.ByName:
                        itemsToShow = itemsToShow
                            .OrderBy(i => i.Name)
                            .ToList();
                        break;

                    case SortMode.ByValue:
                        itemsToShow = itemsToShow
                            .OrderBy(i => i.Value)        // or i.SellPrice / whatever you use
                            .ToList();
                        break;

                    case SortMode.ByRarity:
                        itemsToShow = itemsToShow
                            .OrderBy(i => (int)i.Rarity)  // assuming you have Rarity enum
                            .ToList();
                        break;

                    case SortMode.ByNewest:
                        // TODO: implement when you have timestamps / acquisition order
                        // itemsToShow = itemsToShow.OrderByDescending(i => i.AcquiredTime).ToList();
                        break;
                }

                if (_isSortReversed)
                    itemsToShow.Reverse();
            }

            if (itemsToShow.Count == 0)
            {
                _grid.Height = 100;
                RefreshSortButtonVisuals(); // still update button visuals
                return;
            }

            // === Create slots (your existing code) ===
            int scaledSlotSize = (int)(SlotSize * UIDesktop.UIScale);
            int columns = Columns;
            int rows = (int)Math.Ceiling(itemsToShow.Count / (float)columns);

            for (int i = 0; i < itemsToShow.Count; i++)
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

                var sprite = StaticSpriteFactory.GetItemUISprite(item);
                if (sprite.SpriteSheet != SpriteSheets.NONE)
                {
                    var texture = ResourceLoader.spriteSheets[sprite.SpriteSheet].Texture;
                    slot.Image = new TextureRegion(texture, sprite.SrcRect);
                }

                _grid.Widgets.Add(slot);

                var capturedItem = item;
                UI.UIManager.UIDesktop.DragDropService.RegisterInventorySlot(slot, () => capturedItem);
            }

            _grid.Height = rows * (scaledSlotSize + SlotGap) + 8;

            // Always refresh button visuals after grid rebuild
            RefreshSortButtonVisuals();
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

        private void WireSortButton(string id, SortMode mode)
        {
            ImageButton btn = UI.UIManager.UIDesktop.FindById(id) as ImageButton;

            if (btn == null) return;

            btn.TouchUp += (s, e) =>
            {
                if (_currentSortMode == mode)
                {
                    // Cycle: Applied -> Applied_Reverse -> None
                    if (!_isSortReversed)
                    {
                        _isSortReversed = true;   // now reverse
                    }
                    else
                    {
                        _currentSortMode = SortMode.None;  // back to none
                        _isSortReversed = false;
                    }
                }
                else
                {
                    // Switch to this sort in normal direction
                    _currentSortMode = mode;
                    _isSortReversed = false;
                }

                RefreshSortButtonVisuals();
                RefreshGrid();
            };

            // Set correct icon based on ID
            Rectangle srcRect = id switch
            {
                "sortAZ" => new     Rectangle(64 * 6, 128, 16, 16),
                "sortVal" => new    Rectangle(64 * 6, 128 + 16, 16, 16),
                "sortRarity" => new Rectangle(64 * 6 + 16, 128, 16, 16),
                "sortNewest" => new Rectangle(64 * 6 + 16, 128 + 16, 16, 16),
                _ => new Rectangle(64 + 32, 64 + 64, 32, 32)  // fallback
            };

            UI.UIManager.UIDesktop.SetButtonImage(id, new StaticSprite(SpriteSheets.UI_ICONS, srcRect));
        }

        private void RefreshSortButtonVisuals()
        {
            UpdateSortButtonVisual("sortAZ", SortMode.ByName);
            UpdateSortButtonVisual("sortVal", SortMode.ByValue);
            UpdateSortButtonVisual("sortRarity", SortMode.ByRarity);
            UpdateSortButtonVisual("sortNewest", SortMode.ByNewest);
        }

        private void UpdateSortButtonVisual(string id, SortMode mode)
        {
            ImageButton btn = UI.UIManager.UIDesktop.FindById(id) as ImageButton;

            if (btn == null) return;

            if (_currentSortMode == mode)
            {
                if (_isSortReversed)
                {
                    // APPLIED_REVERSE state - e.g. red tint or different icon
                    //btn.Background = new Color(255, 100, 100, 255);        // reddish
                                                                      // Or change image if you have a "sort descending" icon:
                                                                      // btn.Image = ... descending sprite ...
                }
                else
                {
                    // APPLIED (normal) state - e.g. green/blue tint
                    //btn.Color = new Color(100, 255, 100, 255);
                }
            }
            else
            {
                // NONE state - default look
                //btn.Color = Color.White;
            }
        }


    }
}