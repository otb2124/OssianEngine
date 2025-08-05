using Microsoft.Xna.Framework;
using Utils;
using static Entities.WeaponComboHitSetFactory;

namespace Entities
{
    public class WeaponComboHit
    {
        public Vector2 HitboxPositionOffset { get; }
        public float HitboxRotationOffset { get; }
        public Vector2 EntityPositionOffset { get; }
        public float SwingTimeSec { get; }
        public AttackTypes[] AttackSequence { get; }
        public AnimationStates AnimationState { get; }

        public WeaponComboHit(Vector2 hitboxPositionOffset, float hitboxRotationOffset, Vector2 entityPositionOffset, float swingTimeSec, AttackTypes[] attackSequence, AnimationStates animationState)
        {
            HitboxPositionOffset = hitboxPositionOffset;
            HitboxRotationOffset = hitboxRotationOffset;
            EntityPositionOffset = entityPositionOffset;
            SwingTimeSec = swingTimeSec;
            AttackSequence = attackSequence;
            AnimationState = animationState;
        }
    }
}