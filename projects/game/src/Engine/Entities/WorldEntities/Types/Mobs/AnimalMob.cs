using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utils;
using static Entities.EntityAIBehaviourManager;
using static Entities.BattleMovesetFactory;
using System.Collections.Generic;

namespace Entities
{
    public class AnimalMob : NonEquipmentEntity
    {

        public enum AnimalMobs
        {
            SLIME,
            BAT,
        }

        public AnimalMobs Type;

        public AnimalMob(AnimalMobs mobType, Vector2 pos, float rotation = 0f) : base()
        {
            Type = mobType;
            SetAnimalMobData(out Models modelType);
            Init(modelType, pos, rotation); 
            SetStats();
            SetInventory();
            SetDropInventory();
            SetAI();
        }

        public virtual void SetAnimalMobData(out Models modelType)
        {
            switch (Type)
            {
                case AnimalMobs.SLIME:
                    EntityFraction = EntityFractions.ANIMAL;
                    BloodDropParticle = ParticleSet.ParticleSets.SLIME_BLOOD_SPLASH;
                    modelType = Models.SLIME;

                    break;
                case AnimalMobs.BAT:
                    EntityFraction = EntityFractions.ANIMAL;
                    BloodDropParticle = ParticleSet.ParticleSets.HUMAN_BLOOD_SPLASH;
                    modelType = Models.BAT;

                    
                    break;
                default:
                    modelType = Models.SLIME;
                    break;
            }
        }

        public override void SetStats()
        {
            base.SetStats();

            switch(Type)
            {
                case AnimalMobs.SLIME:


                    StatsManager.Stats = new EntityStat[]
                    {
                        new EntityStat(EntityStats.HP, 100, 100),
                        new EntityStat(EntityStats.MANA, 100, 100),
                        new EntityStat(EntityStats.STAMINA, 100, 100),
                        new EntityStat(EntityStats.MOVEMENT_SPEED, 0.25f, 0.25f),
                        new EntityStat(EntityStats.JUMP_SPEED, 2.5f, 2.5f, 60),
                        new EntityStat(EntityStats.POISE, 100, 100, 3),
                        new EntityStat(EntityStats.AGGRO_RANGE, 200, 200),
                        new EntityStat(EntityStats.UNAGGRO_RANGE, 500, 500)
                    };

                    StatsManager.BodyHitStatsSet = new BattleHitStatsSet(new DamageSet(5, 0), new DefenseSet(0, 0), new StatsCostSet(0, 25, 0), 20, 1);

                    StatsManager.Abilities = new EntityAbility[]
                    {
                        new InvincibleFramesAbility(1f),
                        new FallAbility(),
                        new StaminaRegenerationAbility(3, 1.5f),
                        new GCSRectanglesCalculatorAbility(),
                        new DescencionAbility(0.5f, 1f),
                    };

                    break;

                case AnimalMobs.BAT:


                    StatsManager.Stats = new EntityStat[]
                    {
                        new EntityStat(EntityStats.HP, 100, 100),
                        new EntityStat(EntityStats.MANA, 100, 100),
                        new EntityStat(EntityStats.STAMINA, 100, 100),
                        new EntityStat(EntityStats.MOVEMENT_SPEED, 0.5f, 0.25f),
                        new EntityStat(EntityStats.JUMP_SPEED, 2.8f, 2.5f, 60),
                        new EntityStat(EntityStats.POISE, 100, 100, 3),
                        new EntityStat(EntityStats.AGGRO_RANGE, 200, 200),
                        new EntityStat(EntityStats.UNAGGRO_RANGE, 500, 500),
                        new EntityStat(EntityStats.FLY_SPEED, 0.5f, 0.5f)
                    };

                    StatsManager.BodyHitStatsSet = new BattleHitStatsSet(new DamageSet(5, 0), new DefenseSet(0, 0), new StatsCostSet(0, 25, 0), 20, 1);

                    StatsManager.Abilities = new EntityAbility[]
                    {
                        new InvincibleFramesAbility(1f),
                        new FallAbility(),
                        new StaminaRegenerationAbility(3, 1.5f),
                        new GCSRectanglesCalculatorAbility(),
                        new DescencionAbility(0.5f, 1f),
                        new FlyAbility()
                    };

                    break;
            }

            StatsManager.RefillAll();

        }

        public override void SetAI()
        {
            BehaviourPatterns pattern = BehaviourPatterns.ANIMAL_WALKING;

            switch (Type)
            {
                case AnimalMobs.SLIME:
                    pattern = BehaviourPatterns.ANIMAL_WALKING;
                    break;
                case AnimalMobs.BAT:
                    pattern = BehaviourPatterns.ANIMAL_FLYING;
                    break;
            }


            EntityFraction = EntityFractions.ANIMAL;
            AISet = new EntityAISet(this, pattern, BehaviourCases.IDLE_RANDOM);
        }


        public override void SetAnimations()
        {

            switch (Type)
            {
                case AnimalMobs.SLIME:

                    Model.AManagers = new Animator[]
                    {
                        new Animator
                        (
                            Model.SpriteData.SpriteSheet,
                            new List<Animation>
                            {
                                //idle
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 0), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 0), new Vector2(64, 64), 0.5f)),

                                //moving
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //jumping
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                
                                //roll
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //fallen
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SLIME_BODY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SLIME_BODY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                            }
                        )
                    };

                    break;

                case AnimalMobs.BAT:


                    Model.AManagers = new Animator[]
                    {
                        new Animator
                        (
                            Model.SpriteData.SpriteSheet,
                            new List<Animation>
                            {
                                //idle
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 0), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 0), new Vector2(64, 64), 0.5f)),

                                //moving
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64), new Vector2(0, -16), new Vector2(64, 64), Vector2.Zero, 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //jumping
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                
                                //fly
                                new Animation(new AnimationKey(AnimationStates.FLYING, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.FLYING, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //fly move
                                new Animation(new AnimationKey(AnimationStates.FLYING_AND_MOVING, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.FLYING_AND_MOVING, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //roll
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //fallen
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SLIME_BODY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SLIME_BODY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                            }
                        )
                    };

                    break;
            }
            
        }

        public override void SetBattleBodies()
        {
            BattleBodyData battleBodyData = new BattleBodyData();
            battleBodyData.Sprite = StaticSprites.NONE;
            battleBodyData.WeaponSwingSpeedMultiplier = 1f;
            battleBodyData.MoveSet = BattleMovesets.BODY_SLIME;
            battleBodyData.WeaponOutAnimationData = new Graphics.AnimationFramesData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
            battleBodyData.ModelStateBetweenHits = ModelStates.IDLE;

            BattleBodyManager = new BattleBodyManager(BattleBodyTypes.BODY);
            BattleBodyManager.InitBody(0, battleBodyData);
        }

        public override void Update()
        {
            //Console.WriteLine(Model.ModelState);
            base.Update();
        }
    }
}
