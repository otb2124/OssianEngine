using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using System;
using Utils;
using static Entities.EntityAIBehaviourManager;
using static Entities.WeaponComboMovesetFactory;

namespace Entities
{
    public class AnimalMob : NonEquipmentEntity
    {


        public AnimalMob(Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
        {
            BloodDropParticle = ParticleSet.ParticleSets.SLIME_BLOOD_SPLASH;
        }

        public override void SetStats()
        {
            base.SetStats();

            CanRegensStamina = true;
            CanUpdateIFrames = true;
            CanFall = true;

            Stats.maxHP = 50;
            Stats.maxSpeed = 0.25f;
            Stats.jumpSpeed = 2.5f;
            Stats.MaxPoise = 100f;
            Stats.PoiseRegenSec = 3;

            Stats.Refill();

            Stats.bodyKnockbackPower = 1;
            Stats.bodyDamage = 10;

            Stats.DistanceToAggro = 200f;
            Stats.DistanceToUnaggro = 500f;
        }

        public override void SetAI()
        {
            EntityFraction = EntityFractions.ANIMAL;
            AISet = new EntityAISet(this, BehaviourPatterns.ANIMAL_DEFAULT, BehaviourCases.IDLE_RANDOM);
        }


        public override void SetAnimations()
        {
            float frameSpeed = 0;
            //IDLE
            frameSpeed = 0.5f;
            Model.aManager.AddAnimation(Model.spriteData, Directions.LEFT, AnimationStates.IDLE, 2, new Vector2(0, 0), new Vector2(64, 64), frameSpeed, SpriteEffects.FlipHorizontally);
            Model.aManager.AddAnimation(Model.spriteData, Directions.RIGHT, AnimationStates.IDLE, 2, new Vector2(0, 0), new Vector2(64, 64), frameSpeed, SpriteEffects.None);

            //MOVING
            frameSpeed = 0.5f;
            Model.aManager.AddAnimation(Model.spriteData, Directions.LEFT, AnimationStates.MOVING, 2, new Vector2(0, 0), new Vector2(64, 64), frameSpeed, SpriteEffects.FlipHorizontally);
            Model.aManager.AddAnimation(Model.spriteData, Directions.RIGHT, AnimationStates.MOVING, 2, new Vector2(0, 0), new Vector2(64, 64), frameSpeed, SpriteEffects.None);

            //JUMPING
            frameSpeed = 0.5f;
            Model.aManager.AddAnimation(Model.spriteData, Directions.LEFT, AnimationStates.JUMPING, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed, SpriteEffects.FlipHorizontally);
            Model.aManager.AddAnimation(Model.spriteData, Directions.RIGHT, AnimationStates.JUMPING, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed, SpriteEffects.None);

            //ROLL
            frameSpeed = 0.5f;
            Model.aManager.AddAnimation(Model.spriteData, Directions.LEFT, AnimationStates.ROLL, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed, SpriteEffects.FlipHorizontally);
            Model.aManager.AddAnimation(Model.spriteData, Directions.RIGHT, AnimationStates.ROLL, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed, SpriteEffects.None);

            //FALLEN
            frameSpeed = 0.5f;
            Model.aManager.AddAnimation(Model.spriteData, Directions.LEFT, AnimationStates.FALLEN, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed, SpriteEffects.FlipHorizontally);
            Model.aManager.AddAnimation(Model.spriteData, Directions.RIGHT, AnimationStates.FALLEN, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed, SpriteEffects.None);

            //TODO: GET RID OF
            //weapon out for battle mode
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.WEAPON_OUT_IDLE, 1, new Vector2(0, 128 * 11), new Vector2(64, 128), frameSpeed);

            //attacking
            frameSpeed = 2.5f;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.BLOCKING_SLIME_BODY, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SLIME_BODY_LIGHT, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SLIME_BODY_HEAVY, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
        }

        public override void SetBattleBodies()
        {
            BattleBodyData battleBodyData = new BattleBodyData();
            battleBodyData.Sprite = StaticSprites.NONE;
            battleBodyData.WeaponSwingSpeedMultiplier = 1f;
            battleBodyData.MoveSet = BattleMovesets.BODY_SLIME;
            battleBodyData.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);

            BattleBodyManager = new BattleBodyManager(BattleBodyTypes.BODY);
            BattleBodyManager.InitBody(0, battleBodyData);
        }
    }
}
