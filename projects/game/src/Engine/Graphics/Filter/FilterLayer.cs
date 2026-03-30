using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
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
            aManager = new AnimationSet(StaticSpriteFactory.StaticSpriteMappings[sprites]);
        }

        public void Update()
        {
            GlobalMapTime globalMapTime = Entities.Entities.EntityMapManager.GlobalMapTime;
            float hours = globalMapTime.TotalGameHours - 12f;

            if (Math.Abs(hours - lastUpdateHours) >= 0.01667f)
            {
                float timeFactor = -(float)Math.Cos((hours / 12f) * Math.PI);
                targetAlpha = MathHelper.Lerp(DarknessMinAlpha, DarknessMaxAlpha, (timeFactor + 1f) / 2f);
                lastUpdateHours = hours;
            }

            DarknessCurrentAlpha = MathHelper.Lerp(DarknessCurrentAlpha, targetAlpha, AlphaSmoothingSpeed);
        }

        /// <summary>
        /// Returns this layer's contribution as a light-mask ambient color.
        /// A fully dark layer (black * alpha 1) → ambient Color.Black (no ambient light).
        /// A fully transparent layer (alpha 0)  → ambient Color.White (full ambient light).
        /// Intermediate alphas lerp between white and the layer's tinted color.
        /// </summary>
        public Color GetAmbientContribution()
        {
            // The layer darkens the world by overlaying its color at DarknessCurrentAlpha.
            // In the mask world: alpha=0 means "no darkness" → white ambient (multiply by 1).
            //                    alpha=1 means "full darkness" → the layer's own color ambient.
            // So we lerp from White toward the darkness color as alpha rises.
            return Color.Lerp(Color.White, DarknessCurrentColor, DarknessCurrentAlpha);
        }

        /// <summary>
        /// Draw as a sprite overlay directly onto the backbuffer.
        /// Because FilterManager.Draw() is called after Screen.Unset() and the world blit,
        /// we are on the backbuffer — not inside the Screen render target — so camera
        /// coordinates are irrelevant. We use the same letterboxed destRect that the
        /// world blit uses so the vignette lines up pixel-perfectly.
        /// </summary>
        public void Draw()
        {
            Rectangle dest = Graphics.Screen.GetDestinationRectangle();
            Animation anim = aManager.GetCurrent();
            Texture2D tex = ResourceLoader.spriteSheets[aManager.SpriteSheet].Texture;
            Rectangle src = anim.SourceRectangles[anim.CurrentFrame];

            // Draw directly as a destination rectangle so the sprite is stretched to
            // exactly the letterboxed area — no camera transform, no manual scale math.
            Graphics.Sprites.Draw(tex, dest, src, DarknessCurrentColor * DarknessCurrentAlpha);
        }
    }
}