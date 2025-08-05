using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public static class WeaponComboHitSetFactory
    {
        public enum WeaponComboHitSets
        {
            SWORD,
            KNIFE
        }

        public enum AttackTypes
        {
            LIGHT,
            HEAVY
        }

        private static readonly Dictionary<WeaponComboHitSets, WeaponComboHit[]> hitTemplates = new()
        {
            {
                WeaponComboHitSets.SWORD,
                new[]
                {
                    // X
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.5f), Vector2.Zero, 0.5f, new Vector2(0.2f, 0.4f),
                        new AttackTypes[] { AttackTypes.LIGHT }),
                    // Y
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(15, 50), 1.5f), Vector2.Zero, 1f, new Vector2(0.7f, 1f),
                        new AttackTypes[] { AttackTypes.HEAVY }),
                    


                    // XX
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), new Vector2(20, 0), 0.5f, new Vector2(0.2f, 0.4f),
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT }),
                    // YY
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(15, 50), 1.7f), new Vector2(30, 0), 1f, new Vector2(0.7f, 1f),
                        new AttackTypes[] { AttackTypes.HEAVY, AttackTypes.HEAVY }),



                    // XXX
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.2f), new Vector2(30, 0), 0.7f, new Vector2(0.5f, 0.7f),
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }),
                    // XXY
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(20, 50), 1.2f), new Vector2(30, 0), 1f, new Vector2(0.7f, 1f),
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.HEAVY }),
                }
            },
            {
                WeaponComboHitSets.KNIFE,
                new[]
                {
                    // X
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.25f, new Vector2(0.2f, 0.25f),
                        new AttackTypes[] { AttackTypes.LIGHT }),
                    // Y
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                        new AttackTypes[] { AttackTypes.HEAVY }),

                    // XX
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.25f, new Vector2(0.2f, 0.25f),
                        new AttackTypes[] { AttackTypes.LIGHT }),
                    // XY
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                        new AttackTypes[] { AttackTypes.HEAVY }),



                    // XXX
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.25f, new Vector2(0.2f, 0.25f),
                        new AttackTypes[] { AttackTypes.LIGHT }),
                    // XYY
                    new WeaponComboHit(
                        new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                        new AttackTypes[] { AttackTypes.HEAVY }),
                }
            }
        };



        public static WeaponComboHit[] GetWeaponComboHits(WeaponComboHitSets weaponType)
        {
            var templates = hitTemplates[weaponType];
            return templates;
        }

        public static int GetTotalComboHits(WeaponComboHitSets weaponType)
        {
            return hitTemplates[weaponType].Length;
        }
    }
}