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
            Vector2 inGameMenuSize = new Vector2(80, (64+12)*6);
            Vector2 inGameMenuPos = new Vector2(0 + 10, Graphics.Graphics.screen.Height - inGameMenuSize.Y - 10);

            Vector2 frameSize = new Vector2(Graphics.Graphics.screen.Width - (inGameMenuSize.X + 10 + 10 + 10), Graphics.Graphics.screen.Height - (10+10));
            Position = new Vector2(inGameMenuPos.X + inGameMenuSize.X + 10, Graphics.Graphics.screen.Height - frameSize.Y - 10);
           
            type = UIComponentTypes.MENU_INGAME_INVENTORY;

            children = new UIComponent[3];
            children[0] = new UIInventoryComponent(-1, new Vector2(Position.X + 10, Position.Y + frameSize.Y - 100), Entities.Entities.Player);
            children[1] = new UITextStringComponent(-1, new Vector2(250, Position.Y + frameSize.Y - 30), "Inventory", 0, Vector2.One);
            children[2] = new UIEquipmentComponent(-1, new Vector2(Position.X + 10 + frameSize.X/2, Position.Y + frameSize.Y - 100), Entities.Entities.Player);
            
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
