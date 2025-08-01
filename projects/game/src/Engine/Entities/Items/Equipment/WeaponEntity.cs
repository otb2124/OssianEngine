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

        public Vector2 PositionOffset;
        public float RotationOffset;

        public WeaponEntity()
        {
            hitbox = new WeaponHitbox();

            aManager = new AnimationManager();

            sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
            aManager.AddStaticAnimation(StaticSpriteFactory.spriteMappings[this.sprite]);

            PositionOffset = new Vector2(10, 40);
            RotationOffset = 1.8f;
        }


        public void Update(Model model)
        {
            int horizontalXFactor = model.direction == Directions.RIGHT ? 1 : -1;
            Vector2 weaponPosition = FlatConverter.ToVector2(model.body.Position) + PositionOffset * new Vector2(horizontalXFactor, 0);

            if (model.modelState == ModelStates.ATTACKING_LIGHT)
            {
                hitbox.Update(
                weaponPosition,
                Size,
                RotationOffset * horizontalXFactor
                );


                if (!isSwinging)
                {
                    isSwinging = true;
                    currentSwingTime = 0f;
                }

                UpdateSwing(model.direction);

                if (!isSwinging)
                {
                    model.modelState = ModelStates.WEAPON_OUT_IDLE;
                }
            }
            else
            {
                hitbox.Update(
                new Vector2(0, 0),
                new Vector2(0, 0)
                );
                isSwinging = false;
            }
        }


        public void UpdateSwing(Directions direction)
        {
            currentSwingTime += (float)Graphics.Graphics.gameTime.ElapsedGameTime.TotalSeconds;

            if (currentSwingTime >= swingSpeed)
            {
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
