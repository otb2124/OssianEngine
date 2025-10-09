using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PoiseStats
    {

        public float Poise;
        public float MaxPoise;
        public float PoiseRegenSec;

        public PoiseStats(float maxPoise, float poiseRegenSec)
        {
            MaxPoise = maxPoise;
            Poise = maxPoise;
            PoiseRegenSec = poiseRegenSec;
        }

        public void Refill()
        {
            Poise = MaxPoise;
        }
    }
}
