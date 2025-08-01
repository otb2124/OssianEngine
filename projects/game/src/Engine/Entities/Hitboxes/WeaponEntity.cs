using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System;
using System.Text;
using System.Threading.Tasks;
using UI;
using Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Model = Resources.Model;

namespace Entities
{
    public class WeaponEntity
    {

        public WeaponHitbox hitbox;
        public StaticSprites sprite;
        public AnimationManager aManager;

        public float swingSpeed;
        public float currentSwingTime = 0f;
        public bool isSwinging = false;

        public Vector2 Size;
        public WeaponComboHitSet Combo;

        public bool MovedPlayer = false;

        public WeaponEntity()
        {
            hitbox = new WeaponHitbox();
        }

        public void Init()
        {
            aManager = new AnimationManager();

            sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
            aManager.AddAnimationForBothDirections(StaticSpriteFactory.spriteMappings[sprite], AnimationStates.IDLE, 4, new Vector2(0, 0), new Vector2(128, 128), swingSpeed / 4);

            Combo = new WeaponComboHitSet();
        }


        public void Update(Model model)
        {
            float deltaTime = (float)Graphics.Graphics.gameTime.ElapsedGameTime.TotalSeconds;
            int horizontalXFactor = model.direction == Directions.RIGHT ? 1 : -1;
            Vector2 weaponPosition = model.body.Position.ToVector2() + Combo.GetCurrentHit().HitboxPositionOffset * new Vector2(horizontalXFactor, 1f);

            if (model.modelState == ModelStates.ATTACKING_LIGHT)
            {
                hitbox.Update(
                    weaponPosition,
                    Size,
                    Combo.GetCurrentHit().HitboxRotationOffset * horizontalXFactor
                );

                if(!MovedPlayer)
                {
                    model.body.Move(new FlatVector(Combo.GetCurrentHit().EntityPositionOffset.X * horizontalXFactor, Combo.GetCurrentHit().EntityPositionOffset.Y));
                    MovedPlayer = true;
                }

                if (!isSwinging)
                {
                    isSwinging = true;
                    currentSwingTime = 0f;
                    if (Combo.AllowContinuation && Combo.ContinuationAllowCounter < Combo.ContinuationAllowTimeSec)
                    {
                        Combo.UpdateSet();
                        MovedPlayer = false;
                    }
                    else
                    {
                        Combo.ResetCombo();
                    }
                    Combo.StartContinuationWindow();
                }

                currentSwingTime += deltaTime;

                if (currentSwingTime >= swingSpeed)
                {
                    isSwinging = false;
                    model.modelState = ModelStates.WEAPON_OUT_IDLE;
                }

                aManager.Update(new Tuple<Directions, AnimationStates>(model.direction, AnimationStates.IDLE));
            }
            else
            {
                Combo.UpdateCounter(deltaTime);
                hitbox.Update(
                    new Vector2(0, 0),
                    new Vector2(0, 0)
                );
                isSwinging = false;
            }
        }

        public void Draw(Model model)
        {
            if (model.modelState != ModelStates.ATTACKING_LIGHT)
                return;

            //enitity model draw
            Rectangle spriteSize = model.aManager.GetCurrent().GetCurrentFrame();
            float scaleX = 1f;
            float scaleY = 1f;
            float bodyWidth = model.body.Width + model.bodyOffset.X;
            float bodyHeight = model.body.Height + model.bodyOffset.Y;
            scaleX = bodyWidth / spriteSize.Width;
            scaleY = bodyHeight / spriteSize.Height;

            Vector2 entityBodyPos = model.body.Position.ToVector2();

            float directionXOffset = - 10;
            if (model.direction == Directions.LEFT)
            {
                directionXOffset = model.body.Width*3f + 10;
            }
            
            Vector2 entityBodyPosWithOffset = new Vector2(entityBodyPos.X - model.body.Width/2f - directionXOffset, entityBodyPos.Y - model.body.Height/2f);

            aManager.GetCurrent().Draw(entityBodyPosWithOffset, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), 0f);
            
        }


        public void DrawHitbox()
        {
            hitbox.Draw(Color.Red);
        }
    }
}
