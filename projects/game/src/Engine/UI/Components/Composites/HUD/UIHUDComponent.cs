using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Resources.StaticSpriteFactory;

namespace UI
{
    public class UIHUDComponent : UIComponent
    {

        public UIHUDComponent(int id) : base(id)
        {
            
            Position = Vector2.Zero;

            type = UIComponentTypes.HUD;

            children = new UIComponent[4];
            children[0] = new UICursorComponent(-1);

            //INDICATORS
            Vector2 topLeft = new Vector2(0, Graphics.Graphics.screen.Height - 32*1.5f);
            children[1] = new UIStatBarComponent(-1, new Vector2(Position.X + 10, Position.Y + topLeft.Y), UIStatBarComponent.UIStatBarStatBindings.PLAYER_HEALTH, 100, 100);
            children[2] = new UIStatBarComponent(-1, new Vector2(Position.X + 10, Position.Y + topLeft.Y - 20), UIStatBarComponent.UIStatBarStatBindings.PLAYER_MANA, 100, 100);
            children[3] = new UIStatBarComponent(-1, new Vector2(Position.X + 10, Position.Y + topLeft.Y - 40), UIStatBarComponent.UIStatBarStatBindings.PLAYER_ENDURANCE, 100, 100);
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
