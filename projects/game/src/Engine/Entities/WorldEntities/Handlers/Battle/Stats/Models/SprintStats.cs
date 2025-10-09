using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SprintStats
    {

        public float SprintMultiplier;
        public float StaminaSprintCostSec;

        public SprintStats(float sprintMultiplier, float staminaSprintCostSec)
        {
            SprintMultiplier = sprintMultiplier;
            StaminaSprintCostSec = staminaSprintCostSec;
        }
    }
}
