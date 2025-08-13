using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Entities.WeaponComboHitSetFactory;

namespace Entities
{
    public class WeaponComboBlock
    {

        public RotatedRectangle HitboxOffset;
        public AnimationStates AnimationState;

        public WeaponComboBlock(RotatedRectangle hitboxOffset) 
        {
            HitboxOffset = hitboxOffset;
        }

        public void SetAnimation(WeaponMovesets set)
        {
            switch (set)
            {
                case WeaponMovesets.SWORD:
                    AnimationState = AnimationStates.BLOCKING_SWORD;
                    break;
                case WeaponMovesets.KNIFE:
                    AnimationState = AnimationStates.BLOCKING_KNIFE;
                    break;
                case WeaponMovesets.BARE_HANDS:
                    AnimationState = AnimationStates.BLOCKING_BARE_HANDS;
                    break;
            }
        }
    }
}
