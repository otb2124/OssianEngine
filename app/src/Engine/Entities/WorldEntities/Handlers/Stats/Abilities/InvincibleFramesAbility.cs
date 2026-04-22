using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InvincibleFramesAbility : EntityAbility
    {

        public float InvincibleFramesDurationSec = 1f;
        public int InvincibleCounter = 0;
        public bool IsInvincible = true;

        public InvincibleFramesAbility(float invincibleFramesDurationSec)
        {
            InvincibleFramesDurationSec = invincibleFramesDurationSec;
            Type = EntityStatFeatures.INVINCIBLE_FRAMES;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            if (IsInvincible)
            {
                InvincibleCounter++;
                if (InvincibleCounter > InvincibleFramesDurationSec * Graphics.Graphics.GraphicsFrameRate)
                {
                    IsInvincible = false;
                    InvincibleCounter = 0;
                }
            }
        }
    }
}
