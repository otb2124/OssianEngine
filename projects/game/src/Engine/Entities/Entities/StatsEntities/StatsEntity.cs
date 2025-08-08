using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using Utils;

namespace Entities
{
    public class StatsEntity : PhysicalEntity
    {

        public EntityStats Stats;
        public Inventory Inventory;

        public bool CanRegensStamina;
        public bool CanUpdateIFrames;
        public bool CanFall;

        public ParticleSet.ParticleSets BloodDropParticle;

        public enum EntityFractions
        {
            NEUTRAL,
            PLAYER,
            ANIMAL,
            BANDIT,
        };

        public EntityFractions EntityFraction;

        public StatsEntity(Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
        {
            SetStats();
            SetInventory();
        }

        public StatsEntity(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, float rotation = 0f) : base(sprite, body, pos, rotation)
        {
            SetStats();
            SetInventory();
        }

        public StatsEntity() : base()
        {
            SetStats();
            SetInventory();
        }


        public override void Update()
        {
            EntityModelStateHandler.Update(this);

            if (CanRegensStamina)
            {
                Stats.RegenStamina();
                Stats.OnUsingStamina = false;
            }
            if (CanUpdateIFrames)
            {
                Stats.UpdateInvincibleFrames();
            }
            if(CanFall)
            {
                Stats.UpdateFallen(this.Model);
            }

            Stats.UpdateDescending(this);
            
            base.Update();
        }

        public virtual void SetStats()
        {
            Stats = new EntityStats();
            EntityFraction = EntityFractions.NEUTRAL;
            BloodDropParticle = ParticleSet.ParticleSets.NONE;

            CanRegensStamina = false;
            CanUpdateIFrames = false;
            CanFall = false;
        }

        public virtual void SetInventory()
        {
            Inventory = new Inventory();
        }


        public virtual void DrawHitboxes(){}
    }
}
