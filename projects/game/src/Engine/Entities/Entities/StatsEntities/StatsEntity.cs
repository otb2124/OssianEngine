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
            Stats.RegenStamina();
            Stats.OnUsingStamina = false;
            base.Update();
        }

        public virtual void SetStats()
        {
            Stats = new EntityStats();
            EntityFraction = EntityFractions.NEUTRAL;
        }

        public virtual void SetInventory()
        {
            Inventory = new Inventory();
        }


        public virtual void DrawHitboxes()
        {

        }
    }
}
