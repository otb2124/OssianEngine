using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using System;
using System.Diagnostics;
using Utils;
using MathHelper = Utils.MathHelper;

namespace Entities
{
    public class Player : LivingEntity
    {

        public Player(Vector2 pos) : base(Models.PLAYER, pos, 0f)
        {
            
        }

        public override void SetStats()
        {
            sManager.stats.maxHP = 100;
            sManager.stats.maxSpeed = 2;
            sManager.stats.maxMana = 100;
            sManager.stats.maxEndurance = 100;

            sManager.stats.Refill();

            sManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment = (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE));
            sManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_CHESTPLATE));

            sManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment.PhysDmg = 1;
            ((WeaponEquipment)sManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment).swingSpeed = 0.4f;

            sManager.inventory.SlotsAmount = 42;
            sManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Consumables.HEALTH_POTION)));
            sManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE)));
            sManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            sManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            sManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));


            Debug.WriteLine(sManager.inventory.Items[0].Name);

            base.SetStats();
        }


        public override void SetAnimations()
        {
            model.aManager = new AnimationManager();
            float frameSpeed = 0;
            //idle
            frameSpeed = 0.1f;
            model.aManager.AddAnimation(model.spriteData, Directions.LEFT, AnimationStates.IDLE, 6, new Vector2(0, 0), new Vector2(64, 128), frameSpeed, SpriteEffects.FlipHorizontally);
            model.aManager.AddAnimation(model.spriteData, Directions.RIGHT, AnimationStates.IDLE, 6, new Vector2(0, 0), new Vector2(64, 128), frameSpeed, SpriteEffects.None);

            //move
            frameSpeed = 0.04f;
            model.aManager.AddAnimation(model.spriteData, Directions.LEFT, AnimationStates.MOVING, 8, new Vector2(0, 128), new Vector2(64, 128), frameSpeed, SpriteEffects.FlipHorizontally);
            model.aManager.AddAnimation(model.spriteData, Directions.RIGHT, AnimationStates.MOVING, 8, new Vector2(0, 128), new Vector2(64, 128), frameSpeed, SpriteEffects.None);

            //jump
            frameSpeed = 0.04f;
            model.aManager.AddAnimation(model.spriteData, Directions.LEFT, AnimationStates.JUMPING, 1, new Vector2(0, 128*2), new Vector2(64, 128), frameSpeed, SpriteEffects.FlipHorizontally);
            model.aManager.AddAnimation(model.spriteData, Directions.RIGHT, AnimationStates.JUMPING, 1, new Vector2(0, 128*2), new Vector2(64, 128), frameSpeed, SpriteEffects.None);
        }


        public override void Update()
        {
            //Debug.WriteLine(this.sManager.stats.HP);

            if (KeyHandlerUtil.isPlayerMoving())
            {
                this.UpdateMovement();
            }
            else
            {
                if(!(this.model.modelState == ModelStates.ATTACKING))
                {
                    model.modelState = ModelStates.IDLE;
                }


                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKPRESSED])
                {
                    model.modelState = ModelStates.ATTACKING;
                }
            }

            UpdateHitboxes();

            UpdateAnimationState();
            base.Update();
        }


        public void UpdateMovement()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                model.body.Move(new FlatVector(sManager.stats.speed, 0));
                model.modelState = ModelStates.MOVING;
                model.direction = Directions.RIGHT;
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
            {
                model.body.Move(new FlatVector(-sManager.stats.speed, 0));
                model.modelState = ModelStates.MOVING;
                model.direction = Directions.LEFT;
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
            {
                model.body.Jump(2.5f);
                model.modelState = ModelStates.JUMPING;
            }
        }



        public void UpdateHitboxes()
        {

            //weapon
            float horizontalOffset = this.model.direction == Directions.RIGHT ? 10f : -10f;
            float weaponRot = this.model.direction == Directions.RIGHT ? MathHelper.DegreesToRadians(90) : MathHelper.DegreesToRadians(-90);
            Vector2 weaponPosition = FlatConverter.ToVector2(this.model.body.Position) + new Vector2(horizontalOffset, 0);

            
            if (this.model.modelState == ModelStates.ATTACKING)
            {
                this.sManager.equipmentManager.GetCurrentWeapon().hitbox.Update(
                    weaponPosition,
                    new Vector2(this.model.body.Width, this.model.body.Height)
                );


                if (!this.sManager.equipmentManager.GetCurrentWeapon().isSwinging)
                {
                    this.sManager.equipmentManager.GetCurrentWeapon().Swing();
                }

                this.sManager.equipmentManager.GetCurrentWeapon().UpdateSwing(this.model.direction);

                if (!this.sManager.equipmentManager.GetCurrentWeapon().isSwinging)
                {
                    this.model.modelState = ModelStates.IDLE;
                }
            }
            else
            {
                this.sManager.equipmentManager.GetCurrentWeapon().hitbox.Update(
                new Vector2(0,0),
                new Vector2(0, 0)
                );
                this.sManager.equipmentManager.GetCurrentWeapon().isSwinging = false;
            }


            //armor
            ((ArmorEquipment)this.sManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment).armorHB.Update(
                FlatConverter.ToVector2(this.model.body.Position),
                new Vector2(this.model.body.Width, this.model.body.Height - 20),
                0f
            );
        }


        
        public void UpdateAnimationState()
        {
            switch(model.modelState)
            {
                case ModelStates.MOVING:
                    model.animationState = AnimationStates.MOVING;
                    break;
                case ModelStates.IDLE:
                    model.animationState = AnimationStates.IDLE;
                    break;
                case ModelStates.JUMPING:
                    model.animationState = AnimationStates.JUMPING;
                    break;
            }

            model.aManager.Update(new Tuple<Directions, AnimationStates>(model.direction, model.animationState));
        }

        public override void Draw()
        {
            //Debug.WriteLine(spriteZ);

            base.Draw();
        }

        public override void DrawWeapon()
        {
            if (this.model.modelState == ModelStates.ATTACKING)
            {
                sManager.equipmentManager.Draw(this.model.direction);
            }
        }

    }
}
