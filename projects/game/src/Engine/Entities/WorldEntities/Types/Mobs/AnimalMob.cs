using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utils;
using static Entities.EntityAIBehaviourManager;
using static Entities.BattleMovesetFactory;
using System.Collections.Generic;
using Resources;

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
            SetControl();
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
                        new StaminaRegenerationAbility(20, 1.5f),
                        new InvincibleFramesAbility(1f),
                        new FallAbility(),
                        new GCSRectanglesCalculatorAbility(),
                        new DescencionAbility(0.5f, 1f),
                        new DieAbility(),
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
                        new FlyAbility(),
                        new DieAbility(),
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


        public override void SetControl()
        {
            EntityControlHandler = new EntityControlHandler();
        }


        public override void SetAppearance()
        {

            Model.ModelAppearance = new ModelAppearance();

            ModelAppearancePart bodyPart = new ModelAppearancePart(EntityAppearanceAttributes.BODY);

            bodyPart.AddAnimationSet(AnimationSetSetter.CreateAnimationSetBySpriteSheet(Model.SpriteData.SpriteSheet));

            Model.ModelAppearance.AppearanceParts.Add(bodyPart);

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
