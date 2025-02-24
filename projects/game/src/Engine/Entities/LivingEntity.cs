using Microsoft.Xna.Framework;
using Physics;

namespace Entities
{
    public class LivingEntity : PhysicalEntity
    {

        public LivingEntity(FlatBodyFactory.FlatBodyPreset preset, Vector2 pos, float rotation = 0f) : base(preset, pos, rotation)
        {
            
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
