using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class GlobalMapTime
    {
        public float TotalGameHours;
        private const float HoursPerSecond = 0.01f;
        public const float GlobalStartTime = 12f;

        public GlobalMapTime()
        {
            TotalGameHours = GlobalStartTime; // Start at 0:00
        }

        public void Update()
        {
            TotalGameHours += (float)(Graphics.Graphics.CurrentLogicTime * HoursPerSecond);
            TotalGameHours %= 24f;
        }

        public static Dictionary<Point, float> MapTravelTimeMap = new()
        {
            { new Point(0, 1), 5.5f },
            { new Point(1, 0), 1f }
        };

        public void AdjustForTravel(float travelTimeHours)
        {
            TotalGameHours += travelTimeHours;
            TotalGameHours %= 24f;
        }

        public void Reset()
        {
            TotalGameHours = 0f;
        }

        public override string ToString()
        {
            return $"{GetHours():D2}:{GetMinutes():D2}";
        }

        public int GetHours()
        {
            return (int)TotalGameHours;
        }

        public int GetMinutes()
        {
            return (int)((TotalGameHours - GetHours()) * 60);
        }
    }
}