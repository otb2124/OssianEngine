using Entities;
using Microsoft.Xna.Framework;


namespace UI
{
    public class UIInventoryComponent : UIComponent
    {

        public UIInventoryComponent(int id, Vector2 pos, Inventory inventory) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY;

            children = new UIComponent[1];

            children[0] = new UIInventorySlotBoardComponent(id, pos, inventory.Items);
        }

        public UIInventoryComponent(int id, Vector2 pos, Equipments equipments) : base(id)
        {
            Position = pos;

            type = UIComponentTypes.INVENTORY;

            children = new UIComponent[1];

            children[0] = new UIInventorySlotBoardComponent(id, pos, equipments.ToInventory().Items, new int[][] { new int[] { -1 } });
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
    }
}
