using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using Utils;

namespace Entities
{
    public class Player : EquipmentEntity
    {

        public Player() : base(Models.PLAYER, Vector2.Zero, 0f)
        {
            SetStats();
            SetInventory();
            //SetDropInventory();
        }

        public override void SetStats()
        {
            base.SetStats();

            CanRegensStamina = true;
            CanUpdateIFrames = true;
            CanFall = true;
            CanHangLedges = true;

            Stats.sprintMultiplier = 1.5f;
            Stats.staminaSprintCostSec = 15;

            Stats.staminaRegenSec = 20;
            Stats.staminaUnlockSec = 1.5f;

            Stats.staminaAttackHitCostMultiplier = 1f;

            Stats.rollMultiplier = 2f;
            Stats.staminaRollCostSec = 200;

            Stats.jumpSpeed = 2.8f;
            Stats.DescendingMultiplier = 1f;
            Stats.staminaJumpCostSec = 60;

            Stats.maxHP = 100;
            Stats.maxSpeed = 1;
            Stats.maxMana = 100;
            Stats.maxStamina = 100;
            Stats.MaxPoise = 100;
            Stats.PoiseRegenSec = 10;
            Stats.MaxDescendingSec = 0.5f;

            Stats.Refill();

            EntityFraction = EntityFractions.PLAYER;
            BloodDropParticle = ParticleSet.ParticleSets.HUMAN_BLOOD_SPLASH;
        }


        public override void SetEquipment()
        {
            base.SetEquipment();

            EquipmentManager.SetWeapon(BattleBodyManager, (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE)), WeaponHands.LEFT);

            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Helmets.IRON_HELMET));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Chestplates.IRON_CHESTPLATE));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Gloves.IRON_GLOVES));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Boots.IRON_BOOTS));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Capes.LEATHER_CAPE));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Necklaces.IRON_NECKLACE));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Belts.IRON_BELT));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Rings.IRON_RING));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Rings.IRON_RING));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Containments.BACKPACK));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.Pets.CALL_DOG));
            EquipmentManager.Equipments.SetEquipment(new ItemKey(ItemLib.LightPets.CALL_FIREFLY));
        }

        public override void SetInventory()
        {
            base.SetInventory();

            Inventory.Init(40);

            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Consumables.HEALTH_POTION)));
            //Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TORCH)));
        }


        public override void SetAnimations()
        {
            float frameSpeed = 0;
            //idle
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.IDLE, 9, new Vector2(0, 0), new Vector2(64, 128), frameSpeed);

            //move
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.MOVING, 9, new Vector2(0, 128), new Vector2(64, 128), frameSpeed);

            //jump
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.JUMPING, 1, new Vector2(0, 128*2), Vector2.Zero, new Vector2(64, 128), new Vector2(32, 0), frameSpeed);

            //sprint
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.SPRINTING, 9, new Vector2(0, 128 * 3), new Vector2(64, 128), frameSpeed);

            //battleIdle
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.WEAPON_OUT_IDLE, 9, new Vector2(0, 128 * 4), new Vector2(64, 128), frameSpeed);

            //battleMoving
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.WEAPON_OUT_MOVING, 9, new Vector2(0, 128 * 5), new Vector2(64, 128), frameSpeed);

            //battleRoll
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ROLL, 9, new Vector2(0, 128 * 6), new Vector2(64, 128), frameSpeed);

            //fallen
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.FALLEN, 1, new Vector2(0, 128 * 7), new Vector2(64, 128), frameSpeed);

            //fallen
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.OVERALL_DESCENDING, 1, new Vector2(0, 128 * 9), new Vector2(64, 128), frameSpeed);

            //fallen
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.JUMPING_DESCENDING, 1, new Vector2(0, 128 * 10), Vector2.Zero, new Vector2(64, 128), new Vector2(32, 0), frameSpeed);

            //fallen
            frameSpeed = 0.04f;
            Model.aManager.AddAnimation(Model.SpriteData, Directions.RIGHT, AnimationStates.HANGING_ON_LEDGE_LEFT, 1, new Vector2(0, 128 * 12), new Vector2(64, 128), frameSpeed, SpriteEffects.None);
            Model.aManager.AddAnimation(Model.SpriteData, Directions.LEFT, AnimationStates.HANGING_ON_LEDGE_LEFT, 1, new Vector2(0, 128 * 13), new Vector2(64, 128), frameSpeed, SpriteEffects.None);

            //fallen
            frameSpeed = 0.04f;
            Model.aManager.AddAnimation(Model.SpriteData, Directions.LEFT, AnimationStates.HANGING_ON_LEDGE_RIGHT, 1, new Vector2(0, 128 * 12), new Vector2(64, 128), frameSpeed, SpriteEffects.FlipHorizontally);
            Model.aManager.AddAnimation(Model.SpriteData, Directions.RIGHT, AnimationStates.HANGING_ON_LEDGE_RIGHT, 1, new Vector2(0, 128 * 13), new Vector2(64, 128), frameSpeed, SpriteEffects.FlipHorizontally);

            //attacking
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.BLOCKING_SWORD, 1, new Vector2(0, 128 * 11), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SWORD_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SWORD_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);

            //attacking
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.BLOCKING_KNIFE, 1, new Vector2(0, 128 * 11), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_KNIFE_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_KNIFE_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);

            //attacking
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.BLOCKING_BARE_HANDS, 1, new Vector2(0, 128 * 11), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_BARE_HANDS_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_BARE_HANDS_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
        }

        public override void SetSounds()
        {
            base.SetSounds();
            
            soundSet[EntitySounds.STEP] = new Resources.Sounds[] { Resources.Sounds.FOOT_STONE_W1, Resources.Sounds.FOOT_STONE_W2, Resources.Sounds.FOOT_STONE_W3 };
            soundSet[EntitySounds.RECEIVEDAMAGE] = new Resources.Sounds[] { Resources.Sounds.HUMANOID_HURT };
            soundSet[EntitySounds.JUMP] = new Resources.Sounds[] { Resources.Sounds.FOOT_SOIL_R1, Resources.Sounds.FOOT_SOIL_R2, Resources.Sounds.FOOT_SOIL_R3, Resources.Sounds.FOOT_SOIL_R4 };
            soundSet[EntitySounds.WEAPON_SWING] = new Resources.Sounds[] { Resources.Sounds.SWING_SWORD, Resources.Sounds.SWING_SWORD2 };
        }

        public override void SetEmission()
        {
            //Emission = new LightSource.LightSourceData(LightSource.LightSourceData.LightSourceForms.CIRCULAR, new Vector2(50f, 0f), Vector2.Zero, new Color(1f, 1f, 0.8f, 0.7f), 50f, 0f);
            base.SetEmission();
        }

        public override void Die()
        {
            if(!GameStateManager.IsGod)
            {
                base.Die();
            }
        }

        public override void Update()
        {
            EntityModelStateHandler.UpdatePlayerModelState(this);
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
