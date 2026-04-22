using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class WaterTileEntity : TileEntity
    {

        public WaterTileEntity(Vector2 pos, int[][] indiciesMap, TileSets tileSet, float rot = 0f) : base(pos, indiciesMap, tileSet, rot)
        {
            SpriteZ = 999;
        }

        public WaterTileEntity(Vector2 pos, Point layout, TileSets tileSet, float rot = 0f) : base(pos, layout, tileSet, rot, false, false)
        {
            SpriteZ = 999;
        }
    }
}
