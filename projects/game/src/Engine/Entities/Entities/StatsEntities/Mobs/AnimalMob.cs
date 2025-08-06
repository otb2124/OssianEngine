using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using System;
using Utils;
using static Entities.EntityAIBehaviourManager;

namespace Entities
{
    public class AnimalMob : NonHumanoidEntity
    {

        public EntityAIManager aiManager;
        public BehaviourCases CurrentBehaviourCase;

        public AnimalMob(Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
        {
            EntityFraction = EntityFractions.ANIMAL;
            aiManager = new EntityAIManager(BehaviourPatterns.ANIMAL_DEFAULT);
            CurrentBehaviourCase = BehaviourCases.IDLE_RANDOM;
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

            Stats.Refill();

            Stats.bodyKnockbackPower = 1;
            Stats.bodyDamage = 10;
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
        }

        public override void Update()
        {
            aiManager.Update(this);

            UpdateBodyHitbox(FlatConverter.ToVector2(this.Model.Body.Position), new Vector2(this.Model.Body.Width, this.Model.Body.Height), Model.Body.Angle);
            UpdateDamageHitbox(FlatConverter.ToVector2(this.Model.Body.Position), new Vector2(this.Model.Body.Width, this.Model.Body.Height), 0f);

            Model.aManager.Update(new Tuple<Directions, AnimationStates>(Model.Direction, Model.animationState));

            base.Update();
        }
    }
}
