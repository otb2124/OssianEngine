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
    public class UIStatBarComponent : UIComponent
    {
        public enum UIStatBarStatBindings
        {
            PLAYER_HEALTH,
            PLAYER_MANA,
            PLAYER_ENDURANCE
        }

        public UIStatBarStatBindings Stat;
        public float currentXScale = 1;

        public UIStatBarComponent(int id, Vector2 pos, UIStatBarStatBindings stat, float maxValue, float currentValue) : base(id)
        {
            type = UIComponentTypes.STAT_BAR;
            Position = pos;

            Stat = stat;


            int spriteSheetOffsetY = 0;

            switch(Stat)
            {
                case UIStatBarStatBindings.PLAYER_HEALTH:
                    spriteSheetOffsetY += 0;
                    break;
                case UIStatBarStatBindings.PLAYER_MANA:
                    spriteSheetOffsetY += 32;
                    break;
                case UIStatBarStatBindings.PLAYER_ENDURANCE:
                    spriteSheetOffsetY += 64;
                    break;
            }

            SpriteData[] spriteData = StaticSpriteFactory.UIHUDStatBarCut(new Vector2(0, 0 + spriteSheetOffsetY), 32);

            Vector2 scale = new Vector2(1.5f, 1.5f);

            children = new UIComponent[4];
            children[0] = new UIIconComponent(-1, spriteData[0], Position, scale);
            children[1] = new UIIconComponent(-1, spriteData[1], new Vector2(Position.X + 32*scale.X, Position.Y), scale);
            children[2] = new UIIconComponent(-1, spriteData[2], new Vector2(Position.X + 64*scale.X, Position.Y), scale);
                
            children[3] = new UIIconComponent(-1, spriteData[3], new Vector2(Position.X + 16*scale.X, Position.Y), scale);

            currentXScale = scale.X + 3;
        }


        public void RescaleCurrent()
        {
            ((UIIconComponent)children[3]).Scale.X = currentXScale;


            float rescaleMultiplier = 1;

            switch (Stat)
            {
                case UIStatBarStatBindings.PLAYER_HEALTH:
                    rescaleMultiplier = MathHelper.Clamp(
                            Entities.Entities.player.sManager.stats.HP / Entities.Entities.player.sManager.stats.maxHP,
                            0f,
                            1f
                        );
                    break;
                case UIStatBarStatBindings.PLAYER_MANA:
                    rescaleMultiplier = MathHelper.Clamp(
                            Entities.Entities.player.sManager.stats.mana / Entities.Entities.player.sManager.stats.maxMana,
                            0f,
                            1f
                        );
                    break;
                case UIStatBarStatBindings.PLAYER_ENDURANCE:
                    rescaleMultiplier = MathHelper.Clamp(
                            Entities.Entities.player.sManager.stats.endurance / Entities.Entities.player.sManager.stats.maxEndurance,
                            0f,
                            1f
                        );
                    break;
            }

            ((UIIconComponent)children[3]).Scale.X = rescaleMultiplier*3;
        }


        public override void Update()
        {
            RescaleCurrent();

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

                children[3].Draw();

                for (int i = 0; i < 3; i++)
                {
                    children[i].Draw();
                }
            }
        }
    }
}
