using Microsoft.Xna.Framework;
using Resources;
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

                    entities.Add(new PlatformEntity(new Vector2(0, -50), 47, 3, 2, 0.2f));
                    entities.Add(new PlatformEntity(new Vector2(150, -50), 48, 5, 2));
                    entities.Add(new PlatformEntity(new Vector2(-75, -200), 40, 5, 2));
                    entities.Add(new PlatformEntity(new Vector2(50, -200), 44));
                    entities.Add(new PlatformEntity(new Vector2(200, -200), 43));
                    entities.Add(new PlatformEntity(new Vector2(400, -200), 42));
                    entities.Add(new PlatformEntity(new Vector2(600, -200), 41));
                    entities.Add(new PlatformEntity(new Vector2(800, -200), 40));
                    entities.Add(new PlatformEntity(new Vector2(1000, -200), 39));
                    entities.Add(new PlatformEntity(new Vector2(1200, -200), 38));
                    entities.Add(new PlatformEntity(new Vector2(350, -350), 37, 10, 2));
                    entities.Add(new PlatformEntity(new Vector2(0, -350), 36, 10, 2));
                    entities.Add(new PlatformEntity(new Vector2(-350, -350), 35, 10, 2));

                    entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-100, -50)));
                    entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-200, -50)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 0)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 30)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 50)));
                    entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-50, -150)));
                    entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-65, -150)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(-70, -150)));

                    entities.Add(new Mob(new Vector2(-60, -140), 0f));
                    entities.Add(new Mob(new Vector2(-150, -140), 0f));

                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(-20, 100)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(20, 115)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(30, 110)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(40, 105)));

                    entities.Add(new InteractiveItemEntity(StaticSpriteFactory.GetItemUISpriteByItemKey(new ItemKey(ItemLib.Weapons.TERRABLADE)), FlatBodyPreset.ITEM_DROP, new Vector2(0, 300), new Vector2(30, 30), InteractiveItemEntity.InteractiveItemType.PICKUP, new Inventory(new ItemKey[] {new ItemKey(ItemLib.Weapons.TERRABLADE)})));

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
