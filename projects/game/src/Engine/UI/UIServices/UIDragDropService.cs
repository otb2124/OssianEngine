using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Entities;
using static Entities.EquipmentSlot;

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

                _dragging = entry;
                _dragging.OriginalLeft = widget.Left;
                _dragging.OriginalTop = widget.Top;

                // bring to front
                entry.RootPanel.Widgets.Remove(widget);
                entry.RootPanel.Widgets.Add(widget);

                widget.Opacity = 0.7f;
            };

            widget.MouseMoved += (s, e) =>
            {
                if (_dragging?.Widget != widget) return;
                //widget.Left = e.Position.X - (widget.Width / 2);
                //widget.Top = e.Position.Y - (widget.Height / 2);
            };

            widget.TouchUp += (s, e) =>
            {
                if (_dragging?.Widget != widget) return;

                widget.Opacity = 1f;

                var target = FindSlotUnder(new Point()); //FindSlotUnder(e.Position);

                if (target != null && target != _dragging)
                    HandleDrop(_dragging, target);
                else
                    SnapBack(_dragging);

                _dragging = null;
            };
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
                SwapImages(from, to);
                OnSwapInventory?.Invoke(from, to);
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
            entry.Widget.Left = entry.OriginalLeft;
            entry.Widget.Top = entry.OriginalTop;
        }

        private SlotEntry FindSlotUnder(Point position)
        {
            foreach (var entry in _slots)
            {
                if (entry == _dragging) continue;
                if (entry.Widget.Bounds.Contains(position))
                    return entry;
            }
            return null;
        }

        public void UnregisterInventorySlots(UIInventoryComponent owner)
        {
            _slots.RemoveAll(e => e.Owner == SlotOwner.Inventory);
        }
    }
}