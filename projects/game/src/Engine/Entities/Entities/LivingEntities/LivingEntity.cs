using Microsoft.Xna.Framework;
using Utils;

namespace Entities
{
    public class LivingEntity : PhysicalEntity
    {

        public StatsManager sManager;


        public enum LivingEntityStatus
        {
            FRIENDLY,
            NEUTRAL,
            AGGRESSIVE,
        };

        public LivingEntityStatus status;


        public LivingEntity(Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
        {
            this.sManager = new StatsManager();
            SetStats();
        }

        public virtual void SetStats()
        {
            status = LivingEntityStatus.NEUTRAL;
        }

        public override void Draw()
        {
            base.Draw();
        }


        public virtual void DrawWeapon()
        {
            sManager.equipmentManager.Draw(this.model.direction);
        }

        public override void DrawHitbox()
        {
            sManager.equipmentManager.DrawHitbox();
        }
    }
}
