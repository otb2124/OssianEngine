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
    public static class EntityMapSetter
    {


        public static List<Entity> FillEntityMap(int id)
        {
            List<Entity> entities = new List<Entity>();

            switch (id)
            {
                case 0:


                    //parkour way
                    entities.Add(new TileEntity(new Vector2(550, -400), new Point(5, 5), TileEntity.TileSets.SET0, 0, true, true));
                    entities.Add(new LedgeEntity(new Vector2(472, -460), Directions.LEFT));
                    entities.Add(new LedgeEntity(new Vector2(472, -350), Directions.LEFT));

                    entities.Add(new TileEntity(new Vector2(-75,  -250), new Point(3, 3), TileEntity.TileSets.SET0));
                    entities.Add(new TileEntity(new Vector2(50,   -250), new Point(3, 1), TileEntity.TileSets.SET0));
                    entities.Add(new TileEntity(new Vector2(200,  -250), new Point(2, 1), TileEntity.TileSets.SET0));
                    entities.Add(new TileEntity(new Vector2(400,  -250), new Point(3, 2), TileEntity.TileSets.SET0));
                    entities.Add(new TileEntity(new Vector2(600,  -250), new Point(3, 2), TileEntity.TileSets.SET0, 0.2f));
                    entities.Add(new TileEntity(new Vector2(800,  -250), new Point(3, 2), TileEntity.TileSets.SET0));
                    entities.Add(new TileEntity(new Vector2(1000, -250), new Point(2, 2), TileEntity.TileSets.SET0));
                    entities.Add(new TileEntity(new Vector2(1000, -250), new Point(3, 2), TileEntity.TileSets.SET0));
                    entities.Add(new TileEntity(new Vector2(-150,   90), new Point(15, 1), TileEntity.TileSets.SET0));
                    entities.Add(new TileEntity(new Vector2(0,    -625), new Point(20, 1), TileEntity.TileSets.SET0, 0, true));
                    entities.Add(new TileEntity(new Vector2(-500, -600), new Point(12, 2), TileEntity.TileSets.SET0, 0, true));
                    entities.Add(new TileEntity(new Vector2(500,  -600), new Point(12, 2), TileEntity.TileSets.SET0, 0, true));

                    entities.Add(new PlatformEntity(new Vector2(0, 0), 5));
                    entities.Add(new PlatformEntity(new Vector2(140, -60), 5));

                    entities.Add(new DestroyableEntity(Models.CRATE_0, new Vector2(-200, -500)));

                    //entities.Add(new DestroyableEntity(Models.BALL, new Vector2(-101, 200)));
                    //entities.Add(new DestroyableEntity(Models.BALL, new Vector2(-103, 230)));
                    //entities.Add(new DestroyableEntity(Models.BALL, new Vector2(-102, 250)));
                    //entities.Add(new DestroyableEntity(Models.CRATE_1, new Vector2(-50, 50)));
                    //entities.Add(new DestroyableEntity(Models.CRATE_1, new Vector2(-65, 50)));
                    //entities.Add(new DestroyableEntity(Models.BALL, new Vector2(-70, 50)));

                    entities.Add(new HumanoidMob(HumanoidMob.HumanoidMobs.CITIZEN, new Vector2(-60, 100), 0f));
                    //entities.Add(new HumanoidMob(HumanoidMob.HumanoidMobs.CITIZEN, new Vector2(-150, 100), 0f));
                    //entities.Add(new HumanoidMob(HumanoidMob.HumanoidMobs.CITIZEN, new Vector2(50, 1000), 2f));


                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(-20, 300)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(20, 335)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(30, 310)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(40, 305)));

                    entities.Add(new InteractiveItemEntity(StaticSpriteFactory.GetItemUISpriteByItemKey(new ItemKey(ItemLib.Weapons.TERRABLADE)), FlatBodyPreset.ITEM_DROP, new Vector2(0, 300), new Vector2(30, 30), InteractiveItemEntity.InteractiveItemType.PICKUP, new Inventory(new ItemKey[] {new ItemKey(ItemLib.Weapons.TERRABLADE)})));

                    break;
                case 1:
                    entities.Add(new AnimalMob(Models.SLIME, new Vector2(-250, 100), 0f));
                    entities.Add(new AnimalMob(Models.SLIME, new Vector2(-200, 100), 0f));
                    entities.Add(new HumanoidMob(HumanoidMob.HumanoidMobs.BANDIT, new Vector2(250, 100), 0f));

                    entities.Add(new DestroyableEntity(Models.CRATE_1, new Vector2(-500, -300)));

                    entities.Add(new PlatformEntity(new Vector2(-500, -550), 5));
                    entities.Add(new PlatformEntity(new Vector2(500, -550), 5));
                    entities.Add(new PlatformEntity(new Vector2(-500, -500), 5));
                    entities.Add(new PlatformEntity(new Vector2(500, -500), 5));
                    entities.Add(new PlatformEntity(new Vector2(-500, -450), 5));
                    entities.Add(new PlatformEntity(new Vector2(500, -450), 5));

                    entities.Add(new TileEntity(new Vector2(0, -630), new Point(20, 1), TileEntity.TileSets.SET1, 0, true));
                    entities.Add(new TileEntity(new Vector2(-500, -600), new Point(12, 2), TileEntity.TileSets.SET1, 0, true));
                    entities.Add(new TileEntity(new Vector2(500, -600), new Point(12, 2), TileEntity.TileSets.SET1, 0, true));
                    entities.Add(new TileEntity(new Vector2(1000, -600), new Point(12, 2), TileEntity.TileSets.SET1, 0, true));
                    entities.Add(new TileEntity(new Vector2(-1000, -600), new Point(12, 2), TileEntity.TileSets.SET1, 0, true));

                    break;
            }
            

            return entities;
        }
    }
}
