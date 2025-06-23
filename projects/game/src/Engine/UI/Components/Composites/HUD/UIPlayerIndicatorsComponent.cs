using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Resources.StaticSpriteFactory;

namespace UI
{
    public class UIPlayerIndicatorsComponent : UIComponent
    {

        public UIPlayerIndicatorsComponent(int id, Vector2 pos) : base(id)
        {
            children = new UIComponent[4];

            Position = pos;

            type = UIComponentTypes.PLAYER_INDICATORS;

            //frame
            SpriteData[] spriteData = StaticSpriteFactory.UIHUDStatBarCut(new Vector2(0, 0), 64);
            Vector2 scale = new Vector2(1, 1);
            children = new UIComponent[6];
            children[0] = new UIIconComponent(-1, spriteData[0], Position, scale);
            children[1] = new UIIconComponent(-1, spriteData[1], new Vector2(Position.X + 64 * scale.X, Position.Y), scale);
            children[2] = new UIIconComponent(-1, spriteData[2], new Vector2(Position.X + 64 * 2 * scale.X, Position.Y), scale);

            //indicators
            children[3] = new UIStatBarComponent(-1, new Vector2(Position.X + 40, Position.Y), UIStatBarComponent.UIStatBarStatBindings.PLAYER_HEALTH);
            children[4] = new UIStatBarComponent(-1, new Vector2(Position.X + 40, Position.Y - 8), UIStatBarComponent.UIStatBarStatBindings.PLAYER_MANA);
            children[5] = new UIStatBarComponent(-1, new Vector2(Position.X + 40, Position.Y - 20), UIStatBarComponent.UIStatBarStatBindings.PLAYER_ENDURANCE);
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
