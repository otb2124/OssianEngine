using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AggroStats
    {

        public float DistanceToAggro = -1f;
        public float DistanceToUnaggro = -1f;

        public AggroStats(float distanceToAggro, float distanceToUnaggro)
        {
            DistanceToAggro = distanceToAggro;
            DistanceToUnaggro = distanceToUnaggro;
        }
    }
}
