using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System.Reflection;
using Utils;
using MathHelper = Microsoft.Xna.Framework.MathHelper;
using Model = Resources.Model;

namespace Entities
{
    public class WeaponEquipment : Equipment
    {

        public AnimationManager aManager;
        public StaticSprites sprite;

        public WeaponHitbox hitbox;

        public float swingSpeed;
        public float currentSwingTime = 0f;
        public bool isSwinging = false;

        public WeaponEquipment(ItemKey itemKey) : base(itemKey)
        {
            hitbox = new WeaponHitbox();

            aManager = new AnimationManager();

            this.sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
            this.aManager.AddStaticAnimation(StaticSpriteFactory.spriteMappings[this.sprite]);
        }

        public override void SetItem()
        {
            switch(ItemKey.EnumValue)
            {
                case ItemLib.Weapons.BARE_HAND:
                    Name = "Bare hands";
                    Description = "A terrablade";
                    Value = 0;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 1;
                    swingSpeed = 0.4f;
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    break;
                case ItemLib.Weapons.TERRABLADE:
                    Name = "Terrablade";
                    Description = "A terrablade";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 20;
                    swingSpeed = 0.4f;
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    break;
            }
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


        public void Update(Model model)
        {
            float horizontalOffset = model.direction == Directions.RIGHT ? 10f : -10f;
            float weaponRot = model.direction == Directions.RIGHT ? Utils.MathHelper.DegreesToRadians(90) : Utils.MathHelper.DegreesToRadians(-90);
            Vector2 weaponPosition = FlatConverter.ToVector2(model.body.Position) + new Vector2(horizontalOffset, 0);

            if (model.modelState == ModelStates.ATTACKING)
            {
                hitbox.Update(
                weaponPosition,
                new Vector2(model.body.Width, model.body.Height)
                );


                if (!isSwinging)
                {
                    Swing();
                }

                UpdateSwing(model.direction);

                if (!isSwinging)
                {
                    model.modelState = ModelStates.BATTLE_IDLE;
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

        public override void Draw(Directions direction)
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
