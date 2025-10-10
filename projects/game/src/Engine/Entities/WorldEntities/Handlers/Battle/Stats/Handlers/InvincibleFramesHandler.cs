using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InvincibleFramesHandler : EntityStatFeature
    {

        public float InvincibleFramesDurationSec = 1f;
        public int InvincibleCounter = 0;

        public InvincibleFramesHandler(float invincibleFramesDurationSec)
        {
            InvincibleFramesDurationSec = invincibleFramesDurationSec;
            Type = EntityStatFeatures.INVINCIBLE_FRAMES;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            if (statsManager.IsInvincible)
            {
                InvincibleCounter++;
                if (InvincibleCounter > InvincibleFramesDurationSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    statsManager.IsInvincible = false;
                    InvincibleCounter = 0;
                }
            }
        }
    }
}
