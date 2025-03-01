using Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class EntityManager
    {

        List<Entity> entities;

        public EntityManager()
        {
            entities = new List<Entity>();
        }

        public void Init()
        {
            entities.Add(new PhysicalEntity(ModelFactory.Models.PLATFORM, new Vector2(0, -50), 0.2f));
            entities.Add(new PhysicalEntity(ModelFactory.Models.CRATE_BIG, new Vector2(-100, -50)));
            entities.Add(new PhysicalEntity(ModelFactory.Models.BALL, new Vector2(0, 0)));
            entities.Add(new Player(new Vector2(0, 20)));

            entities.Add(new PhysicalEntity(ModelFactory.Models.PLATFORM, new Vector2(-100, -200)));
            entities.Add(new PhysicalEntity(ModelFactory.Models.PLATFORM, new Vector2(100, -200)));
            entities.Add(new PhysicalEntity(ModelFactory.Models.CRATE_SMALL, new Vector2(-50, -150)));
            entities.Add(new PhysicalEntity(ModelFactory.Models.CRATE_SMALL, new Vector2(-65, -150)));

            entities.Add(new Mob(new Vector2(-60, -140), 0f));
            entities.Add(new Mob(new Vector2(-150, -140), 0f));
        }

        public void Update()
        {
            foreach (var entity in entities)
            {
                if(entity is Player)
                {
                    entity.Update();
                }
            }
            
        }


        public void Draw()
        {
            foreach (var entity in entities.Where(e => e is PhysicalEntity))
            {
                entity.Draw();
            }
        }

        public void DrawShapes()
        {
            foreach (var entity in entities.Where(e => e is PhysicalEntity))
            {
                entity.DrawDebug();
            }
        }


    }
}
