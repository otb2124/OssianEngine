using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Resources.StaticSpriteFactory;

namespace UI
{
    public class UIInGameMenuInventoryComponent : UIComponent
    {

        public UIInGameMenuInventoryComponent(int id) : base(id)
        {
            type = UIComponentTypes.MENU_INGAME_INVENTORY;

            children = new UIComponent[2];
            children[0] = new UIInventoryToEquipmentComponent(-1, new Vector2(100, 500), Entities.Entities.Player.Inventory, Entities.Entities.Player.EquipmentManager.Equipments);
            children[1] = new UITextStringComponent(-1, new Vector2(250, 600), "Inventory", 0, Vector2.One);
        }

        public override void Update()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Update();
                }
            }
        }

        public override void Draw()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Draw();
                }
            }
        }
    }
}
