using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
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

            StatsManager.Stats = new EntityStat[]
            {
                new EntityStat(EntityStats.HP, 100, 100),
                new EntityStat(EntityStats.MANA, 100, 100),
                new EntityStat(EntityStats.STAMINA, 100, 100),
                new EntityStat(EntityStats.MOVEMENT_SPEED, 1f, 1f),
                new EntityStat(EntityStats.JUMP_SPEED, 2.8f, 2.8f, 60),
                new EntityStat(EntityStats.SPRINT_SPEED_MULTIPLIER, 1.5f, 1.5f, 15),
                new EntityStat(EntityStats.ROLL_SPEED_MULTIPLIER, 2f, 2f, 200),
                new EntityStat(EntityStats.POISE, 100, 100, 10)
            };

            StatsManager.StatsBattleHitSpendHandler = new StatsBattleHitSpendTool();

            StatsManager.Abilities = new EntityAbility[]
            {
                new InvincibleFramesAbility(1f),
                new StaminaRegenerationAbility(20, 1.5f),
                new FallAbility(),
                new LedgeHangingAbility(),
                new GCSRectanglesCalculatorAbility(),
                new DescencionAbility(0.5f, 1f),
                new DoubleJumpAbility()
            };

            StatsManager.RefillAll();

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

            Inventory.Init(60);

            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Consumables.HEALTH_POTION)));
            //Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Materials.SWORD_HILT)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TORCH)));



            //test for inventory capacity
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new ItemKey(ItemLib.QuestItems.STONE)));
        }


        public override void SetAnimations()
        {

            Model.AManagers = new Animator[]
                    {
                        new Animator
                        (
                            Model.SpriteData.SpriteSheet,
                            new List<Animation>
                            {
                                //idle
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(9, new Vector2(0, 0), new Vector2(64, 128), 0.1f)),
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT),
                                    new AnimationFramesData(9, new Vector2(0, 0), new Vector2(64, 128), 0.1f)),

                                //moving
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.LEFT),
                                    new AnimationFramesData(9, new Vector2(0, 128), new Vector2(64, 128), 0.1f)),
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.RIGHT),
                                    new AnimationFramesData(9, new Vector2(0, 128), new Vector2(64, 128), 0.1f)),

                                //jumping
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*2), new Vector2(64, 128), 0.04f)),
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*2), new Vector2(64, 128), 0.04f)),

                                //fallen
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*7), new Vector2(64, 128), 0.04f)),
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*7), new Vector2(64, 128), 0.04f)),

                                //roll
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.LEFT),
                                    new AnimationFramesData(9, new Vector2(0, 128*6), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.RIGHT),
                                    new AnimationFramesData(9, new Vector2(0, 128*6), new Vector2(64, 128), 0.15f)),

                                //sprinting
                                new Animation(new AnimationKey(AnimationStates.SPRINTING, Directions.LEFT),
                                    new AnimationFramesData(9, new Vector2(0, 128*6), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.SPRINTING, Directions.RIGHT),
                                    new AnimationFramesData(9, new Vector2(0, 128*6), new Vector2(64, 128), 0.15f)),


                                //weapon out
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_IDLE, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_IDLE, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //weapon out
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_MOVING, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_MOVING, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),


                                //blocking sw
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SWORD, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SWORD, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),

                                //attacking sw l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw ll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                 
                                //attacking sw lll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw hh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw llh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),





                                //blocking kn
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_KNIFE, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_KNIFE, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),

                                //attacking kn l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw ll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                 
                                //attacking sw lll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),





                                //blocking bh
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_BARE_HANDS, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_BARE_HANDS, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),

                                //attacking kn l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw ll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                 
                                //attacking sw lll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                            }
                         )
                    };

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
            ModelStateSwapHandler.Update();
            //Console.WriteLine(Model.ModelState);
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
