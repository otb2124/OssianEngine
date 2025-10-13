using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utils;
using static Entities.EntityAIBehaviourManager;
using static Entities.BattleMovesetFactory;

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

            switch(Type)
            {
                case AnimalMobs.SLIME:
                    float frameSpeed = 0;
                    //IDLE
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.IDLE, 2, new Vector2(0, 0), new Vector2(64, 64), frameSpeed);

                    //MOVING
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.MOVING, 2, new Vector2(0, 0), new Vector2(64, 64), frameSpeed);

                    //JUMPING
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.JUMPING, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);

                    //ROLL
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ROLL, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);

                    //FALLEN
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.FALLEN, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);

                    //attacking
                    frameSpeed = 2.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.BLOCKING_SLIME_BODY, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_LIGHT, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_HEAVY, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    break;

                case AnimalMobs.BAT:
                    frameSpeed = 0;
                    //IDLE
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.IDLE, 1, new Vector2(0, 0), new Vector2(64, 64), frameSpeed);

                    //MOVING
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.MOVING, 3, new Vector2(0, 64), new Vector2(0, -16), new Vector2(64, 64), Vector2.Zero, frameSpeed);

                    //JUMPING
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.JUMPING, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);

                    //FLYING
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.FLYING, 3, new Vector2(0, 64*3), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.FLYING_AND_MOVING, 3, new Vector2(0, 64*2), new Vector2(64, 64), frameSpeed);

                    //ROLL
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ROLL, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);

                    //FALLEN
                    frameSpeed = 0.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.FALLEN, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);

                    //attacking
                    frameSpeed = 2.5f;
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.BLOCKING_SLIME_BODY, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_LIGHT, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_HEAVY, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, 3, new Vector2(0, 64), new Vector2(64, 64), frameSpeed);
                    break;
            }
            
        }

        public override void SetBattleBodies()
        {
            BattleBodyData battleBodyData = new BattleBodyData();
            battleBodyData.Sprite = StaticSprites.NONE;
            battleBodyData.WeaponSwingSpeedMultiplier = 1f;
            battleBodyData.MoveSet = BattleMovesets.BODY_SLIME;
            battleBodyData.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
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
