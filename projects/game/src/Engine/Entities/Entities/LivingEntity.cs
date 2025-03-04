using Microsoft.Xna.Framework;
using Utils;

namespace Entities
{
    public class LivingEntity : PhysicalEntity
    {

        public StatsManager sManager;
        public LivingEntity(Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
        {
            this.sManager = new StatsManager();
            setStats();
        }

        public virtual void setStats()
        {

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
