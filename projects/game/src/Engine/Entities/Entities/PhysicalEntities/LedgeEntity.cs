using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class LedgeEntity : PhysicalEntity
    {

        public LedgeEntity(Vector2 pos, Directions direction) : base(Models.LEDGE, pos)
        {
            Model.Direction = direction;
        }

    }
}
