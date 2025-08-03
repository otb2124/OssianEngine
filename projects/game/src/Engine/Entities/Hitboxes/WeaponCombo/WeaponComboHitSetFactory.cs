using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class WeaponComboHitSetFactory
    {
        public enum WeaponComboHitSets
        {
            SWORD
        }


        private static readonly Dictionary<WeaponComboHitSets, WeaponComboHit[][]> comboHitTemplates = new()
        {
            {
                WeaponComboHitSets.SWORD,
                new WeaponComboHit[][]
                {
                    new WeaponComboHit[] // Combo 0 (Light Attack)
                    {
                        new WeaponComboHit(0, new Vector2(0, 10), 1.7f, Vector2.Zero, 0.5f),
                        new WeaponComboHit(1, new Vector2(0, 10), 1f, new Vector2(20, 0), 0.5f),
                        new WeaponComboHit(2, new Vector2(0, 10), 2f, new Vector2(30, 0), 0.7f),
                    },
                    new WeaponComboHit[] // Combo 1 (Heavy Attack)
                    {
                        new WeaponComboHit(0, new Vector2(0, 10), 1.7f, Vector2.Zero, 1f),
                        new WeaponComboHit(1, new Vector2(0, 10), 2f, new Vector2(30, 0), 1f),
                    }
                }
            }
        };

            public static WeaponComboHitSet[] GetWeaponComboHitSets(WeaponComboHitSets weaponType)
            {
                var templates = comboHitTemplates[weaponType];
                var sets = new WeaponComboHitSet[templates.Length];
                for (int i = 0; i < templates.Length; i++)
                {
                    sets[i] = new WeaponComboHitSet(templates[i]);
                }
                return sets;
            }

        public static int GetTotalComboHits(WeaponComboHitSets weaponType)
        {
            var templates = comboHitTemplates[weaponType];
            return templates.Sum(template => template.Length);
        }
    }


}
