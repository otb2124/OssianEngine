using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Linq;
using Utils;
using static Entities.WeaponComboHitSetFactory;

namespace Entities
{
    public class WeaponComboHit
    {
        public RotatedRectangle HitboxOffset;
        public Vector2 EntityPositionOffset { get; }
        public float SwingTimeSec { get; }
        public Vector2 HitboxAppearanceTimePeriod { get; }
         public AttackTypes[] AttackSequence { get; }
        public AnimationStates AnimationState { get; set; }
        public AnimationData AnimationData { get; set; }

        public WeaponComboHit(RotatedRectangle hitboxOffset, Vector2 entityPositionOffset, float swingTimeSec, Vector2 hitboxAppearanceTimePeriod, AttackTypes[] attackSequence)
        {
            HitboxOffset = hitboxOffset;
            EntityPositionOffset = entityPositionOffset;
            SwingTimeSec = swingTimeSec;
            HitboxAppearanceTimePeriod = hitboxAppearanceTimePeriod;
            AttackSequence = attackSequence;
        }


        public void SetAnimation(WeaponComboHitSets set, float SpeedMultiplier)
        {
            float newSpeed = SwingTimeSec * SpeedMultiplier / (float)Graphics.Graphics.TimeScale / (float)Graphics.Graphics.TimeScale;

            switch (set)
            {
                case WeaponComboHitSets.SWORD:

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
                    break;
            }
        }


    }
}