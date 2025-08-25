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

        private static readonly Dictionary<EntityFractions, HashSet<EntityFractions>> IgnoreHit = new()
        {
            //{ EntityFractions.ANIMAL, new() { EntityFractions.ANIMAL, EntityFractions.BANDIT } },
        };

        public static void CheckWeaponToBodyCollision(BattleEntity entA, BattleEntity entB)
        {
            if (entA == entB) return;

            (RotatedRectangle hitboxA, RotatedRectangle hitboxB) = (entA, entB) switch
            {
                (EquipmentEntity eqA, EquipmentEntity eqB) => (
                    eqA.EquipmentManager.GetCurrentWeaponBody(eqA.BattleBodyManager).Hitbox.outerHalf,
                    eqB.BattleBodyManager.BodyHitbox.extends
                ),
                (NonEquipmentEntity nhA, NonEquipmentEntity nhB) => (
                    nhA.BattleBodyManager.BattleBodies[0].Hitbox.outerHalf,
                    nhB.BattleBodyManager.BodyHitbox.extends
                ),
                (NonEquipmentEntity nhA, EquipmentEntity eqB) => (
                    nhA.BattleBodyManager.BattleBodies[0].Hitbox.outerHalf,
                    eqB.BattleBodyManager.BodyHitbox.extends
                ),
                (EquipmentEntity eqA, NonEquipmentEntity nhB) => (
                    eqA.EquipmentManager.GetCurrentWeaponBody(eqA.BattleBodyManager).Hitbox.outerHalf,
                    nhB.BattleBodyManager.BodyHitbox.extends
                ),
                _ => (null, null)
            };


            if (CheckIntersection(hitboxA, hitboxB) && CanDealDamage(entA.EntityFraction, entB.EntityFraction))
            {
                if(entB.Model.ModelState != ModelStates.BLOCKING && entA.Model.ModelState != ModelStates.BLOCKING)
                {
                    BattleHitHandler.HandleHit(entB, entA, hitboxB, hitboxA);
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
                        eqA.EquipmentManager.GetCurrentWeaponBody(eqA.BattleBodyManager).Hitbox.outerHalf,
                        eqB.EquipmentManager.GetCurrentWeaponBody(eqB.BattleBodyManager).Hitbox.outerHalf,
                        eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                        eqA.EquipmentManager.GetCurrentWeapon().KnockbackPower
                    ),
                    (NonEquipmentEntity nhA, NonEquipmentEntity nhB) => (
                        nhA.BattleBodyManager.BattleBodies[0].Hitbox.outerHalf,
                        nhB.BattleBodyManager.BattleBodies[0].Hitbox.outerHalf,
                        nhA.Stats.BodyDamage,
                        nhA.Stats.BodyKnockbackPower
                    ),
                    (NonEquipmentEntity nhA, EquipmentEntity eqB) => (
                        nhA.BattleBodyManager.BattleBodies[0].Hitbox.outerHalf,
                        eqB.EquipmentManager.GetCurrentWeaponBody(eqB.BattleBodyManager).Hitbox.outerHalf,
                        nhA.Stats.BodyDamage,
                        nhA.Stats.BodyKnockbackPower
                    ),
                    (EquipmentEntity eqA, NonEquipmentEntity nhB) => (
                        eqA.EquipmentManager.GetCurrentWeaponBody(eqA.BattleBodyManager).Hitbox.outerHalf,
                        nhB.BattleBodyManager.BattleBodies[0].Hitbox.outerHalf,
                        eqA.EquipmentManager.GetCurrentWeapon().PhysDmg,
                        eqA.EquipmentManager.GetCurrentWeapon().KnockbackPower
                    ),
                    _ => (null, null, 0f, 0f)
                };


                if (CheckIntersection(hitboxA, hitboxB) && CanDealDamage(entA.EntityFraction, entB.EntityFraction))
                {
                    if (entB.Model.ModelState == ModelStates.BLOCKING)
                    {
                        BattleHitHandler.HandleBlockHit(entB, damageA, knockBackPowerA, hitboxA.Position);
                    }
                }
            }

        }

        private static bool CanDealDamage(EntityFractions attacker, EntityFractions target)
        {
            return !IgnoreHit.TryGetValue(attacker, out var targets) || !targets.Contains(target);
        }



        public static void CheckForInterraction(InteractiveEntity interactiveEnt, EquipmentEntity livingEnt)
        {
            if (CheckIntersection(livingEnt.BattleBodyManager.BodyHitbox.extends, interactiveEnt.InteractionField.extends))
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
