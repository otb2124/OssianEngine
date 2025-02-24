using Microsoft.Xna.Framework;
using Physics;

namespace Entities
{
    public class PhysicalEntity : Entity
    {

        public FlatBody body;

        public PhysicalEntity(BodyDynamics bodyDynamics, BodyShapeType bodyShapeType, Vector2 pos, Vector2 size, float density, float resitution, float rotation = 0f) : base() 
        {

            bool isStatic = bodyDynamics == BodyDynamics.STATIC;

            string errorMsg;
            bool success;
            if (bodyShapeType == BodyShapeType.Box)
            {
                success = FlatBody.CreateBoxBody(size.X, size.Y, density, isStatic, resitution, out body, out errorMsg);
                body.RotateTo(rotation);
            }
            else
            {
                success = FlatBody.CreateCircleBody(size.X, density, isStatic, resitution, out body, out errorMsg);
            }

            body.MoveTo(FlatConverter.ToFlatVector(pos));
            body.BodyShapeType = bodyShapeType;
            Physics.Physics.flatWorld.AddBody(body);
        }


        public void Draw()
        {

            this.body.Draw();

            base.Draw();
        }
    }
}
