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
        public Widget RootPanel;
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

        public UITooltipComponent tooltip;


        public UIDragDropService()
        {
            InitializeTooltip();
            InitializeGlobalHandlers();
        }

        public void RegisterInventorySlot(ImageButton widget, Func<Item> getItem)
        {
            var entry = new SlotEntry
            {
                Widget = widget,
                Owner = SlotOwner.Inventory,
                RootPanel = widget.Parent
            };
            _slots.Add(entry);
            WireEvents(entry, getItem);
        }

        public void RegisterEquipmentSlot(ImageButton widget, EquipmentSlots slot)
        {
            var entry = new SlotEntry
            {
                Widget = widget,
                Owner = SlotOwner.Equipment,
                EquipSlot = slot,
                RootPanel = widget.Parent
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

                var absPos = entry.Widget.ToGlobal(Point.Zero);
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

            var mousePos = Inputs.Inputs.mouse.GetMouseScreenPosition();
            var finalPos = new Vector2(mousePos.X * UIDesktop.UIScale, mousePos.Y * UIDesktop.UIScale).ToPoint();
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
            foreach (var entry in _slots)
            {
                if (entry == _dragging) continue;

                var absoluteBounds = new Rectangle(entry.Widget.ToGlobal(Point.Zero), new Point((int)entry.Widget.Width, (int)entry.Widget.Width));

                Console.WriteLine(entry.EquipSlot.ToString());
                Console.WriteLine(absoluteBounds);
                Console.WriteLine(position);

                if (absoluteBounds.Contains(position))
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
            
        }

        private void ShowTooltip(Item item, Point position)
        {
            if(tooltip == null)
            {
                tooltip = new UITooltipComponent();
                UI.UIManager.UIDesktop.AddComponent(tooltip);
            }


            if (item == null || tooltip == null) return;

            UI.UIManager.UIDesktop.SetLabelText("tooltipName", item.Name);
            UI.UIManager.UIDesktop.SetLabelText("tooltipType", item.Type.ToString());
            UI.UIManager.UIDesktop.SetLabelText("tooltipDesc", item.Description);
            UI.UIManager.UIDesktop.SetLabelText("tooltipRarity", item.Rarity.ToString());
            UI.UIManager.UIDesktop.SetLabelText("tooltipValue", item.Value.ToString());

            // position tooltip offset from cursor
            tooltip.Template.Project.Root.Left = position.X + 16;
            tooltip.Template.Project.Root.Top = position.Y + 16;
            tooltip.Template.Project.Root.Visible = true;
        }

        private void HideTooltip()
        {
            if (tooltip != null)
                tooltip.Template.Project.Root.Visible = false;
        }

        private void UpdateTooltipPosition(Point position)
        {
            if (tooltip == null || !tooltip.Template.Project.Root.Visible) return;
            tooltip.Template.Project.Root.Left = position.X + 16;
            tooltip.Template.Project.Root.Top = position.Y + 16;
        }
    }
}