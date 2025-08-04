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
            SWORD
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
                    // X: [LIGHT]
                    new WeaponComboHit(new Vector2(0, 10), 1.7f, Vector2.Zero, 0.5f,
                        new AttackTypes[] { AttackTypes.LIGHT }),
                    // XX: [LIGHT, LIGHT]
                    new WeaponComboHit(new Vector2(0, 10), 1f, new Vector2(20, 0), 0.5f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT }),
                    // XXX: [LIGHT, LIGHT, LIGHT]
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.7f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }),
                    // Y: [HEAVY]
                    new WeaponComboHit(new Vector2(0, 10), 1.7f, Vector2.Zero, 1f,
                        new AttackTypes[] { AttackTypes.HEAVY }),
                    // YY: [HEAVY, HEAVY]
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 1f,
                        new AttackTypes[] { AttackTypes.HEAVY, AttackTypes.HEAVY }),


                    //QUICKFIX BUGGED
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.7f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }),
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.7f,
                        new AttackTypes[] { AttackTypes.HEAVY, AttackTypes.HEAVY, AttackTypes.HEAVY }),
                    //
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