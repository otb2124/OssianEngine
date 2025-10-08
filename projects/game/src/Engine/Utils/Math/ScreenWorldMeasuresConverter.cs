using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ScreenWorldMeasuresConverter
    {



        public static Vector2 ToWorldPos(Vector2 screenPos)
        {
            return screenPos += Graphics.Graphics.camera.Position;
        }


        public static Vector2 ToScreenPos(Vector2 worldPos)
        {
            Vector2 screenPos = worldPos - Graphics.Graphics.camera.Position;
            Vector2 screenPosPlusScreenBounds = new Vector2(screenPos.X + Graphics.Graphics.ScreenResolution.X / 2f, screenPos.Y + Graphics.Graphics.ScreenResolution.Y / 2f);

            return screenPosPlusScreenBounds;
        }

        public static Vector2 FlatBodyBoundsToScreen(Vector2 bodyBounds)
        {
            float currentZoom = (float)Graphics.Graphics.camera.Z;
            float baseZoom = (float)Graphics.Graphics.camera.GetZFromHeight(Graphics.Graphics.screen.Height);
            float zoomFactor = currentZoom / baseZoom;

            return bodyBounds *= zoomFactor;
        }
    }
}
