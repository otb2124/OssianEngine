using Microsoft.Xna.Framework;
using Physics;
using Resources;

namespace Entities
{
    public class LivingEntity : PhysicalEntity
    {

        public Stats stats;
        public LivingEntity(FlatBodyFactory.FlatBodyPreset preset, Sprite.Sprites sprite, Vector2 pos, float rotation = 0f) : base(preset, sprite, pos, rotation)
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
