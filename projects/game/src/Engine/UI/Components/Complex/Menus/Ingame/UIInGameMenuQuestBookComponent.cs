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
    public class UIInGameMenuQuestBookComponent : UIComponent
    {

        public UIInGameMenuQuestBookComponent(int id) : base(id)
        {
            Vector2 inGameMenuSize = new Vector2(80, (64+12)*6);
            Vector2 inGameMenuPos = new Vector2(0 + 10, Graphics.Graphics.screen.Height - inGameMenuSize.Y - 10);

            Vector2 frameSize = new Vector2(Graphics.Graphics.screen.Width - (inGameMenuSize.X + 10 + 10 + 10), Graphics.Graphics.screen.Height - (10+10));
            Position = new Vector2(inGameMenuPos.X + inGameMenuSize.X + 10, Graphics.Graphics.screen.Height - frameSize.Y - 10);
           

            type = UIComponentTypes.MENU_INGAME_QUESTBOOK;

            children = new UIComponent[2];
            children[0] = new UIFrameComponent(-1, Position, frameSize);
            children[1] = new UITextStringComponent(-1, new Vector2(Position.X + frameSize.X / 2, Position.Y + frameSize.Y - 30), "QuestBook", 0, Vector2.One, Color.White);
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
