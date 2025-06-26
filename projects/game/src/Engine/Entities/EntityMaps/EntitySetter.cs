using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public static class EntitySetter
    {


        public static List<Entity> FillEntityMap(int id)
        {
            List<Entity> entities = new List<Entity>();

            switch (id)
            {
                case 0:
                    Entities.player = new Player(new Vector2(0, 50));
                    entities.Add(Entities.player);

                    entities.Add(new PlatformEntity(new Vector2(150, 100), 50, 3, 3, 0.75f));
                    entities.Add(new PlatformEntity(new Vector2(300, 100), 50, 3, 3));
                    entities.Add(new PlatformEntity(new Vector2(0, -50), 49, 3, 2, 0.2f));
                    entities.Add(new PlatformEntity(new Vector2(150, -50), 49, 3, 2));


                    entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-100, -50)));
                    entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-200, -50)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 0)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 30)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 50)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(-20, 100)));

                    entities.Add(new PlatformEntity(new Vector2(-200, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(-100, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(50, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(200, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(400, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(600, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(800, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(1000, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(1200, -200), 47));

                    entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-50, -150)));
                    entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-65, -150)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(-70, -150)));

                    entities.Add(new Mob(new Vector2(-60, -140), 0f));
                    entities.Add(new Mob(new Vector2(-150, -140), 0f));

                    entities.Add(new PlatformEntity(new Vector2(0, -350), 46, 3, 2));

                    break;
                case 1:
                    Entities.player = new Player(new Vector2(0, 20));
                    entities.Add(Entities.player);

                    entities.Add(new PlatformEntity(new Vector2(300, 100), 50, 3, 3));
                    entities.Add(new PlatformEntity(new Vector2(150, -50), 49, 3, 2));

                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(-20, 100)));

                    entities.Add(new PlatformEntity(new Vector2(-200, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(-100, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(50, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(200, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(400, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(600, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(800, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(1000, -200), 47));
                    entities.Add(new PlatformEntity(new Vector2(1200, -200), 47));

                    break;
            }
            

            return entities;
        }
    }
}
