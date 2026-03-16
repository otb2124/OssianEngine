using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UI
{
    public enum UIInventoryTypes
    {
        INVENTORY,
        EQUIPMENT,
        TRADE_BUFFER
    }

    // Shows a pageable, filterable inventory grid.
    // Always operates directly on Inventory.Items — no copies, no snapshots.
    public class UIInventoryComponent : UIComponent
    {
        public Inventory Inventory;
        public UIInventoryTypes InventoryType = UIInventoryTypes.INVENTORY;

        public UIInventorySortingService SortingService;
        public UIInventoryPagerService PagerService;

        // The current page's item list (a GetRange slice — read-only view)
        public List<Item> CurrentPageItems => PagerService.GetCurrentPage();

        // Slot board is children[0], sorting panel children[1], pager children[2]
        private UIInventorySlotBoardComponent Board => (UIInventorySlotBoardComponent)children[0];
        private UIInventorySortingPanelComponent SortPanel => children[1] as UIInventorySortingPanelComponent;
        private UIInventoryPagerComponent Pager => children[2] as UIInventoryPagerComponent;

        // Set by Update() so the exchange component knows something changed
        public UIInventorySlotComponent ClickedSlot { get; private set; }
        public bool SlotRightClicked { get; private set; }

        public UIInventoryComponent(int id, Vector2 pos, Inventory inventory) : base(id)
        {
            Position = pos;
            type = UIComponentTypes.INVENTORY;
            Inventory = inventory;

            SortingService = new UIInventorySortingService(Inventory.Items);
            PagerService = new UIInventoryPagerService(SortingService.GetFilteredItems());

            children = new UIComponent[4];
            children[0] = new UIInventorySlotBoardComponent(-1, pos, CurrentPageItems);
            children[1] = new UIInventorySortingPanelComponent(-1, pos);
            children[2] = new UIInventoryPagerComponent(-1, new Vector2(pos.X, 100),
                              PagerService.GetIndicatorToString());
            children[3] = new UITextStringComponent(-1, new Vector2(250, 600),
                              "Inventory", 0, Vector2.One, Color.White);
        }

        public override void Update()
        {
            ClickedSlot = null;
            SlotRightClicked = false;

            for (int i = 0; i < children.Length; i++)
                children[i]?.Update();

            // Sorting
            if (SortPanel != null && SortPanel.WasOptionTypeChangedFlag)
            {
                SortingService.SetSortingOption(SortPanel.CurrentOptionType);
                PagerService.UpdateList(SortingService.GetFilteredItems());
                ApplyPage();
            }

            // Paging
            if (Pager != null)
            {
                if (Pager.OnPrevClick) { PagerService.SwitchToPrevious(); ApplyPage(); }
                if (Pager.OnNextClick) { PagerService.SwitchToNext(); ApplyPage(); }
            }

            // Detect clicked slot — report back to parent
            foreach (UIComponent child in Board.children)
            {
                if (child is UIInventorySlotComponent slot && slot.Item != null)
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

        // Refresh board to match current inventory state (call after equip/unequip)
        public void RefreshBoard()
        {
            PagerService.UpdateList(SortingService.GetFilteredItems());
            ApplyPage();
        }

        private void ApplyPage()
        {
            Board.Items = CurrentPageItems;
            Board.UpdateSlotsLayout();
            Board.UpdateSlots();
            Board.UpdateSlotItems();
            Pager?.UpdateIndicator(PagerService.GetIndicatorToString());
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