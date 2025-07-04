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

        public enum LivingEntityStatus
        {
            FRIENDLY,
            NEUTRAL,
            AGGRESSIVE,
        };

        public LivingEntityStatus status;

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
            status = LivingEntityStatus.NEUTRAL;
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
