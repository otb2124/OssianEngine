using Microsoft.Xna.Framework;
using Resources;

namespace Entities
{
    public class LivingEntity : PhysicalEntity
    {

        public Stats stats;
        public LivingEntity(ModelFactory.Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
        {
            this.stats = new Stats();
            setStats();
        }

        public virtual void setStats()
        {

        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
