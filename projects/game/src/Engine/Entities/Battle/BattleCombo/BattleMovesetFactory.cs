using CSPlatformerSandbox.Engine.Entities.Battle.WeaponCombo;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Entities
{
    public static class BattleMovesetFactory
    {
        public enum BattleMovesets
        {
            WEAPON_SWORD,
            WEAPON_KNIFE,
            WEAPON_BARE_HANDS,

            BODY_SLIME,
        }

        public enum AttackTypes
        {
            LIGHT,
            HEAVY,
            BLOCK,
        }

        public static readonly Dictionary<BattleMovesets, BattleMoveset> Movesets = new()
        {
            {
                BattleMovesets.WEAPON_SWORD,
                    new BattleMoveset
                    (
                        new[]
                        {
                            // X
                            new BattleComboHit(
                                new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.5f), Vector2.Zero, 0.5f, new Vector2(0.2f, 0.4f),
                                new AttackTypes[] { AttackTypes.LIGHT },
                                new BattleHitData(1, 1, 1, 1)
                            ),
                            // Y
                            new BattleComboHit(
                                new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(15, 50), 1.5f), Vector2.Zero, 1f, new Vector2(0.7f, 1f),
                                new AttackTypes[] { AttackTypes.HEAVY },
                                new BattleHitData(1, 1, 1, 1)
                            ),
                    

                            // XX
                            new BattleComboHit(
                                new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), new Vector2(20, 0), 0.5f, new Vector2(0.2f, 0.4f),
                                new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT },
                                new BattleHitData(1, 1, 1, 1)
                            ),
                            // YY
                            new BattleComboHit(
                                new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(15, 50), 1.7f), new Vector2(30, 0), 1f, new Vector2(0.7f, 1f),
                                new AttackTypes[] { AttackTypes.HEAVY, AttackTypes.HEAVY },
                                new BattleHitData(1, 1, 1, 1)
                            ),


                            // XXX
                            new BattleComboHit(
                                new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.2f), new Vector2(30, 0), 0.7f, new Vector2(0f, 0.7f),
                                new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT },
                                new BattleHitData(1, 1, 1, 1.5f)
                            ),

                            // XXY
                            new BattleComboHit(
                                new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(20, 50), 1.2f), new Vector2(30, 0), 1f, new Vector2(0.7f, 1f),
                                new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.HEAVY },
                                new BattleHitData(1, 1, 1, 1.5f)
                            ),

                            //BLOCK
                            new BattleComboHit(
                                new Utils.RotatedRectangle(new Vector2(15, 20), new Vector2(10, 30), 0f), Vector2.Zero, 1f, new Vector2(0f, 0.9f),
                                new AttackTypes[] { AttackTypes.BLOCK },
                                new BattleHitData(1, 1, 1, 1)
                            ),
                        }
                    )
                    },
                    {
                        BattleMovesets.WEAPON_KNIFE,
                        new BattleMoveset
                        (
                            new[]
                            {
                                // X
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.25f, new Vector2(0.2f, 0.25f),
                                    new AttackTypes[] { AttackTypes.LIGHT },
                                    new BattleHitData(1, 1, 1, 1)
                                ),

                                // Y
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.HEAVY },
                                    new BattleHitData(1, 1, 1, 1)
                                ),

                                // XX
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.25f, new Vector2(0.2f, 0.25f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT },
                                    new BattleHitData(1, 1, 1, 1)
                                ),

                                // XY
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY },
                                    new BattleHitData(1, 1, 1, 1)
                                ),


                                // XXX
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.25f, new Vector2(0.2f, 0.25f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                                // XYY
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY, AttackTypes.HEAVY },
                                    new BattleHitData(1, 1, 1, 1)
                                ),

                                //BLOCK
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(15, 20), new Vector2(10, 30), 0f), Vector2.Zero, 1f, new Vector2(0f, 0.9f),
                                    new AttackTypes[] { AttackTypes.BLOCK },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                            }
                        )
                    },
                    {
                        BattleMovesets.WEAPON_BARE_HANDS,
                        new BattleMoveset(
                            new[]
                            {
                                // X
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 2f), Vector2.Zero, 0.25f, new Vector2(0.2f, 0.25f),
                                    new AttackTypes[] { AttackTypes.LIGHT },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                                // Y
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.HEAVY },
                                    new BattleHitData(1, 1, 1, 1)
                                ),

                                // XX
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.25f, new Vector2(0.2f, 0.25f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                                // XY
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), Vector2.Zero, 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY },
                                    new BattleHitData(1, 1, 1, 1)
                                ),



                                // XXX
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), new Vector2(10, 0), 0.25f, new Vector2(0.2f, 0.25f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                                // XYY
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), new Vector2(10, 0), 0.5f, new Vector2(0.4f, 0.5f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.HEAVY, AttackTypes.HEAVY },
                                    new BattleHitData(1, 1, 1, 1)
                                ),


                                //BLOCK
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(15, 20), new Vector2(10, 30), 0f), Vector2.Zero, 1f, new Vector2(0f, 0.9f),
                                    new AttackTypes[] { AttackTypes.BLOCK },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                            }
                        )

                    },
                    {
                        BattleMovesets.BODY_SLIME,
                        new BattleMoveset
                        (
                            new[]
                            {
                                // X
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.5f), Vector2.Zero, 0.5f, new Vector2(0.2f, 0.4f),
                                    new AttackTypes[] { AttackTypes.LIGHT },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                                // Y
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(15, 50), 1.5f), Vector2.Zero, 1f, new Vector2(0.7f, 1f),
                                    new AttackTypes[] { AttackTypes.HEAVY },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                    

                                // XX
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.7f), new Vector2(20, 0), 0.5f, new Vector2(0.2f, 0.4f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                                // YY
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(15, 50), 1.7f), new Vector2(30, 0), 1f, new Vector2(0.7f, 1f),
                                    new AttackTypes[] { AttackTypes.HEAVY, AttackTypes.HEAVY },
                                    new BattleHitData(1, 1, 1, 1)
                                ),


                                // XXX
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(0, 10), new Vector2(10, 40), 1.2f), new Vector2(30, 0), 0.7f, new Vector2(0f, 0.7f),
                                    new AttackTypes[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT },
                                    new BattleHitData(1, 1, 1, 1)
                                ),

                                //BLOCK
                                new BattleComboHit(
                                    new Utils.RotatedRectangle(new Vector2(15, 20), new Vector2(10, 30), 0f), Vector2.Zero, 1f, new Vector2(0f, 0.9f),
                                    new AttackTypes[] { AttackTypes.BLOCK },
                                    new BattleHitData(1, 1, 1, 1)
                                ),
                            }
                        )
                    },
        };



        public static BattleComboHit[] GetWeaponComboHits(BattleMovesets weaponType)
        {
            return Movesets[weaponType].Combos;
        }

        public static int GetTotalComboHits(BattleMovesets weaponType)
        {
            return Movesets[weaponType].Combos.Length;
        }

        public static BattleComboHit GetComboHit(BattleMovesets weaponType, AttackTypes[] sequence)
        {
            if (!Movesets.ContainsKey(weaponType) || sequence == null || sequence.Length == 0 || sequence.Length > GetLongestComboHit(weaponType).AttackSequence.Length)
            {
                return null;
            }

            return Movesets[weaponType].Combos.FirstOrDefault(h => h.AttackSequence.SequenceEqual(sequence));
        }

        public static BattleComboHit GetLongestComboHit(BattleMovesets weaponType)
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
            else if(type == AttackTypes.HEAVY)
            {
                return ModelStates.ATTACKING_HEAVY;
            }
            else
            {
                return ModelStates.BLOCKING;
            }
        }

        public static AttackTypes SwitchModelStateToAttackType(ModelStates state)
        {
            if (state == ModelStates.ATTACKING_LIGHT)
            {
                return AttackTypes.LIGHT;
            }
            else if (state == ModelStates.ATTACKING_HEAVY)
            {
                return AttackTypes.HEAVY;
            }
            else
            {
                return AttackTypes.BLOCK;
            }

        }
    }
}