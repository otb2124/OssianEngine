using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class JumpStats
    {


        public float JumpSpeed;
        public float StaminaJumpCostSec;

        public JumpStats(float maxJumpSpeed, float staminaCostSec)
        {
            JumpSpeed = maxJumpSpeed;
            StaminaJumpCostSec = staminaCostSec;
        }
    }
}
