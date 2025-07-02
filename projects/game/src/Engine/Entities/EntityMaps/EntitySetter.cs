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

                    //entities.Add(new PlatformEntity(new Vector2(0, -50), 47, 3, 2, 0.2f));
                    //entities.Add(new PlatformEntity(new Vector2(150, -50), 48, 5, 2));
                    entities.Add(new PlatformEntity(new Vector2(-75,  0), new Point(3, 3)));
                    entities.Add(new PlatformEntity(new Vector2(50,   0), new Point(3, 1)));
                    entities.Add(new PlatformEntity(new Vector2(200,  0), new Point(2, 1)));
                    entities.Add(new PlatformEntity(new Vector2(400,  0), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(600,  0), new Point(3, 2), 20));
                    entities.Add(new PlatformEntity(new Vector2(800,  0), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(1000, 0), new Point(2, 2)));
                    entities.Add(new PlatformEntity(new Vector2(1200, 0), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(-150, 200), new Point(15, 1)));
                    entities.Add(new PlatformEntity(new Vector2(0, -250), new Point(20, 1), 0, true));
                    entities.Add(new PlatformEntity(new Vector2(-500, -230), new Point(12, 2), 0, true));
                    entities.Add(new PlatformEntity(new Vector2(500, -230), new Point(12, 2), 0, true));

                    entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-100, 150)));
                    entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-200, 150)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 200)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 230)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 250)));
                    entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-50, 50)));
                    entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-65, 50)));
                    entities.Add(new PhysicalEntity(Models.BALL, new Vector2(-70, 50)));

                    entities.Add(new Mob(new Vector2(-60, 100), 0f));
                    entities.Add(new Mob(new Vector2(-150, 100), 0f));

                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(-20, 300)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(20, 335)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(30, 310)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(40, 305)));

                    entities.Add(new InteractiveItemEntity(StaticSpriteFactory.GetItemUISpriteByItemKey(new ItemKey(ItemLib.Weapons.TERRABLADE)), FlatBodyPreset.ITEM_DROP, new Vector2(0, 300), new Vector2(30, 30), InteractiveItemEntity.InteractiveItemType.PICKUP, new Inventory(new ItemKey[] {new ItemKey(ItemLib.Weapons.TERRABLADE)})));

                    break;
                case 1:
                    Entities.player = new Player(new Vector2(0, 20));
                    entities.Add(Entities.player);

                    entities.Add(new PlatformEntity(new Vector2(300, 100), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(150, -50), new Point(3, 2)));

                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(-20, 100)));

                    entities.Add(new PlatformEntity(new Vector2(-200, -200), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(-100, -200), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(50, -200), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(200, -200), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(400, -200), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(600, -200), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(800, -200), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(1000, -200), new Point(3, 2)));
                    entities.Add(new PlatformEntity(new Vector2(1200, -200), new Point(3, 2)));

                    break;
            }
            

            return entities;
        }
    }
}
