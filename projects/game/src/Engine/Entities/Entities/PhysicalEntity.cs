using Microsoft.Xna.Framework;
using Physics;
using Resources;

namespace Entities
{
    public class PhysicalEntity : SpriteEntity
    {

        public FlatBody body;

        public PhysicalEntity(FlatBodyFactory.FlatBodyPreset preset, Sprite.Sprites sprite, Vector2 pos, float rotation = 0f) : base(sprite, pos) 
        {
            body = FlatBodyFactory.createFlatBody(preset);
            body.MoveTo(FlatConverter.ToFlatVector(pos));
            body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(body);
            body.owner = this;

            this.sprite = ResourceLoader.sprites[sprite];
        }


        public override void Draw()
        {
            this.body.Draw(sprite);

            //Debug
            //this.body.Draw();

            //base.Draw();
        }
    }
}
