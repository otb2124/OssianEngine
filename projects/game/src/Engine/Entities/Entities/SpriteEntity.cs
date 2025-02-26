using Microsoft.Xna.Framework;
using Resources;

namespace Entities
{
    public class SpriteEntity : Entity
    {

        public Sprite sprite;
        public Vector2 spritePos;

        public SpriteEntity(SpriteFactory.Sprites sprite, Vector2 pos) : base()
        {
            this.sprite = ResourceLoader.sprites[sprite];
            this.spritePos = pos;
        }

        public override void Draw()
        {
            this.sprite.Draw(spritePos);
            base.Draw();
        }
    }
}
