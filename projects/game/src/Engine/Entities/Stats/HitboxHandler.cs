using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class HitboxHandler
    {


        public void CheckForCollisions(LivingEntity entA, LivingEntity entB)
        {
            //check both ways
            CheckForHit(entA.sManager.equipmentManager.weaponHB, entA.sManager.equipmentManager.armorHB);
            CheckForHit(entA.sManager.equipmentManager.armorHB, entA.sManager.equipmentManager.weaponHB);
        }


        public void CheckForHit(Hitbox hitboxFrom, Hitbox hitboxTo)
        {

        }

    }
}
