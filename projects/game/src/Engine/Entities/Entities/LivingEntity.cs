using Microsoft.Xna.Framework;
using Resources;

namespace Entities
{
    public class LivingEntity : PhysicalEntity
    {

        public StatsManager sManager;
        public LivingEntity(ModelFactory.Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
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

        public override void DrawHitbox()
        {
            sManager.equipmentManager.weaponHB.Draw(Color.Red);
            sManager.equipmentManager.armorHB.Draw(Color.Blue);
        }
    }
}
