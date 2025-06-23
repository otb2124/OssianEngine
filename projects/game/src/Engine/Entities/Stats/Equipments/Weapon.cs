using CSPlatformerSandbox.Engine.Entities.Stats;
using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utils;
using MathHelper = Microsoft.Xna.Framework.MathHelper;

namespace Entities
{
    public class Weapon : Equipment
    {

        public AnimationManager aManager;
        public StaticSprites sprite;

        public WeaponHitbox hitbox;

        public Weapon()
        {
            hitbox = new WeaponHitbox();

            aManager = new AnimationManager();

            this.sprite = StaticSprites.ENTITIES_WEAPONS_SWORD0;
            this.aManager.AddStaticAnimation(this.sprite);
        }

        public void Swing()
        {
            if (isSwinging)
                return;

            isSwinging = true;
            currentSwingTime = 0f;
        }

        public void UpdateSwing(Directions direction)
        {
            if (!isSwinging)
            {
                float startRotation = direction == Directions.LEFT ? 0 : 180;
                hitbox.extends.Rotation = MathHelper.ToRadians(startRotation);
                return;
            }

            currentSwingTime += (float)Graphics.Graphics.gameTime.ElapsedGameTime.TotalSeconds;

            float startRotationSwing = direction == Directions.LEFT ? -180 : 180;
            float endRotationSwing = direction == Directions.LEFT ? 0 : 0;

            if (currentSwingTime >= swingSpeed)
            {
                hitbox.extends.Rotation = MathHelper.ToRadians(endRotationSwing);
                isSwinging = false;
            }
            else
            {
                float rotationAmount = MathHelper.ToRadians(startRotationSwing) + (MathHelper.ToRadians(endRotationSwing) - MathHelper.ToRadians(startRotationSwing)) * (currentSwingTime / swingSpeed);
                hitbox.extends.Rotation = rotationAmount;
            }
        }




        public override void Draw(Directions direction)
        {
            //model
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
