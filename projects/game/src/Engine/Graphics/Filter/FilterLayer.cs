using Entities;
using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using MathHelper = Microsoft.Xna.Framework.MathHelper;

namespace Graphics
{
    public class FilterLayer
    {

        public Color DarknessCurrentColor;

        public float DarknessCurrentAlpha;

        public float DarknessMaxAlpha = 0.95f;
        public float DarknessMinAlpha = 0f;

        public AnimationSet aManager;

        private float targetAlpha;
        private float lastUpdateHours;
        private const float AlphaSmoothingSpeed = 0.05f;

        public FilterLayer(Color color, float MaxAlpha = 1f, float MinAlpha = 0f, float CurrentAlpha = 0f, StaticSprites sprites = StaticSprites.LIGHT_DARKNESS_FULL)
        {
            DarknessCurrentColor = color;
            DarknessMaxAlpha = MaxAlpha;
            DarknessMinAlpha = MinAlpha;
            DarknessCurrentAlpha = CurrentAlpha;
            targetAlpha = DarknessCurrentAlpha;
            aManager = new AnimationSet(StaticSpriteFactory.spriteMappings[sprites]);
        }

        public void Update()
        {
            GlobalMapTime globalMapTime = Entities.Entities.EntityMapManager.GlobalMapTime;
            float hours = globalMapTime.TotalGameHours - 12f;

            if (Math.Abs(hours - lastUpdateHours) >= 0.01667f)
            {
                // Calculate target alpha using cosine for day-night cycle
                // Negate cosine to make midnight (0:00/24:00) max alpha, noon (12:00) min alpha
                float timeFactor = -(float)Math.Cos((hours / 12f) * Math.PI);
                targetAlpha = MathHelper.Lerp(DarknessMinAlpha, DarknessMaxAlpha, (timeFactor + 1f) / 2f);
                lastUpdateHours = hours;
            }

            DarknessCurrentAlpha = MathHelper.Lerp(DarknessCurrentAlpha, targetAlpha, AlphaSmoothingSpeed);
        }

        public void Draw()
        {
            Vector2 scale = new Vector2(Graphics.ScreenResolution.X / aManager.GetCurrent().SourceRectangles[0].Width, Graphics.ScreenResolution.Y / aManager.GetCurrent().SourceRectangles[0].Height);
            aManager.DrawCurrent(new Vector2(Graphics.camera.Position.X - Graphics.ScreenResolution.X / 2, Graphics.camera.Position.Y - Graphics.ScreenResolution.Y / 2), DarknessCurrentColor * DarknessCurrentAlpha, 0f, Vector2.Zero, scale, 0f);
        }
    }
}
