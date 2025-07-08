using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Utils;

namespace Entities
{
    public class Player : EquipmentEntity
    {

        public Player() : base(Models.PLAYER, Vector2.Zero, 0f)
        {
            
        }

        public override void SetStats()
        {
            base.SetStats();

            Stats.sprintMultiplier = 1.5f;
            Stats.staminaSprintCostSec = 15 ;

            Stats.staminaRegenSec = 20;
            Stats.staminaUnlockSec = 1.5f;

            Stats.staminaAttackCost = 40;

            Stats.rollMultiplier = 2f;
            Stats.staminaRollCostSec = 200;

            Stats.jumpSpeed = 2.5f;
            Stats.staminaJumpCostSec = 60;

            Stats.maxHP = 100;
            Stats.maxSpeed = 2;
            Stats.maxMana = 100;
            Stats.maxStamina = 100;

            Stats.Refill();

            EntityFraction = EntityFractions.PLAYER;
        }


        public override void SetEquipment()
        {
            base.SetEquipment();

            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment = (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_CHESTPLATE));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.HELMET).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_HELMET));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.GLOVES).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_GLOVES));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.BOOTS).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_BOOTS));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CAPE).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.LEATHER_CAPE));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.NECKLACE).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.IRON_NECKLACE));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.BELT).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.IRON_BELT));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.RING_L).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.IRON_RING));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.RING_R).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.IRON_RING));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CONTAINMENT).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.BACKPACK));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.PET).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.CALL_DOG));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.PET_LIGHT).Equipment = (AccessoryEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Accessories.CALL_FIREFLY));
        }

        public override void SetInventory()
        {
            base.SetInventory();

            Inventory.SlotsAmount = 42;
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Consumables.HEALTH_POTION)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
        }


        public override void SetAnimations()
        {
            float frameSpeed = 0;
            //idle
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.IDLE, 9, new Vector2(0, 0), new Vector2(64, 128), frameSpeed);

            //move
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.MOVING, 9, new Vector2(0, 128), new Vector2(64, 128), frameSpeed);

            //jump
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.JUMPING, 1, new Vector2(0, 128*2), new Vector2(64, 128), frameSpeed);

            //sprint
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.SPRINTING, 9, new Vector2(0, 128 * 3), new Vector2(64, 128), frameSpeed);

            //battleIdle
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.BATTLE_IDLE, 9, new Vector2(0, 128 * 4), new Vector2(64, 128), frameSpeed);

            //battleMoving
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.BATTLE_MOVING, 9, new Vector2(0, 128 * 5), new Vector2(64, 128), frameSpeed);

            //battleRoll
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.BATTLE_ROLL, 9, new Vector2(0, 128 * 6), new Vector2(64, 128), frameSpeed);
        }


        public override void Update()
        {

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.TOGGLEWEAPONPRESSED])
            {
                EquipmentManager.IsWeaponOut = !EquipmentManager.IsWeaponOut;
            }


            if (KeyHandlerUtil.isPlayerMoving())
            {
                UpdateMovement();
            }
            else
            {
                if(!(Model.modelState == ModelStates.ATTACKING))
                {
                    Model.modelState = ModelStates.IDLE;

                    if(EquipmentManager.IsWeaponOut)
                    {
                        Model.modelState = ModelStates.BATTLE_IDLE;
                    }
                }


                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKPRESSED] && (Stats.stamina - Stats.staminaAttackCost) > 0 && EquipmentManager.IsWeaponOut)
                {
                    Stats.stamina -= Stats.staminaAttackCost;
                    Model.modelState = ModelStates.ATTACKING;
                }
            }

            base.Update();
        }


        public void UpdateMovement()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                Model.direction = Directions.RIGHT;

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.SPRINTPRESSED] && (Stats.stamina - Stats.staminaSprintCostSec / 60) > 0 && !Stats.OnStaminaRegen && !EquipmentManager.IsWeaponOut)
                {
                    Model.body.Move(new FlatVector(Stats.speed * Stats.sprintMultiplier, 0));
                    Model.modelState = ModelStates.SPRINTING;
                    Stats.OnUsingStamina = true;
                    Stats.stamina -= Stats.staminaSprintCostSec / 60;
                }
                else
                {
                    Model.body.Move(new FlatVector(Stats.speed, 0));
                    Model.modelState = ModelStates.MOVING;

                    if (EquipmentManager.IsWeaponOut)
                    {
                        Model.modelState = ModelStates.BATTLE_MOVING;
                    }
                }
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
            {
                Model.direction = Directions.LEFT;

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.SPRINTPRESSED] && (Stats.stamina - Stats.staminaSprintCostSec / 60) > 0 && !Stats.OnStaminaRegen && !EquipmentManager.IsWeaponOut)
                {
                    Model.body.Move(new FlatVector(-Stats.speed * Stats.sprintMultiplier, 0));
                    Model.modelState = ModelStates.SPRINTING;
                    Stats.OnUsingStamina = true;
                    Stats.stamina -= Stats.staminaSprintCostSec / 60;
                }
                else
                {
                    Model.body.Move(new FlatVector(-Stats.speed, 0));
                    Model.modelState = ModelStates.MOVING;

                    if (EquipmentManager.IsWeaponOut)
                    {
                        Model.modelState = ModelStates.BATTLE_MOVING;
                    }
                }
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED] && (Stats.stamina - (Stats.staminaJumpCostSec / 60)) > 0 && !EquipmentManager.IsWeaponOut)
            {
                Model.body.Jump(Stats.jumpSpeed);
                Model.modelState = ModelStates.JUMPING;
                Stats.stamina -= Stats.staminaJumpCostSec / 60;
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED] && (Stats.stamina - (Stats.staminaRollCostSec / 60)) > 0 && EquipmentManager.IsWeaponOut)
            {

                if (Model.direction == Directions.RIGHT)
                {
                    Model.body.Move(new FlatVector(Stats.speed * Stats.rollMultiplier, 0));
                }
                else
                {
                    Model.body.Move(new FlatVector(-Stats.speed * Stats.rollMultiplier, 0));
                }

                Model.modelState = ModelStates.BATTLE_ROLL;
                Stats.stamina -= Stats.staminaRollCostSec / 60;
            }
        }

        public override void Draw()
        {
            //Debug.WriteLine(spriteZ);
            base.Draw();
        }

        public override void DrawWeapon()
        {
            if (this.Model.modelState == ModelStates.ATTACKING)
            {
                EquipmentManager.Draw(this.Model.direction);
            }
        }

    }
}
