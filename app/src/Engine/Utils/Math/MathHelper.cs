using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class MathHelper
    {




        public static float RadiansToDegree(float radians)
        {
            return radians * (180f / (float)Math.PI);
        }

        public static float DegreesToRadians(float degrees)
        {
            return degrees * ((float)Math.PI / 180f);
        }
    }
}
