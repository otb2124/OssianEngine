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

            aManager = new AnimationManager();

            sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
            aManager.AddStaticAnimation(StaticSpriteFactory.spriteMappings[this.sprite]);

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

        public void Draw(Directions direction)
        {
            //Model
            Rectangle spriteSize = aManager.GetCurrent().GetCurrentFrame();
            float scaleX = 1f;
            float scaleY = 1f;
            Vector2 newPos = new Vector2(hitbox.outerHalf.Center.X, hitbox.outerHalf.Center.Y);
            Vector2 textureCenter = new Vector2(spriteSize.Width / 2f, spriteSize.Height / 2f);

            //for offsets
            //float bodyWidth = hitboxData.extends.Width + bodyOffset.X;
            //float bodyHeight = hitboxData.extends.Height + bodyOffset.Y;

            float bodyWidth = hitbox.outerHalf.Width;
            float bodyHeight = hitbox.outerHalf.Height;

            scaleX = bodyWidth / spriteSize.Width;
            scaleY = bodyHeight / spriteSize.Height;
            newPos = hitbox.outerHalf.Center - new Vector2(bodyWidth / 2f, bodyHeight / 2f);
            newPos += new Vector2(spriteSize.Width / 2f * scaleX, spriteSize.Height / 2f * scaleY);

            SpriteEffects spriteEffect = direction == Directions.RIGHT ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            this.aManager.GetCurrent().Draw(newPos, Color.White, hitbox.extends.Rotation, textureCenter, new Vector2(scaleX, scaleY), spriteEffect, 0f);
        }


        public void DrawHitbox()
        {
            this.hitbox.Draw(Color.Red);
        }
    }
}
