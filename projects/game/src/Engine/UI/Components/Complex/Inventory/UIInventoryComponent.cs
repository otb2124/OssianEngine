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

    public class UIInventoryComponent : UIComponent
    {

        public List<Item> Items;
        public UIInventoryTypes InventoryType;

        public UIInventorySortingService SortingService;
        public UIInventoryPagerService PagerService;

        public bool WasSortedFlag = false;
        public bool WasPageChangedFlag = false;

        public UIInventoryComponent(int id, Vector2 pos, Inventory inventory) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY;

            SortingService = new UIInventorySortingService(inventory.Items);
            PagerService = new UIInventoryPagerService(inventory.Items);
            Items = PagerService.Pages[PagerService.CurrentPage];

            InventoryType = UIInventoryTypes.INVENTORY;

            children = new UIComponent[4];
            children[0] = new UIInventorySlotBoardComponent(-1, pos, Items);
            children[1] = new UIInventorySortingPanelComponent(-1, pos);
            children[2] = new UIInventoryPagerComponent(-1, new Vector2(pos.X, 100), PagerService.GetIndicatorToString());
            children[3] = new UITextStringComponent(-1, new Vector2(250, 600), "Inventory", 0, Vector2.One, Color.White);
        }

        public UIInventoryComponent(int id, Vector2 pos, Equipments equipments) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY;

            InventoryType = UIInventoryTypes.EQUIPMENT;

            Items = equipments.ToInventory().Items;

            children = new UIComponent[4];
            children[0] = new UIInventorySlotBoardComponent(-1, pos, Items, new int[][] { new int[] { -1 } });
        }

        public override void Update()
        {
            WasSortedFlag = false;
            WasPageChangedFlag = false;

            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    if(children[i] != null)
                    {
                        children[i].Update();
                    }
                }


                if (children[1] != null)
                {
                    if (((UIInventorySortingPanelComponent)children[1]).WasOptionTypeChangedFlag)
                    {
                        SortingService.SetSortingOption(((UIInventorySortingPanelComponent)children[1]).CurrentOptionType);
                        SwitchSorting(SortingService.CurrentSortingOption);
                    }
                }

                if (children[2] != null)
                {
                    if(children[2] is UIInventoryPagerComponent pagerComponent)
                    {
                        if(pagerComponent.OnPrevClick || pagerComponent.OnNextClick)
                        {
                            if (pagerComponent.OnPrevClick)
                            {
                                PagerService.SwitchToPrevious();
                            }

                            if (pagerComponent.OnNextClick)
                            {
                                PagerService.SwitchToNext();
                            }

                            SwitchPage(PagerService.CurrentPage);
                        }
                    }
                    
                }
            }
        }

        public void SwitchSorting(UIInventorySortingOptions option)
        {
            SortingService.SetSortingOption(option);
            PagerService.UpdateList(SortingService.GetSortedItems());
            Items = SortingService.GetSortedItems();
            ((UIInventorySlotBoardComponent)children[0]).Items = Items;
            ((UIInventorySlotBoardComponent)children[0]).UpdateSlotItems();
            WasSortedFlag = true;

            SwitchPage(0);
        }

        public void SwitchPage(int id)
        {
            Items = PagerService.Pages[id];

            ((UIInventorySlotBoardComponent)children[0]).Items = Items;
            ((UIInventorySlotBoardComponent)children[0]).UpdateSlotsLayout();
            ((UIInventorySlotBoardComponent)children[0]).UpdateSlots();
            ((UIInventorySlotBoardComponent)children[0]).UpdateSlotItems();

            ((UIInventoryPagerComponent)children[2]).UpdateIndicator(PagerService.GetIndicatorToString());

            WasPageChangedFlag = true;
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


        public override void Refresh()
        {
            foreach (UIComponent child in children)
            {
                if(child != null)
                {
                    child.Refresh();
                }
            }
        }
    }
}
