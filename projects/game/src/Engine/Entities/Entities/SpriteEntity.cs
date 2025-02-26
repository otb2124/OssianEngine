using Microsoft.Xna.Framework;
using Resources;

namespace Entities
{
    public class SpriteEntity : Entity
    {

        public Sprite sprite;

        public SpriteEntity(Sprite.Sprites sprite, Vector2 pos, float rotation = 0f) : base()
        {
            this.sprite = ResourceLoader.sprites[sprite];
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
