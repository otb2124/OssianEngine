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

        public Vector2 HangingPosition;

        public LedgeEntity(Vector2 pos, Directions direction) : base(Models.LEDGE, pos)
        {
            Model.Direction = direction;
            HangingPosition = new Vector2(pos.X + 10 * Resources.Model.GetDirectionCoefficient(direction), pos.Y - 10);
        }

    }
}
