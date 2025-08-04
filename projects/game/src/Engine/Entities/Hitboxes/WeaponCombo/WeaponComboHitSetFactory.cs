using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.WeaponEntity;

namespace Entities
{
    public static class WeaponComboHitSetFactory
    {
        public enum WeaponComboHitSets
        {
            SWORD
        }


        private static readonly Dictionary<WeaponComboHitSets, (WeaponComboHit[], AttackType[])[]> comboHitTemplates = new()
        {
            {
                WeaponComboHitSets.SWORD,
                new (WeaponComboHit[], AttackType[])[]
                {
                    // Combo 0: X X X (Light, Light, Light)
                    (
                        new WeaponComboHit[]
                        {
                            new WeaponComboHit(0, new Vector2(0, 10), 1.7f, Vector2.Zero, 0.5f),
                            new WeaponComboHit(1, new Vector2(0, 10), 1f, new Vector2(20, 0), 0.5f),
                            new WeaponComboHit(2, new Vector2(0, 10), 2f, new Vector2(30, 0), 0.7f),
                        },
                        new AttackType[] { AttackType.Light, AttackType.Light, AttackType.Light }
                    ),
                    // Combo 1: Y Y (Heavy, Heavy)
                    (
                        new WeaponComboHit[]
                        {
                            new WeaponComboHit(0, new Vector2(0, 10), 1.7f, Vector2.Zero, 1f),
                            new WeaponComboHit(1, new Vector2(0, 10), 2f, new Vector2(30, 0), 1f),
                        },
                        new AttackType[] { AttackType.Heavy, AttackType.Heavy }
                    ),
                    // Combo 2: X X Y (Light, Light, Heavy)
                    (
                        new WeaponComboHit[]
                        {
                            new WeaponComboHit(0, new Vector2(0, 10), 1.7f, Vector2.Zero, 0.5f),
                            new WeaponComboHit(1, new Vector2(0, 10), 1f, new Vector2(20, 0), 0.5f),
                            new WeaponComboHit(2, new Vector2(15, 10), 2f, new Vector2(30, 0), 1f),
                        },
                        new AttackType[] { AttackType.Light, AttackType.Light, AttackType.Heavy }
                    ),
                    // Combo 3: X Y X Y (Light, Heavy, Light, Heavy)
                    (
                        new WeaponComboHit[]
                        {
                            new WeaponComboHit(0, new Vector2(0, 10), 1.7f, Vector2.Zero, 0.5f),
                            new WeaponComboHit(1, new Vector2(15, 10), 2f, new Vector2(30, 0), 1f),
                            new WeaponComboHit(2, new Vector2(0, 10), 1f, new Vector2(20, 0), 0.5f),
                            new WeaponComboHit(3, new Vector2(15, 10), 2f, new Vector2(30, 0), 1f),
                        },
                        new AttackType[] { AttackType.Light, AttackType.Heavy, AttackType.Light, AttackType.Heavy }
                    )
                }
            }
        };

        public static WeaponComboHitSet[] GetWeaponComboHitSets(WeaponComboHitSets weaponType)
        {
            var templates = comboHitTemplates[weaponType];
            var sets = new WeaponComboHitSet[templates.Length];
            for (int i = 0; i < templates.Length; i++)
            {
                sets[i] = new WeaponComboHitSet(templates[i].Item1, templates[i].Item2);
            }
            return sets;
        }

        public static int GetTotalComboHits(WeaponComboHitSets weaponType)
        {
            var templates = comboHitTemplates[weaponType];
            return templates.Sum(template => template.Item1.Length);
        }
    }


}
