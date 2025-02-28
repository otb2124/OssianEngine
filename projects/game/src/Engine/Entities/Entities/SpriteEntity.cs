using Microsoft.Xna.Framework;
using Resources;

namespace Entities
{
    public class SpriteEntity : Entity
    {

        public Sprite sprite;
        public Vector2 spritePos;

        public SpriteEntity(StaticSpriteFactory.StaticSprites spritePreset, Vector2 pos) : base()
        {
            this.sprite = StaticSpriteFactory.GetSprite(spritePreset);
            this.spritePos = pos;
        }

        public override void Draw()
        {
            this.sprite.Draw(spritePos);
        }
    }
}
