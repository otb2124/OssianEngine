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
            { EntityFractions.ANIMAL, new() { EntityFractions.ANIMAL, EntityFractions.BANDIT } },
        };

        public static void CheckWeaponToBodyCollision(StatsEntity entA, StatsEntity entB)
        {
            if (entA == entB) return;

            (RotatedRectangle hitboxA, RotatedRectangle hitboxB, float damageA, float knockBackPowerA) = (entA, entB) switch
            {
                (EquipmentEntity eqA, EquipmentEntity eqB) => (
                    eqA.EquipmentManager.GetCurrentWeaponBody((EquipmentWeaponBodyManager)eqA.WeaponBodyManager).Hitbox.outerHalf,
                    eqB.EquipmentManager.GetCurrentArmor().hitbox.extends,
                    eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                    eqA.EquipmentManager.GetCurrentWeapon().KnockbackPower
                ),
                (NonEquipmentEntity nhA, NonEquipmentEntity nhB) => (
                    nhA.DamageHitbox.extends,
                    nhB.BodyHitbox.extends,
                    nhA.Stats.bodyDamage,
                    nhA.Stats.bodyKnockbackPower
                ),
                (NonEquipmentEntity nhA, EquipmentEntity eqB) => (
                    nhA.DamageHitbox.extends,
                    eqB.EquipmentManager.GetCurrentArmor().hitbox.extends,
                    nhA.Stats.bodyDamage,
                    nhA.Stats.bodyKnockbackPower
                ),
                (EquipmentEntity eqA, NonEquipmentEntity nhB) => (
                    eqA.EquipmentManager.GetCurrentWeaponBody((EquipmentWeaponBodyManager)eqA.WeaponBodyManager).Hitbox.outerHalf,
                    nhB.BodyHitbox.extends,
                    eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                    eqA.EquipmentManager.GetCurrentWeapon().KnockbackPower
                ),
                _ => (null, null, 0f, 0f)
            };


            if (CheckIntersection(hitboxA, hitboxB) && CanDealDamage(entA.EntityFraction, entB.EntityFraction))
            {
                if(entB.Model.ModelState != ModelStates.BLOCKING && entA.Model.ModelState != ModelStates.BLOCKING)
                {
                    BattleHandler.HandleHit(entB, damageA, knockBackPowerA, hitboxA.Position);
                }
            }

        }

        public static void CheckWeaponToWeaponCollision(StatsEntity entA, StatsEntity entB)
        {
            if (entA == entB) return;

            if (entB.Model.ModelState == ModelStates.BLOCKING && (entA.Model.ModelState == ModelStates.ATTACKING_LIGHT || entA.Model.ModelState == ModelStates.ATTACKING_HEAVY)
                || entA.Model.ModelState == ModelStates.BLOCKING && (entB.Model.ModelState == ModelStates.ATTACKING_LIGHT || entB.Model.ModelState == ModelStates.ATTACKING_HEAVY))
            {

                (RotatedRectangle hitboxA, RotatedRectangle hitboxB, float damageA, float knockBackPowerA) = (entA, entB) switch
                {
                    (EquipmentEntity eqA, EquipmentEntity eqB) => (
                        eqA.EquipmentManager.GetCurrentWeaponBody((EquipmentWeaponBodyManager)eqA.WeaponBodyManager).Hitbox.outerHalf,
                        eqB.EquipmentManager.GetCurrentWeaponBody((EquipmentWeaponBodyManager)eqB.WeaponBodyManager).Hitbox.outerHalf,
                        eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                        eqA.EquipmentManager.GetCurrentWeapon().KnockbackPower
                    ),
                    (NonEquipmentEntity nhA, NonEquipmentEntity nhB) => (
                        nhA.DamageHitbox.extends,
                        nhB.DamageHitbox.extends,
                        nhA.Stats.bodyDamage,
                        nhA.Stats.bodyKnockbackPower
                    ),
                    (NonEquipmentEntity nhA, EquipmentEntity eqB) => (
                        nhA.DamageHitbox.extends,
                        eqB.EquipmentManager.GetCurrentWeaponBody((EquipmentWeaponBodyManager)eqB.WeaponBodyManager).Hitbox.outerHalf,
                        nhA.Stats.bodyDamage,
                        nhA.Stats.bodyKnockbackPower
                    ),
                    (EquipmentEntity eqA, NonEquipmentEntity nhB) => (
                        eqA.EquipmentManager.GetCurrentWeaponBody((EquipmentWeaponBodyManager)eqA.WeaponBodyManager).Hitbox.outerHalf,
                        nhB.DamageHitbox.extends,
                        eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                        eqA.EquipmentManager.GetCurrentWeapon().KnockbackPower
                    ),
                    _ => (null, null, 0f, 0f)
                };


                if (CheckIntersection(hitboxA, hitboxB) && CanDealDamage(entA.EntityFraction, entB.EntityFraction))
                {
                    if (entB.Model.ModelState == ModelStates.BLOCKING)
                    {
                        BattleHandler.HandleBlockHit(entB, damageA, knockBackPowerA, hitboxA.Position);
                    }
                }
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
