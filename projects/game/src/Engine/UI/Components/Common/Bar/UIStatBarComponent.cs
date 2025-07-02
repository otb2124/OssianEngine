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

        public UIStatBarComponent(int id, Vector2 pos, UIStatBarStatBindings stat) : base(id)
        {
            type = UIComponentTypes.STAT_BAR;
            Position = pos;

            Stat = stat;


            int spriteSheetOffsetX = 0;

            switch(Stat)
            {
                case UIStatBarStatBindings.PLAYER_HEALTH:
                    spriteSheetOffsetX += 0;
                    break;
                case UIStatBarStatBindings.PLAYER_MANA:
                    spriteSheetOffsetX += 64;
                    break;
                case UIStatBarStatBindings.PLAYER_ENDURANCE:
                    spriteSheetOffsetX += 128;
                    break;
            }

            SpriteData[] spriteData = StaticSpriteFactory.UIHUDStatBarCut(new Vector2(0 + spriteSheetOffsetX, 0), 64);

            Vector2 scale = new Vector2(1, 1);

            children = new UIComponent[1];
                
            children[0] = new UIIconComponent(-1, spriteData[3], new Vector2(Position.X + 16*scale.X, Position.Y), scale);

            currentXScale = scale.X;
        }


        public void RescaleCurrent()
        {
            ((UIIconComponent)children[0]).Scale.X = currentXScale;


            float rescaleMultiplier = 1;

            switch (Stat)
            {
                case UIStatBarStatBindings.PLAYER_HEALTH:
                    rescaleMultiplier = MathHelper.Clamp(
                            Entities.Entities.player.statsManager.stats.HP / Entities.Entities.player.statsManager.stats.maxHP,
                            0f,
                            1f
                        );
                    break;
                case UIStatBarStatBindings.PLAYER_MANA:
                    rescaleMultiplier = MathHelper.Clamp(
                            Entities.Entities.player.statsManager.stats.mana / Entities.Entities.player.statsManager.stats.maxMana,
                            0f,
                            1f
                        );
                    break;
                case UIStatBarStatBindings.PLAYER_ENDURANCE:
                    rescaleMultiplier = MathHelper.Clamp(
                            Entities.Entities.player.statsManager.stats.stamina / Entities.Entities.player.statsManager.stats.maxStamina,
                            0f,
                            1f
                        );
                    break;
            }

            ((UIIconComponent)children[0]).Scale.X = rescaleMultiplier*1.65f;
        }


        public override void Update()
        {
            RescaleCurrent();

            if (children != null)
            {
                children[0].Update();
            }
        }

        public override void Draw()
        {
            if (children != null)
            {
                children[0].Draw();
            }
        }
    }
}
