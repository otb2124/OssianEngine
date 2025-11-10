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


        public static List<WorldEntity> FillEntityMap(int id)
        {
            List<WorldEntity> entities = new List<WorldEntity>();

            switch (id)
            {
                case 0:
                    //ledge parkour way
                    entities.Add(new TileEntity(new Vector2(550, -400), new Point(5, 5), TileEntity.TileSets.TILE_0, 0, true) { IsWall = true});
                    entities.Add(new LedgeEntity(new Vector2(474, -460), Directions.LEFT, LedgeEntity.Ledges.LEDGE0));
                    entities.Add(new LedgeEntity(new Vector2(474, -330), Directions.LEFT, LedgeEntity.Ledges.LEDGE0, true));
                    entities.Add(new LedgeEntity(new Vector2(626, -460), Directions.RIGHT, LedgeEntity.Ledges.LEDGE0));
                    entities.Add(new LedgeEntity(new Vector2(626, -330), Directions.RIGHT, LedgeEntity.Ledges.LEDGE0, true));

                    entities.Add(new TileEntity(new Vector2(-75,  -250), new Point(3, 3), TileEntity.TileSets.TILE_0));
                    entities.Add(new TileEntity(new Vector2(50,   -250), new Point(3, 1), TileEntity.TileSets.TILE_0));
                    entities.Add(new TileEntity(new Vector2(200,  -250), new Point(2, 1), TileEntity.TileSets.TILE_0));
                    entities.Add(new TileEntity(new Vector2(400,  -250), new Point(3, 2), TileEntity.TileSets.TILE_0));
                    entities.Add(new TileEntity(new Vector2(600,  -200), new Point(6, 1), TileEntity.TileSets.TILE_0, 0.4f));

                    entities.Add(new PlatformEntity(Platforms.STAIR0, new Vector2(700, -100), 5, true, 0.7f));
                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(800, -100), 5, false, 0.7f));

                    entities.Add(new TileEntity(new Vector2(800,  -250), new Point(3, 2), TileEntity.TileSets.TILE_0));
                    entities.Add(new TileEntity(new Vector2(1000, -250), new Point(2, 2), TileEntity.TileSets.TILE_0));
                    entities.Add(new TileEntity(new Vector2(1000, -250), new Point(3, 2), TileEntity.TileSets.TILE_0));
                    entities.Add(new TileEntity(new Vector2(-150,   90), new Point(15, 1), TileEntity.TileSets.TILE_0));

                    entities.Add(new WaterTileEntity(new Vector2(510, -644), new Point(12, 1), TileEntity.TileSets.WATER_0, 0));

                    entities.Add(new TileEntity(new Vector2(0,    -625), new Point(20, 1), TileEntity.TileSets.TILE_0, 0, true));
                    entities.Add(new TileEntity(new Vector2(-500, -600), new Point(12, 2), TileEntity.TileSets.TILE_0, 0, true));
                    entities.Add(new TileEntity(new Vector2(500,  -680), new Point(12, 2), TileEntity.TileSets.TILE_0, 0, true));

                    entities.Add(new LadderEntity(Ladders.LADDER0, new Vector2(-120, -60), 10));
                    entities.Add(new LadderEntity(Ladders.LADDER0, new Vector2(-220, 0), 5));

                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(140, 0), 5));
                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(0, -60), 5));
                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(140, -120), 1));

                    entities.Add(new DestroyableEntity(Models.CRATE_0, new Vector2(-200, -500)) { IsWall = true});
                    entities.Add(new LedgeEntity(new Vector2(-220, -480), Directions.LEFT, LedgeEntity.Ledges.INVISIBLE, true));
                    entities.Add(new LedgeEntity(new Vector2(-180, -480), Directions.RIGHT, LedgeEntity.Ledges.INVISIBLE, true));


                    //TODO: fix collision

                    entities.Add(new DestroyableEntity(Models.BALL, new Vector2(-101, 0)));
                    entities.Add(new DestroyableEntity(Models.BALL, new Vector2(-103, 0)));
                    entities.Add(new DestroyableEntity(Models.BALL, new Vector2(-102, 0)));
                    entities.Add(new DestroyableEntity(Models.CRATE_1, new Vector2(-50, 50)));
                    entities.Add(new DestroyableEntity(Models.CRATE_1, new Vector2(-65, 50)));
                    entities.Add(new DestroyableEntity(Models.BALL, new Vector2(-70, 50)));

                    entities.Add(new HumanoidEntity(HumanoidEntity.HumanoidMobs.VIGO, new Vector2(100, 100), 0f));
                    entities.Add(new HumanoidEntity(HumanoidEntity.HumanoidMobs.WANEGRO, new Vector2(-100, 100), 0f));

                    //entities.Add(new HumanoidEntity(HumanoidEntity.HumanoidMobs.BANDIT, new Vector2(-60,  100), 0f));
                    entities.Add(new AnimalMob(AnimalMob.AnimalMobs.SLIME, new Vector2(-200, 100), 0f));
                    entities.Add(new AnimalMob(AnimalMob.AnimalMobs.BAT, new Vector2(-300, 100), 0f));

                    //entities.Add(new ProjectileEntity(new Vector2(-100, 200)));

                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(-20, 300)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(20, 335)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(30, 310)));
                    entities.Add(new InteractiveItemEntity(InteractiveItemEntity.InteractiveItems.GOLD_COIN, new Vector2(40, 305)));
                    //entities.Add(EntityHelper.CreateItemDrop(new EquatableKey(ItemLib.Weapons.TERRABLADE), new Vector2(0, 300)));

                    break;
                case 1:
                    //entities.Add(new AnimalMob(Models.SLIME, new Vector2(-250, 100), 0f));
                    //entities.Add(new AnimalMob(Models.SLIME, new Vector2(-200, 100), 0f));
                    entities.Add(new DestroyableEntity(Models.CRATE_1, new Vector2(-500, -300)));

                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(-500, -550), 5));
                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(500, -550), 5));
                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(-500, -500), 5));
                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(500, -500), 5));
                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(-500, -450), 5));
                    entities.Add(new PlatformEntity(Platforms.PLATFROM0, new Vector2(500, -450), 5));

                    entities.Add(new TileEntity(new Vector2(0, -630), new Point(20, 1), TileEntity.TileSets.TILE_1, 0, true));
                    entities.Add(new TileEntity(new Vector2(-500, -600), new Point(12, 2), TileEntity.TileSets.TILE_1, 0, true));
                    entities.Add(new TileEntity(new Vector2(500, -600), new Point(12, 2), TileEntity.TileSets.TILE_1, 0, true));
                    entities.Add(new TileEntity(new Vector2(1000, -600), new Point(12, 2), TileEntity.TileSets.TILE_1, 0, true));
                    entities.Add(new TileEntity(new Vector2(-1000, -600), new Point(12, 2), TileEntity.TileSets.TILE_1, 0, true));

                    

                    break;
            }
            

            return entities;
        }
    }
}
