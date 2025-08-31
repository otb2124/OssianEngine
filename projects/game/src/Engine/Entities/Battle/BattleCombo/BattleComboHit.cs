using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Linq;
using Utils;
using static Entities.BattleMovesetFactory;

namespace Entities
{

    

    public class BattleComboHit
    {
        public RotatedRectangle HitboxOffset;
        public Vector2 EntityPositionOffset;
        public float SwingTimeSec;
        public Vector2 HitboxAppearanceTimePeriod;
        public AttackTypes[] AttackSequence;
        public AnimationStates AnimationState;
        public AnimationData AnimationData;
        public BattleDamageStatsMultiplierData BattleHitData;

        public BattleComboHit(RotatedRectangle hitboxOffset, Vector2 entityPositionOffset, float swingTimeSec, Vector2 hitboxAppearanceTimePeriod, AttackTypes[] attackSequence, BattleDamageStatsMultiplierData battleHitData)
        {
            HitboxOffset = hitboxOffset;
            EntityPositionOffset = entityPositionOffset;
            SwingTimeSec = swingTimeSec;
            HitboxAppearanceTimePeriod = hitboxAppearanceTimePeriod;
            AttackSequence = attackSequence;
            BattleHitData = battleHitData;
        }


        public void SetAnimation(BattleMovesets set, float SpeedMultiplier)
        {
            float newSpeed = SwingTimeSec * SpeedMultiplier / (float)Graphics.Graphics.TimeScale / (float)Graphics.Graphics.TimeScale;

            switch (set)
            {
                case BattleMovesets.WEAPON_SWORD:

                    if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SWORD_LIGHT;
                        AnimationData = new AnimationData(4, new Vector2(0, 0), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT;
                        AnimationData = new AnimationData(4, new Vector2(0, 0), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_LIGHT;
                        AnimationData = new AnimationData(4, new Vector2(0, 0), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SWORD_HEAVY;
                        AnimationData = new AnimationData(4, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.HEAVY, AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY;
                        AnimationData = new AnimationData(4, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY;
                        AnimationData = new AnimationData(4, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.BLOCK }))
                    {
                        AnimationState = AnimationStates.BLOCKING_SWORD;
                        AnimationData = new AnimationData(1, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    break;


                case BattleMovesets.WEAPON_KNIFE:

                    if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_KNIFE_LIGHT;
                        AnimationData = new AnimationData(4, new Vector2(0, 0), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT;
                        AnimationData = new AnimationData(4, new Vector2(0, 0), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT;
                        AnimationData = new AnimationData(4, new Vector2(0, 0), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_KNIFE_HEAVY;
                        AnimationData = new AnimationData(4, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY;
                        AnimationData = new AnimationData(4, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.HEAVY, AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY;
                        AnimationData = new AnimationData(4, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.BLOCK }))
                    {
                        AnimationState = AnimationStates.BLOCKING_KNIFE;
                        AnimationData = new AnimationData(1, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    break;

                case BattleMovesets.WEAPON_BARE_HANDS:

                    if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_BARE_HANDS_LIGHT;
                        AnimationData = new AnimationData(4, new Vector2(0, 0), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT;
                        AnimationData = new AnimationData(4, new Vector2(0, 0), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT;
                        AnimationData = new AnimationData(4, new Vector2(0, 0), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_BARE_HANDS_HEAVY;
                        AnimationData = new AnimationData(4, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY;
                        AnimationData = new AnimationData(4, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.HEAVY, AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY_HEAVY;
                        AnimationData = new AnimationData(4, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.BLOCK }))
                    {
                        AnimationState = AnimationStates.BLOCKING_KNIFE;
                        AnimationData = new AnimationData(1, new Vector2(0, 128), new Vector2(128, 128), newSpeed);
                    }
                    break;



                case BattleMovesets.BODY_SLIME:

                    AnimationData = new AnimationData(1, Vector2.Zero, Vector2.Zero, newSpeed);

                    if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SLIME_BODY_LIGHT;
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT;
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT, AttackTypes.LIGHT, AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT;
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SLIME_BODY_HEAVY;
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.HEAVY, AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY;
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.BLOCK }))
                    {
                        AnimationState = AnimationStates.BLOCKING_SLIME_BODY;
                    }
                    break;

                case BattleMovesets.WEAPON_MAGIC:

                    AnimationData = new AnimationData(1, Vector2.Zero, Vector2.Zero, newSpeed);

                    if (AttackSequence.SequenceEqual(new[] { AttackTypes.LIGHT }))
                    {
                        AnimationState = AnimationStates.ATTACKING_BARE_HANDS_LIGHT;
                    }
                    else if (AttackSequence.SequenceEqual(new[] { AttackTypes.HEAVY }))
                    {
                        AnimationState = AnimationStates.ATTACKING_BARE_HANDS_HEAVY;
                    }

                    break;
            }
        }


    }
}