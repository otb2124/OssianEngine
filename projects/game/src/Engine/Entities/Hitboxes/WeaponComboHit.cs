using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class WeaponComboHit
    {


        public int SetId;

        public Vector2 EntityPositionOffset;
        public Vector2 HitboxPositionOffset;
        public float HitboxRotationOffset;

        public WeaponComboHit(int setId, Vector2 hitboxPositionOffset, float hitboxRotationOffset, Vector2 entityPositionOffset)
        {
            SetId = setId;
            HitboxPositionOffset = hitboxPositionOffset;
            HitboxRotationOffset = hitboxRotationOffset;
            EntityPositionOffset = entityPositionOffset;
        }
    }
}
