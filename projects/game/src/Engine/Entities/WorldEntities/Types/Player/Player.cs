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

        public Player() : base(Models.HUMAN_M, Vector2.Zero, 0f)
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

            StatsManager.StatsBattleHitSpendHandler = new StatsBattleHitSpendHandler();

            StatsManager.Abilities = new EntityAbility[]
            {
                new InvincibleFramesAbility(1f),
                new StaminaRegenerationAbility(20, 1.5f),
                new FallAbility(),
                new LedgeHangingAbility(),
                new GCSRectanglesCalculatorAbility(),
                new DescencionAbility(0.5f, 1f),
                new DoubleJumpAbility(),
                new InwaterWalkingAbility(0.5f)
            };

            StatsManager.RefillAll();

            //StatsManager.AddStatEffect(StatEffects.FAST_LEGS);

            EntityFraction = EntityFractions.PLAYER;
            BloodDropParticle = ParticleSet.ParticleSets.HUMAN_BLOOD_SPLASH;
        }


        public override void SetEquipment()
        {
            base.SetEquipment();

            EquipmentManager.SetWeapon(BattleBodyManager, (WeaponEquipment)ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.TERRABLADE)), WeaponHands.LEFT);

            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Helmets.IRON_HELMET));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Chestplates.IRON_CHESTPLATE));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Boots.IRON_BOOTS));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Gloves.IRON_GLOVES));

            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Capes.LEATHER_CAPE));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Necklaces.IRON_NECKLACE));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Belts.IRON_BELT));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Rings.IRON_RING));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Rings.IRON_RING));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Containments.BACKPACK));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.Pets.CALL_DOG));
            EquipmentManager.Equipments.SetEquipment(new EquatableKey(ItemLib.LightPets.CALL_FIREFLY));
        }

        public override void SetInventory()
        {
            base.SetInventory();

            Inventory.Init(60);

            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Consumables.HEALTH_POTION)));
            //Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.TERRABLADE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Materials.SWORD_HILT)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Materials.SWORD_HILT)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.Weapons.TORCH)));



            //test for inventory capacity
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.NOTE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.HEAD)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
            Inventory.AddItem(ItemFactory.CreateItem(new EquatableKey(ItemLib.QuestItems.STONE)));
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
            Console.WriteLine(Model.ModelState);
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
