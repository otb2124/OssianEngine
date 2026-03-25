using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Entities;
using static Entities.EquipmentSlot;
using Myra.Graphics2D;

namespace UI
{
    public enum SlotOwner { Inventory, Equipment }

    public class SlotEntry
    {
        public ImageButton Widget;
        public SlotOwner Owner;
        public EquipmentSlots EquipSlot;   // set if Owner == Equipment
        public Item Item;
        public int OriginalLeft;
        public int OriginalTop;
        public Panel RootPanel;
    }

    public class UIDragDropService
    {
        private SlotEntry _dragging;
        private List<SlotEntry> _slots = new List<SlotEntry>();

        // callbacks
        public Action<SlotEntry, SlotEntry> OnSwapInventory;   // inv <-> inv
        public Action<SlotEntry, SlotEntry> OnEquip;           // inv -> equip
        public Action<SlotEntry, SlotEntry> OnUnequip;         // equip -> inv

        private ImageButton _dragGhost;

        private Panel _tooltip;
        private Label _tooltipName;
        private Label _tooltipDesc;
        private Label _tooltipType;


        public UIDragDropService()
        {
            InitializeTooltip();
            InitializeGlobalHandlers();
        }

        public void RegisterInventorySlot(ImageButton widget, Panel rootPanel, Func<Item> getItem)
        {
            var entry = new SlotEntry
            {
                Widget = widget,
                Owner = SlotOwner.Inventory,
                RootPanel = rootPanel
            };
            _slots.Add(entry);
            WireEvents(entry, getItem);
        }

        public void RegisterEquipmentSlot(ImageButton widget, Panel rootPanel, EquipmentSlots slot)
        {
            var entry = new SlotEntry
            {
                Widget = widget,
                Owner = SlotOwner.Equipment,
                EquipSlot = slot,
                RootPanel = rootPanel
            };
            _slots.Add(entry);
            WireEvents(entry, () => entry.Item);
        }

        public void UnregisterAll()
        {
            _slots.Clear();
        }

        private void WireEvents(SlotEntry entry, Func<Item> getItem)
        {
            var widget = entry.Widget;

            widget.TouchDown += (s, e) =>
            {
                entry.Item = getItem();
                if (entry.Item == null) return;

                HideTooltip();

                _dragging = entry;
                _dragging.OriginalLeft = (int)widget.Left;
                _dragging.OriginalTop = (int)widget.Top;

                // Create ghost
                _dragGhost = new ImageButton
                {
                    Width = widget.Width,
                    Height = widget.Height,
                    Background = null,
                    Image = widget.Image,
                    Opacity = 0.75f,
                    ZIndex = 10000
                };

                // Add ghost to top level
                UI.UIManager.UIDesktop.Desktop.Widgets.Add(_dragGhost);

                var absPos = UIDesktop.GetAbsolutePosition(widget);
                _dragGhost.Left = absPos.X;
                _dragGhost.Top = absPos.Y;

                widget.Opacity = 0.7f;   // dim original slot
            };

            widget.MouseEntered += (s, e) =>
            {
                if (_dragging != null) return; // don't show tooltip while dragging
                var item = getItem();
                if (item == null) return;
                var mousePos = Inputs.Inputs.mouse.GetMouseScreenPosition();
                var scaledPos = new Point(
                    (int)(mousePos.X * UIDesktop.UIScale),
                    (int)(mousePos.Y * UIDesktop.UIScale)
                );
                ShowTooltip(item, scaledPos);
            };

            widget.MouseLeft += (s, e) =>
            {
                HideTooltip();
            };
        }

        public void InitializeGlobalHandlers()
        {
            // One-time setup - call this once after creating UIDragDropService
            UI.UIManager.UIDesktop.Desktop.TouchUp += OnDesktopTouchUp;
        }

        private void OnDesktopTouchUp(object sender, EventArgs e)
        {
            if (_dragging == null || _dragGhost == null) return;

            HideTooltip();

            // Get final mouse position
            var finalPos = new Vector2(Inputs.Inputs.mouse.GetMouseScreenPosition().X * UIDesktop.UIScale, Inputs.Inputs.mouse.GetMouseScreenPosition().Y * UIDesktop.UIScale).ToPoint();
            var target = FindSlotUnder(finalPos);

            // Clean up ghost
            UI.UIManager.UIDesktop.Desktop.Widgets.Remove(_dragGhost);
            _dragGhost = null;

            // Restore original slot opacity
            if (_dragging.Widget != null)
                _dragging.Widget.Opacity = 1f;

            if (target != null && target != _dragging)
            {
                HandleDrop(_dragging, target);
            }
            else
            {
                SnapBack(_dragging);
            }

            _dragging = null;
        }

