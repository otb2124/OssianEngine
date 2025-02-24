using Microsoft.Xna.Framework;
using Physics;

namespace Entities
{
    public class EntityManager
    {

        PhysicalEntity ent1;
        PhysicalEntity ent2;
        static Color color1 = Color.Red;
        static Color color2 = Color.Blue;

        public void Init()
        {
            ent1 = new PhysicalEntity(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(0, -50), new Vector2(10, 10), 0.5f, 0.5f);
            ent2 = new PhysicalEntity(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(0, -100), new Vector2(100, 10), 1, 0, 0.2f);
        }

        public void Update()
        {
        }


        public void Draw()
        {
            ent1.Draw();
            ent2.Draw();
        }
    }
}
