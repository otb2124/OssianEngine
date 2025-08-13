using CSPlatformerSandbox.Engine.Entities.Battle.WeaponCombo;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Entities
{
    public static class WeaponComboMovesetFactory
    {
        public enum WeaponMovesets
        {
            SWORD,
            KNIFE,
            BARE_HANDS,
        }

        public enum AttackTypes
        {
            LIGHT,
            HEAVY,
            BLOCK,
        }

        public static readonly Dictionary<WeaponMovesets, WeaponComboMoveset> Movesets = new()
        {
            {
                WeaponMovesets.SWORD,
                    new WeaponComboMoveset
                    (
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
                                new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.2f), new Vector2(30, 0), 0.7f, new Vector2(0f, 0.7f),
                                new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }),

                            // XXY
                            new WeaponComboHit(
                                new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(20, 50), 1.2f), new Vector2(30, 0), 1f, new Vector2(0.7f, 1f),
                                new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.HEAVY }),

                            //BLOCK
                            new WeaponComboHit(
                                new Utils.RotatedRectangle(new Vector2(15, 20), new Vector2(10, 30), 0f), Vector2.Zero, 1f, new Vector2(0f, 0.9f),
                                new AttackTypes[] { AttackTypes.BLOCK }),
                        }
                    )
                    },
                    {
                        WeaponMovesets.KNIFE,
                        new WeaponComboMoveset
                        (
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
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT }),
                                // XY
                                new WeaponComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY }),



                                // XXX
                                new WeaponComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.25f, new Vector2(0.2f, 0.25f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }),
                                // XYY
                                new WeaponComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY, AttackTypes.HEAVY }),

                                //BLOCK
                                new WeaponComboHit(
                                    new Utils.RotatedRectangle(new Vector2(15, 20), new Vector2(10, 30), 0f), Vector2.Zero, 1f, new Vector2(0f, 0.9f),
                                    new AttackTypes[] { AttackTypes.BLOCK }),
                            }
                        )
                    },
                    {
                        WeaponMovesets.BARE_HANDS,
                        new WeaponComboMoveset(
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
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT }),
                                // XY
                                new WeaponComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY }),



                                // XXX
                                new WeaponComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), new Vector2(10, 0), 0.25f, new Vector2(0.2f, 0.25f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }),
                                // XYY
                                new WeaponComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), new Vector2(10, 0), 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY, AttackTypes.HEAVY }),


                                //BLOCK
                                new WeaponComboHit(
                                    new Utils.RotatedRectangle(new Vector2(15, 20), new Vector2(10, 30), 0f), Vector2.Zero, 1f, new Vector2(0f, 0.9f),
                                    new AttackTypes[] { AttackTypes.BLOCK }),
                            }
                        )

                    }
        };



        public static WeaponComboHit[] GetWeaponComboHits(WeaponMovesets weaponType)
        {
            return Movesets[weaponType].Combos;
        }

        public static int GetTotalComboHits(WeaponMovesets weaponType)
        {
            return Movesets[weaponType].Combos.Length;
        }

        public static WeaponComboHit GetComboHit(WeaponMovesets weaponType, AttackTypes[] sequence)
        {
            if (!Movesets.ContainsKey(weaponType) || sequence == null || sequence.Length == 0 || sequence.Length > GetLongestComboHit(weaponType).AttackSequence.Length)
            {
                return null;
            }

            return Movesets[weaponType].Combos.FirstOrDefault(h => h.AttackSequence.SequenceEqual(sequence));
        }

        public static WeaponComboHit GetLongestComboHit(WeaponMovesets weaponType)
        {
            if (!Movesets.ContainsKey(weaponType))
            {
                return null;
            }

            var longestHit = Movesets[weaponType].Combos
                .OrderByDescending(h => h.AttackSequence.Length)
                .FirstOrDefault();

            return longestHit;
        }

        public static ModelStates SwitchAttackTypeToModelState(AttackTypes type)
        {
            if (type == AttackTypes.LIGHT)
            {
                return ModelStates.ATTACKING_LIGHT;
            }

            return ModelStates.ATTACKING_HEAVY;
        }
    }
}