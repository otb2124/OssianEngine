using CSPlatformerSandbox.Engine.Entities.Stats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class HitboxChecker
    {

        public static void CheckForCollisions(LivingEntity entA, LivingEntity entB)
        {
            //check both ways
            if(CheckForHit(entA.sManager.equipmentManager.GetCurrentWeapon().hitbox, entB.sManager.equipmentManager.chestplate.armorHB))
            {
                HitboxHandler.HandleHit(entA, entB);
            }
            if (CheckForHit(entB.sManager.equipmentManager.GetCurrentWeapon().hitbox, entA.sManager.equipmentManager.chestplate.armorHB))
            {
                HitboxHandler.HandleHit(entB, entA);
            }
        }

        public static void CheckForInterraction(InteractiveEntity interractiveEnt, LivingEntity livingEnt)
        {
            if(CheckForInterraction(livingEnt.sManager.equipmentManager.chestplate.armorHB, interractiveEnt.InteractionField))
            {
                HitboxHandler.HandleInterraction(interractiveEnt, livingEnt);
            }
        }


        public static bool CheckForHit(WeaponHitbox weaponhitboxFrom, Hitbox hitboxTo)
        {
            if (weaponhitboxFrom != null && hitboxTo != null)
            {
                if (weaponhitboxFrom.outerHalf.Intersects(hitboxTo.extends))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckForInterraction(Hitbox hitboxFrom, Hitbox hitboxTo)
        {
            if (hitboxFrom != null && hitboxTo != null)
            {
                if (hitboxFrom.extends.Intersects(hitboxTo.extends))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
