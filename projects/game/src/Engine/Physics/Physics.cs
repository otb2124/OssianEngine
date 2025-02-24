using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public static class Physics
    {

        public static Stopwatch watch;
        public static double totalWorldStepTIme = 0d;
        public static int totalBodyCount = 0;
        public static int totalSampleCount = 0;
        public static Stopwatch sampleTimer = new Stopwatch();
        public static string worldStepTimeString = string.Empty;
        public static string bodyCountString = string.Empty;


        public static FlatWorld flatWorld;
    }
}
