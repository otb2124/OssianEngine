using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Entities.StatsEntity;

namespace Entities
{
    public static class HitboxChecker
    {

        private static readonly Dictionary<EntityFractions, HashSet<EntityFractions>> ignoreHit = new()
        {
            { EntityFractions.ANIMAL, new() { EntityFractions.ANIMAL } },
        };

        public static void CheckForCollision(StatsEntity entA, StatsEntity entB)
        {
            if (entA == entB) return;

            (RotatedRectangle hitboxA, RotatedRectangle hitboxB, float damageA, float knockBackPowerA) = (entA, entB) switch
            {
                (EquipmentEntity eqA, EquipmentEntity eqB) => (
                    eqA.EquipmentManager.GetCurrentWeapon().WeaponEntity.Hitbox.outerHalf,
                    eqB.EquipmentManager.GetCurrentArmor().hitbox.extends,
                    eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                    eqA.EquipmentManager.GetCurrentWeapon().KnockbackPower
                ),
                (NonHumanoidEntity nhA, NonHumanoidEntity nhB) => (
                    nhA.DamageHitbox.extends,
                    nhB.BodyHitbox.extends,
                    nhA.Stats.bodyDamage,
                    nhA.Stats.bodyKnockbackPower
                ),
                (NonHumanoidEntity nhA, EquipmentEntity eqB) => (
                    nhA.DamageHitbox.extends,
                    eqB.EquipmentManager.GetCurrentArmor().hitbox.extends,
                    nhA.Stats.bodyDamage,
                    nhA.Stats.bodyKnockbackPower
                ),
                (EquipmentEntity eqA, NonHumanoidEntity nhB) => (
                    eqA.EquipmentManager.GetCurrentWeapon().WeaponEntity.Hitbox.outerHalf,
                    nhB.BodyHitbox.extends,
                    eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                    eqA.EquipmentManager.GetCurrentWeapon().KnockbackPower
                ),
                _ => (null, null, 0f, 0f)
            };


            if (CheckIntersection(hitboxA, hitboxB) && CanDealDamage(entA.EntityFraction, entB.EntityFraction))
            {
                BattleHandler.HandleHit(entB, damageA, knockBackPowerA, entA.Model.body.Position);
            }

        }

        private static bool CanDealDamage(EntityFractions attacker, EntityFractions target)
        {
            return !ignoreHit.TryGetValue(attacker, out var targets) || !targets.Contains(target);
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
