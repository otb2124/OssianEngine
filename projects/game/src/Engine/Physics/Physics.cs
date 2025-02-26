using Microsoft.Xna.Framework;
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

        public static CollisionHandler collisionHandler;

        public static void Update()
        {
            FlatWorld.TransformCount = 0;
            FlatWorld.NoTransformCount = 0;

            watch.Restart();
            flatWorld.Step(FlatUtil.GetElapsedTimeInSeconds(Graphics.Graphics.gameTime), 20);
            watch.Stop();

            totalWorldStepTIme += watch.Elapsed.TotalMilliseconds;
            totalBodyCount += flatWorld.BodyCount;
            totalSampleCount++;
        }
    }
}
