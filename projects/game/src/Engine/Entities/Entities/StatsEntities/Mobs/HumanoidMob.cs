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
    public class HumanoidMob : EquipmentEntity
    {

        public enum HumanoidMobs
        {
            CITIZEN,
            BANDIT,
        }


        public HumanoidMobs Type;
        public EntityAISet AISet;


        public HumanoidMob(HumanoidMobs type, Vector2 pos, float rotation) : base()
        {
            Type = type;
            SetHumanoidMobData(out Models modelType);
            Init(modelType, pos, rotation);
        }

        public void SetHumanoidMobData(out Models modelType)
        {
            switch(Type)
            {
                case HumanoidMobs.CITIZEN:
                    EntityFraction = EntityFractions.BANDIT;
                    AISet = new EntityAISet(this, BehaviourPatterns.BANDIT_DEFAULT, BehaviourCases.IDLE_RANDOM);
                    BloodDropParticle = ParticleSet.ParticleSets.HUMAN_BLOOD_SPLASH;
                    modelType = Models.BANDIT;
                    break;
                case HumanoidMobs.BANDIT:
                    EntityFraction = EntityFractions.BANDIT;
                    AISet = new EntityAISet(this, BehaviourPatterns.BANDIT_DEFAULT, BehaviourCases.IDLE_RANDOM);
                    BloodDropParticle = ParticleSet.ParticleSets.HUMAN_BLOOD_SPLASH;
                    modelType = Models.BANDIT;

                    Stats.DistanceToAggro = 200f;
                    Stats.DistanceToUnaggro = 500f;
                    break;
                default:
                    modelType = Models.BANDIT; 
                    break;
            }
        }

        public override void SetAnimations()
        {
            float frameSpeed = 0;
            //idle
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.IDLE, 9, new Vector2(0, 0), new Vector2(64, 128), frameSpeed);

            //move
            frameSpeed = 0.1f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.MOVING, 9, new Vector2(0, 128), new Vector2(64, 128), frameSpeed);

            //jump
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.JUMPING, 1, new Vector2(0, 128 * 2), new Vector2(64, 128), frameSpeed);

            //fallen
            frameSpeed = 0.04f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.FALLEN, 1, new Vector2(0, 128 * 7), new Vector2(64, 128), frameSpeed);

            //battleRoll
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ROLL, 9, new Vector2(0, 128 * 6), new Vector2(64, 128), frameSpeed);

            //weapon out
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.WEAPON_OUT_IDLE, 1, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);

            //attacking
            frameSpeed = 0.15f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SWORD_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_LIGHT, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SWORD_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY, 3, new Vector2(0, 128 * 8), new Vector2(64, 128), frameSpeed);
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

            CanRegensStamina = true;
            CanUpdateIFrames = true;
            CanFall = true;

            Stats.sprintMultiplier = 1.5f;
            Stats.staminaSprintCostSec = 15;

            Stats.staminaRegenSec = 20;
            Stats.staminaUnlockSec = 1.5f;

            Stats.staminaAttackHitCost = 25;

            Stats.rollMultiplier = 2f;
            Stats.staminaRollCostSec = 200;

            Stats.jumpSpeed = 2.8f;
            Stats.staminaJumpCostSec = 60;

            Stats.maxHP = 100;
            Stats.maxSpeed = 1;
            Stats.maxMana = 100;
            Stats.maxStamina = 100;

            Stats.Refill();
        }
    

        public override void SetEquipment()
        {
            base.SetEquipment();

            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment = (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_CHESTPLATE));
        }


        public override void Update()
        {
            if(!Stats.IsFallen && !Stats.IsFalling)
            {
                AISet.Update(this);
            }

            Model.aManager.Update(new Tuple<Directions, AnimationStates>(Model.Direction, Model.animationState));

            base.Update();
        }


        public override void Draw()
        {
            base.Draw();
        }
    }
}
