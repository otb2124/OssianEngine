using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
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
            Init(Models.HUMAN_M, pos, rotation);

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

                    Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Consumables.HEALTH_POTION)));
                    Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.TERRABLADE)));
                    Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.TORCH)));
                    break;
                case HumanoidMobs.VIGO:
                    base.SetInventory();

                    Inventory.Init(40);

                    Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Consumables.HEALTH_POTION)));
                    Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.TERRABLADE)));
                    Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.TORCH)));
                    break;
                case HumanoidMobs.WANEGRO:
                    base.SetInventory();

                    Inventory.Init(40);

                    Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Consumables.HEALTH_POTION)));
                    Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.TERRABLADE)));
                    Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.TORCH)));
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

        public override void SetAppearance()
        {

            Model.ModelAppearance = new ModelAppearance();

            ModelAppearancePart bodyPart = new ModelAppearancePart(EntityAppearanceAttributes.BODY);

            bodyPart.AddAnimationSet(AnimationSetSetter.CreateAnimationSetBySpriteSheet(Model.SpriteData.SpriteSheet));

            Model.ModelAppearance.AppearanceParts.Add(bodyPart);
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


                    StatsManager.Stats = new EntityStat[]
                    {
                        new EntityStat(EntityStats.HP, 100, 100),
                        new EntityStat(EntityStats.MANA, 100, 100),
                        new EntityStat(EntityStats.STAMINA, 100, 100),
                        new EntityStat(EntityStats.MOVEMENT_SPEED, 0.5f, 0.5f),
                        new EntityStat(EntityStats.JUMP_SPEED, 2.8f, 2.8f, 60),
                        new EntityStat(EntityStats.POISE, 100, 100, 3),
                        new EntityStat(EntityStats.AGGRO_RANGE, 200, 200),
                        new EntityStat(EntityStats.UNAGGRO_RANGE, 500, 500),
                        new EntityStat(EntityStats.ROLL_SPEED_MULTIPLIER, 2, 2, 200),
                        new EntityStat(EntityStats.SPRINT_SPEED_MULTIPLIER, 1.5f, 1.5f, 15)
                    };

                    StatsManager.Abilities = new EntityAbility[]
                    {
                        new StaminaRegenerationAbility(20, 1.5f),
                        new InvincibleFramesAbility(1f),
                        new FallAbility(),
                        new GCSRectanglesCalculatorAbility(),
                        new DescencionAbility(0.5f, 1f)
                    };

                    StatsManager.StatsBattleHitSpendHandler = new StatsBattleHitSpendTool();

                    break;

                case HumanoidMobs.CITIZEN:

                    StatsManager.Stats = new EntityStat[]
                    {
                        new EntityStat(EntityStats.HP, 100, 100),
                        new EntityStat(EntityStats.MANA, 100, 100),
                        new EntityStat(EntityStats.STAMINA, 100, 100),
                        new EntityStat(EntityStats.MOVEMENT_SPEED, 0.5f, 0.5f),
                        new EntityStat(EntityStats.JUMP_SPEED, 2.8f, 2.8f, 60),
                        new EntityStat(EntityStats.POISE, 100, 100, 3),
                        new EntityStat(EntityStats.ROLL_SPEED_MULTIPLIER, 2, 2, 200),
                        new EntityStat(EntityStats.SPRINT_SPEED_MULTIPLIER, 1.5f, 1.5f, 15)
                    };

                    StatsManager.Abilities = new EntityAbility[]
                    {
                        new StaminaRegenerationAbility(20, 1.5f),
                        new InvincibleFramesAbility(1f),
                        new FallAbility(),
                        new GCSRectanglesCalculatorAbility(),
                        new DescencionAbility(0.5f, 1f)
                    };

                    StatsManager.StatsBattleHitSpendHandler = new StatsBattleHitSpendTool();


                    break;

                case HumanoidMobs.VIGO:

                    StatsManager.Stats = new EntityStat[]
                    {
                        new EntityStat(EntityStats.HP, 100, 100),
                        new EntityStat(EntityStats.MANA, 100, 100),
                        new EntityStat(EntityStats.STAMINA, 100, 100),
                        new EntityStat(EntityStats.MOVEMENT_SPEED, 0.5f, 0.5f),
                        new EntityStat(EntityStats.JUMP_SPEED, 2.8f, 2.8f, 60),
                        new EntityStat(EntityStats.POISE, 100, 100, 3),
                        new EntityStat(EntityStats.ROLL_SPEED_MULTIPLIER, 2, 2, 200),
                        new EntityStat(EntityStats.SPRINT_SPEED_MULTIPLIER, 1.5f, 1.5f, 15)
                    };

                    StatsManager.Abilities = new EntityAbility[]
                    {
                        new StaminaRegenerationAbility(20, 1.5f),
                        new InvincibleFramesAbility(1f),
                        new FallAbility(),
                        new GCSRectanglesCalculatorAbility(),
                        new DescencionAbility(0.5f, 1f)
                    };

                    StatsManager.StatsBattleHitSpendHandler = new StatsBattleHitSpendTool();


                    break;

                case HumanoidMobs.WANEGRO:

                    StatsManager.Stats = new EntityStat[]
                    {
                        new EntityStat(EntityStats.HP, 100, 100),
                        new EntityStat(EntityStats.MANA, 100, 100),
                        new EntityStat(EntityStats.STAMINA, 100, 100),
                        new EntityStat(EntityStats.MOVEMENT_SPEED, 0.5f, 0.5f),
                        new EntityStat(EntityStats.JUMP_SPEED, 2.8f, 2.8f, 60),
                        new EntityStat(EntityStats.POISE, 100, 100, 3),
                        new EntityStat(EntityStats.ROLL_SPEED_MULTIPLIER, 2, 2, 200),
                        new EntityStat(EntityStats.SPRINT_SPEED_MULTIPLIER, 1.5f, 1.5f, 15)
                    };

                    StatsManager.Abilities = new EntityAbility[]
                    {
                        new StaminaRegenerationAbility(20, 1.5f),
                        new InvincibleFramesAbility(1f),
                        new FallAbility(),
                        new GCSRectanglesCalculatorAbility(),
                        new DescencionAbility(0.5f, 1f)
                    };

                    StatsManager.StatsBattleHitSpendHandler = new StatsBattleHitSpendTool();


                    break;
            }

            StatsManager.RefillAll();

            BloodDropParticle = ParticleSet.ParticleSets.HUMAN_BLOOD_SPLASH;
        }


        public override void SetEquipment()
        {
            base.SetEquipment();

            EquipmentManager.SetWeapon(BattleBodyManager, (WeaponEquipment)ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.BARE_HAND)), WeaponHands.LEFT);
            EquipmentManager.Equipments.GetEquipmentSlot(EquipmentSlot.EquipmentSlotTypes.CHESTPLATE).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new EquatableKey(ItemLib.Chestplates.IRON_CHESTPLATE));
        }


        public override void SetDropInventory()
        {
            base.SetDropInventory();

            DropInventory.AddDrop(new Drop(new EquatableKey(ItemLib.Materials.SWORD_HILT), 0.99f));
            DropInventory.AddDrop(new Drop(new EquatableKey(ItemLib.Capes.LEATHER_CAPE), 0.25f));
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
