using Microsoft.Xna.Framework;
using Physics;
using Resources;

namespace Entities
{
    public class LivingEntity : PhysicalEntity
    {

        public LivingEntity(FlatBodyFactory.FlatBodyPreset preset, Sprite.Sprites sprite, Vector2 pos, float rotation = 0f) : base(preset, sprite, pos, rotation){}
        public override void Draw()
        {
            base.Draw();
        }
    }
}
