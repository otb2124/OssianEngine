using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public static class HitboxChecker
    {

        public static void CheckForCollision(StatsEntity entA, StatsEntity entB)
        {
            if (entA == entB) return;

            (RotatedRectangle hitboxA, RotatedRectangle hitboxB, float damageA, float damageB) = (entA, entB) switch
            {
                (EquipmentEntity eqA, EquipmentEntity eqB) => (
                    eqA.EquipmentManager.GetCurrentWeapon().hitbox.outerHalf,
                    eqB.EquipmentManager.GetCurrentArmor().hitbox.extends,
                    eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                    eqB.EquipmentManager.GetCurrentWeapon().PhysDmg
                ),
                (NonHumanoidEntity nhA, NonHumanoidEntity nhB) => (
                    nhA.DamageHitbox.extends,
                    nhB.BodyHitbox.extends,
                    nhA.Stats.bodyDamage,
                    nhB.Stats.bodyDamage
                ),
                (NonHumanoidEntity nhA, EquipmentEntity eqB) => (
                    nhA.DamageHitbox.extends,
                    eqB.EquipmentManager.GetCurrentArmor().hitbox.extends,
                    nhA.Stats.bodyDamage,
                    eqB.EquipmentManager.GetCurrentWeapon().PhysDmg
                ),
                //possibly redundant part of code
                (EquipmentEntity eqA, NonHumanoidEntity nhB) => (
                    eqA.EquipmentManager.GetCurrentWeapon().hitbox.outerHalf,
                    nhB.BodyHitbox.extends,
                    eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                    nhB.Stats.bodyDamage
                ),
                _ => (null, null, 0f, 0f)
            };


            if (CheckIntersection(hitboxA, hitboxB))
            {
                BattleHandler.HandleHit(entB, damageA);
            }

        }

        public static void CheckForInterraction(InteractiveEntity interactiveEnt, EquipmentEntity livingEnt)
        {
            if (CheckIntersection(livingEnt.EquipmentManager.GetCurrentArmor().hitbox.extends, interactiveEnt.InteractionField.extends))
            {
                InteractionHandler.HandleInterraction(interactiveEnt, livingEnt);
            }
        }


        public static bool CheckForHit(RotatedRectangle from, RotatedRectangle to)
        {
            if (from != null && to != null)
            {
                if (from.Intersects(to))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckIntersection(RotatedRectangle from, RotatedRectangle to) 
        { 
            return from != null && to != null && from.Intersects(to); 
        }

    }
}
