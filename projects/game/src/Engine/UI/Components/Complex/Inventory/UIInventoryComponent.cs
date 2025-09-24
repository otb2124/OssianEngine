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


        public UIInventoryComponent(int id, Vector2 pos, Inventory inventory) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY;

            SortingService = new UIInventorySortingService(inventory.Items);
            Items = SortingService.GetSortedItems();

            InventoryType = UIInventoryTypes.INVENTORY;

            children = new UIComponent[4];
            children[0] = new UIInventorySlotBoardComponent(-1, pos, Items);
            children[1] = new UIInventorySortingPanelComponent(-1, pos);
            children[2] = new UIInventoryPagerComponent(-1, new Vector2(250, 100));
            children[3] = new UITextStringComponent(-1, new Vector2(250, 600), "Inventory", 0, Vector2.One);
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
                        Items = SortingService.GetSortedItems(((UIInventorySortingPanelComponent)children[1]).CurrentOptionType);
                        Refresh();
                        children[0] = new UIInventorySlotBoardComponent(-1, Position, Items);
                    }
                }
            }
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
                child.Refresh();
            }

            base.Refresh();
        }
    }
}
