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
            statsManager.stats.sprintMultiplier = 1.5f;
            statsManager.stats.enduranceRegenSec = 10;
            statsManager.stats.enduranceSprintCostSec = 40;
            statsManager.stats.enduranceUnlockSec = 1.5f;
            statsManager.stats.enduranceJumpCostSec = 60;
            statsManager.stats.enduranceAttackCost = 40;

            statsManager.stats.maxHP = 100;
            statsManager.stats.maxSpeed = 2;
            statsManager.stats.maxMana = 100;
            statsManager.stats.maxEndurance = 100;
            statsManager.stats.jumpSpeed = 2.5f;

            statsManager.stats.Refill();

            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment = (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_CHESTPLATE));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.HELMET).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_HELMET));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.GLOVES).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_GLOVES));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.BOOTS).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_BOOTS));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CAPE).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.LEATHER_CAPE));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.NECKLACE).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.IRON_NECKLACE));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.BELT).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.IRON_BELT));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.RING_L).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.IRON_RING));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.RING_R).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.IRON_RING));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CONTAINMENT).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.BACKPACK));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.PET).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.CALL_DOG));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.PET_LIGHT).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.CALL_FIREFLY));

            statsManager.inventory.SlotsAmount = 42;
            statsManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Consumables.HEALTH_POTION)));
            statsManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE)));
            statsManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            statsManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            statsManager.inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));

            status = LivingEntityStatus.FRIENDLY;

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

            //jump
            //frameSpeed = 0.1f;
            model.aManager.AddAnimation(model.spriteData, Directions.LEFT, AnimationStates.SPRINTING, 8, new Vector2(0, 128 * 3), new Vector2(64, 128), frameSpeed, SpriteEffects.FlipHorizontally);
            model.aManager.AddAnimation(model.spriteData, Directions.RIGHT, AnimationStates.SPRINTING, 8, new Vector2(0, 128 * 3), new Vector2(64, 128), frameSpeed, SpriteEffects.None);
        }


        public override void Update()
        {

            statsManager.stats.OnUsingEndurance = false;

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


                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKPRESSED] && (statsManager.stats.endurance - statsManager.stats.enduranceAttackCost) > 0)
                {
                    statsManager.stats.endurance -= statsManager.stats.enduranceAttackCost;
                    model.modelState = ModelStates.ATTACKING;
                }
            }

            statsManager.stats.RegenEndurance();

            UpdateHitboxes();

            UpdateAnimationState();
            base.Update();
        }


        public void UpdateMovement()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                model.direction = Directions.RIGHT;

                if(Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.SPRINTPRESSED] && (statsManager.stats.endurance - statsManager.stats.enduranceSprintCostSec / 60) > 0 && !statsManager.stats.OnEnduranceRegen)
                {
                    model.body.Move(new FlatVector(statsManager.stats.speed*statsManager.stats.sprintMultiplier, 0));
                    model.modelState = ModelStates.SPRINTING;
                    statsManager.stats.OnUsingEndurance = true;
                    statsManager.stats.endurance-=statsManager.stats.enduranceSprintCostSec/60;
                }
                else
                {
                    model.body.Move(new FlatVector(statsManager.stats.speed, 0));
                    model.modelState = ModelStates.MOVING;
                }
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
            {
                model.direction = Directions.LEFT;

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.SPRINTPRESSED] && (statsManager.stats.endurance - statsManager.stats.enduranceSprintCostSec/60) > 0 && !statsManager.stats.OnEnduranceRegen)
                {
                    model.body.Move(new FlatVector(-statsManager.stats.speed * statsManager.stats.sprintMultiplier, 0));
                    model.modelState = ModelStates.SPRINTING;
                    statsManager.stats.OnUsingEndurance = true;
                    statsManager.stats.endurance -= statsManager.stats.enduranceSprintCostSec / 60;
                }
                else
                {
                    model.body.Move(new FlatVector(-statsManager.stats.speed, 0));
                    model.modelState = ModelStates.MOVING;
                }
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED] && (statsManager.stats.endurance - (statsManager.stats.enduranceJumpCostSec/60)) > 0)
            {
                model.body.Jump(statsManager.stats.jumpSpeed);
                model.modelState = ModelStates.JUMPING;
                statsManager.stats.endurance -= statsManager.stats.enduranceJumpCostSec/60;
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
                this.statsManager.equipmentManager.GetCurrentWeapon().hitbox.Update(
                    weaponPosition,
                    new Vector2(this.model.body.Width, this.model.body.Height)
                );


                if (!this.statsManager.equipmentManager.GetCurrentWeapon().isSwinging)
                {
                    this.statsManager.equipmentManager.GetCurrentWeapon().Swing();
                }

                this.statsManager.equipmentManager.GetCurrentWeapon().UpdateSwing(this.model.direction);

                if (!this.statsManager.equipmentManager.GetCurrentWeapon().isSwinging)
                {
                    this.model.modelState = ModelStates.IDLE;
                }
            }
            else
            {
                this.statsManager.equipmentManager.GetCurrentWeapon().hitbox.Update(
                new Vector2(0,0),
                new Vector2(0, 0)
                );
                this.statsManager.equipmentManager.GetCurrentWeapon().isSwinging = false;
            }


            //armor
            ((ArmorEquipment)this.statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment).hitbox.Update(
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
                case ModelStates.SPRINTING:
                    model.animationState = AnimationStates.SPRINTING;
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
                statsManager.equipmentManager.Draw(this.model.direction);
            }
        }

    }
}
