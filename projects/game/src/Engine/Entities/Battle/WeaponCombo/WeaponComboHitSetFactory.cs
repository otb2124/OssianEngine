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
                    new WeaponComboHit(new Vector2(0, 10), 1.7f, Vector2.Zero, 0.5f,
                        new AttackTypes[] { AttackTypes.LIGHT },
                        Utils.AnimationStates.ATTACKING_SWORD_LIGHT),
                    // Y
                    new WeaponComboHit(new Vector2(0, 10), 1.7f, Vector2.Zero, 1f,
                        new AttackTypes[] { AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_SWORD_HEAVY),
                    


                    // XX
                    new WeaponComboHit(new Vector2(0, 10), 1f, new Vector2(20, 0), 0.5f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT },
                        Utils.AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT),
                    // YY
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 1f,
                        new AttackTypes[] { AttackTypes.HEAVY, AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY),



                    // XXX
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.7f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT },
                        Utils.AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_LIGHT),
                    // XXY
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 1f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY),
                     

                    
                    
                    


                    //QUICKFIX BUGGED
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.7f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT },
                        Utils.AnimationStates.ATTACKING_LIGHT),
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.7f,
                        new AttackTypes[] { AttackTypes.HEAVY, AttackTypes.HEAVY, AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_LIGHT),
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 1f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.HEAVY, AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_LIGHT),
                    //
                }
            },
            {
                WeaponComboHitSets.KNIFE,
                new[]
                {
                    // X
                    new WeaponComboHit(new Vector2(0, 10), 1.7f, Vector2.Zero, 0.25f,
                        new AttackTypes[] { AttackTypes.LIGHT },
                        Utils.AnimationStates.ATTACKING_KNIFE_LIGHT),
                    // Y
                    new WeaponComboHit(new Vector2(0, 10), 1.7f, Vector2.Zero, 0.5f,
                        new AttackTypes[] { AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_KNIFE_HEAVY),
                    


                    // XX
                    new WeaponComboHit(new Vector2(0, 10), 1f, new Vector2(20, 0), 0.25f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT },
                        Utils.AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT),
                    // XY
                    new WeaponComboHit(new Vector2(0, 10), 1f, new Vector2(20, 0), 0.5f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY),
                    // YY
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.5f,
                        new AttackTypes[] { AttackTypes.HEAVY, AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_KNIFE_HEAVY_HEAVY),



                    // XXX
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.3f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT },
                        Utils.AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT),
                    // XYY
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.25f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY, AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY),

                    
                    
                    


                    //QUICKFIX BUGGED
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.3f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT },
                        Utils.AnimationStates.ATTACKING_LIGHT),
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.3f,
                        new AttackTypes[] { AttackTypes.HEAVY, AttackTypes.HEAVY, AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_LIGHT),
                    new WeaponComboHit(new Vector2(0, 10), 2f, new Vector2(30, 0), 0.5f,
                        new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY, AttackTypes.HEAVY, AttackTypes.HEAVY },
                        Utils.AnimationStates.ATTACKING_LIGHT),
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