        public void Update()
        {
            var mousePos = Inputs.Inputs.mouse.GetMouseScreenPosition();
            var scaledPos = new Point(
                (int)(mousePos.X * UIDesktop.UIScale),
                (int)(mousePos.Y * UIDesktop.UIScale)
            );

            if (_dragGhost != null && _dragging != null)
            {
                _dragGhost.Left = (int)(scaledPos.X - _dragGhost.Width / 2);
                _dragGhost.Top = (int)(scaledPos.Y - _dragGhost.Height / 2);
            }

            UpdateTooltipPosition(scaledPos);
        }

        private void HandleDrop(SlotEntry from, SlotEntry to)
        {
            Console.WriteLine(to.EquipSlot);
            // inventory -> equipment
            if (from.Owner == SlotOwner.Inventory && to.Owner == SlotOwner.Equipment)
            {
                if (from.Item == null || !from.Item.CanEquipTo(to.EquipSlot))
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Cannot equip {from.Item?.Name} to {to.EquipSlot}");
                    SnapBack(from);
                    return;
                }
                SwapImages(from, to);
                OnEquip?.Invoke(from, to);
            }
            // equipment -> inventory
            else if (from.Owner == SlotOwner.Equipment && to.Owner == SlotOwner.Inventory)
            {
                SwapImages(from, to);
                OnUnequip?.Invoke(from, to);
                var inv = UI.UIManager.UIDesktop.Components.Find(c => c is UIInventoryComponent) as UIInventoryComponent;
                inv?.PushItem(from.Item);
            }
            // inventory -> inventory
            else if (from.Owner == SlotOwner.Inventory && to.Owner == SlotOwner.Inventory)
            {
                //SwapImages(from, to);
                //OnSwapInventory?.Invoke(from, to);
            }
            // equipment -> equipment: not allowed
            else
            {
                SnapBack(from);
                return;
            }

            SnapBack(from);
        }

        private void SwapImages(SlotEntry a, SlotEntry b)
        {
            (a.Widget.Image, b.Widget.Image) = (b.Widget.Image, a.Widget.Image);
            (a.Item, b.Item) = (b.Item, a.Item);
        }

        private void SnapBack(SlotEntry entry)
        {
            if (entry == null) return;

            entry.Widget.Left = entry.OriginalLeft;
            entry.Widget.Top = entry.OriginalTop;
            entry.Widget.Opacity = 1f;
        }

        private SlotEntry FindSlotUnder(Point position)
        {
            Console.WriteLine(position);
            foreach (var entry in _slots)
            {
                if (entry == _dragging) continue;
                if (UIDesktop.GetAbsoluteBounds(entry.Widget).Contains(position))
                    return entry;
            }
            return null;
        }

        public void UnregisterInventorySlots(UIInventoryComponent owner)
        {
            _slots.RemoveAll(e => e.Owner == SlotOwner.Inventory);
        }



        private void InitializeTooltip()
        {
            _tooltipName = new Label
            {
                StyleName = "questTitle",
                Width = 164
            };

            _tooltipDesc = new Label
            {
                StyleName = "muted",
                Width = 164
            };

            _tooltipType = new Label
            {
                StyleName = "hud",
                Width = 164
            };

            var content = new VerticalStackPanel
            {
                Spacing = 4,
                Left = 8,
                Top = 8,
            };
            content.Widgets.Add(_tooltipName);
            content.Widgets.Add(_tooltipType);
            content.Widgets.Add(_tooltipDesc);

            _tooltip = new Panel
            {
                Width = 180,
                Visible = false,
                ZIndex = 9999,
                Background = new SolidBrush(new Color(15, 15, 25, 230)),
                Border = new SolidBrush(new Color(120, 120, 160, 200)),
                BorderThickness = new Thickness(1)
            };
            _tooltip.Widgets.Add(content);

            UI.UIManager.UIDesktop.Desktop.Widgets.Add(_tooltip);
        }

        private void ShowTooltip(Item item, Point position)
        {
            if (item == null || _tooltip == null) return;

            _tooltipName.Text = item.Name;
            _tooltipType.Text = item.Type.ToString();
            _tooltipDesc.Text = item.Description ?? "";

            // position tooltip offset from cursor
            _tooltip.Left = position.X + 16;
            _tooltip.Top = position.Y + 16;
            _tooltip.Visible = true;
        }

        private void HideTooltip()
        {
            if (_tooltip != null)
                _tooltip.Visible = false;
        }

        private void UpdateTooltipPosition(Point position)
        {
            if (_tooltip == null || !_tooltip.Visible) return;
            _tooltip.Left = position.X + 16;
            _tooltip.Top = position.Y + 16;
        }
    }
}