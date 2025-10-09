using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InvincibleFramesHandler
    {

        public float InvincibleFramesDurationSec = 1f;
        public int InvincibleCounter = 0;
        public bool IsInvincible = true;

        public InvincibleFramesHandler(float invincibleFramesDurationSec)
        {
            InvincibleFramesDurationSec = invincibleFramesDurationSec;
        }

        public void Update()
        {
            if (IsInvincible)
            {
                InvincibleCounter++;
                if (InvincibleCounter > InvincibleFramesDurationSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    IsInvincible = false;
                    InvincibleCounter = 0;
                }
            }
        }
    }
}
