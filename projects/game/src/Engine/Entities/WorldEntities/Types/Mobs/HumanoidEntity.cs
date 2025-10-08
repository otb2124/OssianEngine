using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System;
using UI;
using Utils;
using static Entities.EntityAIBehaviourManager;


namespace Entities
{
    public class HumanoidEntity : EquipmentEntity
    {

        public enum HumanoidMobs
        {
            CITIZEN,
            BANDIT,
            VIGO,
            WANEGRO,
        }


        public HumanoidMobs Type;

        public HumanoidEntity(HumanoidMobs type, Vector2 pos, float rotation) : base()
        {
            Type = type;

            EntityFraction = EntityFractions.BANDIT;
            BloodDropParticle = ParticleSet.ParticleSets.HUMAN_BLOOD_SPLASH;
            Init(Models.BANDIT, pos, rotation);

            SetStats();
            SetInventory();
            SetDropInventory();
            SetAI();
            SetInteractionType();
        }

        public override void SetInventory()
        {
            switch (Type)
            {
                case HumanoidMobs.CITIZEN:
                    base.SetInventory();

                    Inventory.Init(40);

                    Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Consumables.HEALTH_POTION)));
                    Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE)));
                    Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TORCH)));
                    break;
                case HumanoidMobs.VIGO:
                    base.SetInventory();

                    Inventory.Init(40);

                    Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Consumables.HEALTH_POTION)));
                    Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE)));
                    Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TORCH)));
                    break;
                case HumanoidMobs.WANEGRO:
                    base.SetInventory();

                    Inventory.Init(40);

                    Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Consumables.HEALTH_POTION)));
                    Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE)));
                    Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TORCH)));
                    break;
                default:
                    break;
            }
        }

        public override void SetAI()
        {

            switch (Type)
            {
                case HumanoidMobs.CITIZEN:
                    AISet = new EntityAISet(this, BehaviourPatterns.BANDIT_DEFAULT, BehaviourCases.STILL);
                    break;
                case HumanoidMobs.BANDIT:
                    AISet = new EntityAISet(this, BehaviourPatterns.BANDIT_DEFAULT, BehaviourCases.IDLE_RANDOM);
                    break;
                case HumanoidMobs.VIGO:
                    AISet = new EntityAISet(this, BehaviourPatterns.BANDIT_DEFAULT, BehaviourCases.STILL);
                    break;
                case HumanoidMobs.WANEGRO:
                    AISet = new EntityAISet(this, BehaviourPatterns.BANDIT_DEFAULT, BehaviourCases.STILL);
                    break;
                default:
                    break;
            }
        }

        public override void SetInteractionType()
        {
            switch (Type)
            {
                case HumanoidMobs.CITIZEN:
                    break;
                case HumanoidMobs.VIGO:
                    InteractionManager = new InteractionManager(new InteractionData(InteractionTriggers.INTERACTION_BUTTON_PRESSED, new int[] { 0, 1 }, 0));
                    break;
                case HumanoidMobs.WANEGRO:
                    InteractionManager = new InteractionManager(new InteractionData(InteractionTriggers.INTERACTION_BUTTON_PRESSED, new int[] { 100 }, 1));
                    break;
                default:
                    break;
            }
        }

        public override void SetAnimations()
        {
            float frameSpeed = 0;
            //idle
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.IDLE, 9, new Vector2(0, 0), new Vector2(64, 128), frameSpeed);

            //move
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.MOVING, 9, new Vector2(0, 128), new Vector2(64, 128), frameSpeed);

            //jump
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.JUMPING, 1, new Vector2(0, 128 * 2), new Vector2(64, 128), frameSpeed);

            //fallen
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.FALLEN, 1, new Vector2(0, 128 * 7), new Vector2(64, 128), frameSpeed);

            //battleRoll
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ROLL, 9, new Vector2(0, 128 * 6), new Vector2(64, 128), frameSpeed);

            //weapon out
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.WEAPON_OUT_IDLE, 1, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);

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
        }


        public override void SetStats()
        {
            base.SetStats();

            switch(Type)
            {
                case HumanoidMobs.BANDIT:
                    CanRegensStamina = true;
                    CanUpdateIFrames = true;
                    CanFall = true;

                    Stats.sprintMultiplier = 1.5f;
                    Stats.staminaSprintCostSec = 15;

                    Stats.staminaRegenSec = 20;
                    Stats.staminaUnlockSec = 1.5f;

                    Stats.staminaAttackHitCostMultiplier = 25;

                    Stats.rollMultiplier = 2f;
                    Stats.staminaRollCostSec = 200;

                    Stats.jumpSpeed = 2.8f;
                    Stats.staminaJumpCostSec = 60;

                    Stats.maxHP = 100;
                    Stats.maxSpeed = 0.5f;
                    Stats.maxMana = 100;
                    Stats.maxStamina = 100;
                    Stats.MaxPoise = 100;
                    Stats.PoiseRegenSec = 10;

                    Stats.DistanceToAggro = 200f;
                    Stats.DistanceToUnaggro = 500f;
                    break;

                case HumanoidMobs.CITIZEN:
                    CanRegensStamina = true;
                    CanUpdateIFrames = true;
                    CanFall = true;

                    Stats.sprintMultiplier = 1.5f;
                    Stats.staminaSprintCostSec = 15;

                    Stats.staminaRegenSec = 20;
                    Stats.staminaUnlockSec = 1.5f;

                    Stats.staminaAttackHitCostMultiplier = 25;

                    Stats.rollMultiplier = 2f;
                    Stats.staminaRollCostSec = 200;

                    Stats.jumpSpeed = 2.8f;
                    Stats.staminaJumpCostSec = 60;

                    Stats.maxHP = 100;
                    Stats.maxSpeed = 0.5f;
                    Stats.maxMana = 100;
                    Stats.maxStamina = 100;
                    Stats.MaxPoise = 100;
                    Stats.PoiseRegenSec = 10;

                    break;

                case HumanoidMobs.VIGO:
                    CanRegensStamina = true;
                    CanUpdateIFrames = true;
                    CanFall = true;

                    Stats.sprintMultiplier = 1.5f;
                    Stats.staminaSprintCostSec = 15;

                    Stats.staminaRegenSec = 20;
                    Stats.staminaUnlockSec = 1.5f;

                    Stats.staminaAttackHitCostMultiplier = 25;

                    Stats.rollMultiplier = 2f;
                    Stats.staminaRollCostSec = 200;

                    Stats.jumpSpeed = 2.8f;
                    Stats.staminaJumpCostSec = 60;

                    Stats.maxHP = 100;
                    Stats.maxSpeed = 0.5f;
                    Stats.maxMana = 100;
                    Stats.maxStamina = 100;
                    Stats.MaxPoise = 100;
                    Stats.PoiseRegenSec = 10;

                    break;

                case HumanoidMobs.WANEGRO:
                    CanRegensStamina = true;
                    CanUpdateIFrames = true;
                    CanFall = true;

                    Stats.sprintMultiplier = 1.5f;
                    Stats.staminaSprintCostSec = 15;

                    Stats.staminaRegenSec = 20;
                    Stats.staminaUnlockSec = 1.5f;

                    Stats.staminaAttackHitCostMultiplier = 25;

                    Stats.rollMultiplier = 2f;
                    Stats.staminaRollCostSec = 200;

                    Stats.jumpSpeed = 2.8f;
                    Stats.staminaJumpCostSec = 60;

                    Stats.maxHP = 100;
                    Stats.maxSpeed = 0.5f;
                    Stats.maxMana = 100;
                    Stats.maxStamina = 100;
                    Stats.MaxPoise = 100;
                    Stats.PoiseRegenSec = 10;

                    break;
            }

            Stats.Refill();

            BloodDropParticle = ParticleSet.ParticleSets.HUMAN_BLOOD_SPLASH;
        }


        public override void SetEquipment()
        {
            base.SetEquipment();

            EquipmentManager.SetWeapon(BattleBodyManager, (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.BARE_HAND)), WeaponHands.LEFT);
            EquipmentManager.Equipments.GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes.CHESTPLATE).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Chestplates.IRON_CHESTPLATE));
        }


        public override void SetDropInventory()
        {
            base.SetDropInventory();

            DropInventory.AddDrop(new Drop(new ItemKey(ItemLib.Materials.SWORD_HILT), 0.99f));
            DropInventory.AddDrop(new Drop(new ItemKey(ItemLib.Capes.LEATHER_CAPE), 0.25f));
        }
    }
}
