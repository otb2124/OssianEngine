using Microsoft.Xna.Framework;
using Physics;

namespace Entities
{
    public class PhysicalEntity : Entity
    {

        public FlatBody body;

        public PhysicalEntity(FlatBodyFactory.FlatBodyPreset preset, Vector2 pos, float rotation = 0f) : base() 
        {
            body = FlatBodyFactory.createFlatBody(preset);
            body.MoveTo(FlatConverter.ToFlatVector(pos));
            body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(body);
        }


        public override void Draw()
        {
            this.body.Draw();

            base.Draw();
        }
    }
}
