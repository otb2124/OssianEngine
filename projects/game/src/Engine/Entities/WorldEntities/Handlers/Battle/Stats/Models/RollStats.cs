using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RollStats
    {

        public float StaminaRollCostSec;
        public float RollMultiplier;

        public RollStats(float rollMult, float staminaCost) 
        {
            RollMultiplier = rollMult;
            StaminaRollCostSec = staminaCost;
        }
    }
}
