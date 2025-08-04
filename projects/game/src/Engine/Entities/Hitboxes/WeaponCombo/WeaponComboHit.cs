using Microsoft.Xna.Framework;
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

        public WeaponComboHit(Vector2 hitboxPositionOffset, float hitboxRotationOffset, Vector2 entityPositionOffset, float swingTimeSec, AttackTypes[] attackSequence)
        {
            HitboxPositionOffset = hitboxPositionOffset;
            HitboxRotationOffset = hitboxRotationOffset;
            EntityPositionOffset = entityPositionOffset;
            SwingTimeSec = swingTimeSec;
            AttackSequence = attackSequence;
        }
    }
}