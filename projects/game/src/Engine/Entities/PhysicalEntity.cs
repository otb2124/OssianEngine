using Microsoft.Xna.Framework;
using Physics;

namespace Entities
{
    public class PhysicalEntity : Entity
    {

        public FlatBody body;

        public PhysicalEntity(BodyDynamics bodyDynamics, BodyShapeType bodyShapeType, Vector2 pos) : base() 
        {

            bool isStatic = bodyDynamics == BodyDynamics.STATIC;


            string errorMsg;
            bool success;
            if (bodyShapeType == BodyShapeType.Box)
            {
                success = FlatBody.CreateBoxBody(100, 10, 1, isStatic, 0, out body, out errorMsg);
                body.RotateTo(0.2f);
            }
            else
            {
                success = FlatBody.CreateCircleBody(5, 0.5f, isStatic, 0.5f, out body, out errorMsg);
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
