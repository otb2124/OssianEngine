using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ExperienceStats
    {

        public int ExperienceLvl = 0;
        public int Experience = 0;


        public Dictionary<int, int> LevelExperienceCost = new()
        {
            {1, 100 },
            {2, 250 },
            {3, 500 }
        };
    }
}
