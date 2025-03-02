using CSPlatformerSandbox.Engine.Entities.Stats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if(CheckForHit(entA.sManager.equipmentManager.GetCurrentWeapon().hitbox, entB.sManager.equipmentManager.armorHB))
            {
                HandleHit(entA, entB);
            }
            if (CheckForHit(entB.sManager.equipmentManager.GetCurrentWeapon().hitbox, entA.sManager.equipmentManager.armorHB))
            {
                HandleHit(entB, entA);
            }
            
        }


        public bool CheckForHit(WeaponHitbox hitboxFrom, Hitbox hitboxTo)
        {
            if (hitboxFrom != null && hitboxTo != null)
            {
                if (hitboxFrom.outerHalf.Intersects(hitboxTo.extends))
                {
                    return true;
                }
            }

            return false;
        }

        private void HandleHit(LivingEntity entFrom, LivingEntity entTo)
        {

            Debug.WriteLine("hit!");
            entFrom.sManager.DealDamageTo(entTo);

        }

    }
}
