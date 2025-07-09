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


        public EntityAIManager aiManager;
        public BehaviourCases CurrentBehaviourCase;

        public HumanoidMob(Models modelPreset, Vector2 pos, float rotation) : base(modelPreset, pos, rotation)
        {
            EntityFraction = EntityFractions.BANDIT;
            aiManager = new EntityAIManager(BehaviourPatterns.ANIMAL_DEFAULT);
            CurrentBehaviourCase = BehaviourCases.IDLE_RANDOM;
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
        }

        public override void SetStats()
        {
            base.SetStats();

            Stats.maxHP = 100;
            Stats.HP = 100;
            Stats.maxSpeed = 0.5f;
            Stats.jumpSpeed = 2.5f;

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
            if(!Stats.IsFallen)
            {
                aiManager.Update(this, CurrentBehaviourCase);
            }

            EquipmentManager.GetCurrentWeapon().hitbox.Update(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Width * 2, this.Model.body.Height), 0f);
            EquipmentManager.GetCurrentArmor().hitbox.Update(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Width, this.Model.body.Height), 0f);

            Model.aManager.Update(new Tuple<Directions, AnimationStates>(Model.direction, Model.animationState));

            base.Update();
        }


        public override void Draw()
        {
            base.Draw();
        }
    }
}
