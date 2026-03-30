using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics
{
    // ─────────────────────────────────────────────────────────────────────────
    // ColorGradeEffect
    //
    // Tints the entire screen toward a color at a given intensity.
    // Good for: hurt flash, poison overlay, underwater tint, area transitions.
    //
    // Shader parameters expected in the .fx file:
    //   float4 TintColor   — the color to blend toward
    //   float  Intensity   — 0 = no effect, 1 = fully the tint color
    // ─────────────────────────────────────────────────────────────────────────
    public class ColorGradeEffect : PostProcessEffect
    {
        public Color TintColor = Color.White;
        public float Intensity = 0f;           // 0–1

        public ColorGradeEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["TintColor"]?.SetValue(TintColor.ToVector4());
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // ScreenFadeEffect
    //
    // Fades the screen to a solid color (default black).
    // Drive Alpha from 0 → 1 to fade out, 1 → 0 to fade in.
    //
    // Uses no shader — just draws a solid color quad over the composited frame
    // using AlphaBlend, so it works without any .fx file.
    //
    // Shader parameters expected in the .fx file:
    //   float4 FadeColor  — the fade-to color (usually black)
    //   float  Alpha      — 0 = invisible, 1 = fully opaque
    // ─────────────────────────────────────────────────────────────────────────
    public class ScreenFadeEffect : PostProcessEffect
    {
        public Color FadeColor = Color.Black;
        public float Alpha = 0f;            // 0 = scene visible, 1 = fully faded

        public ScreenFadeEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["FadeColor"]?.SetValue(FadeColor.ToVector4());
            Shader.Parameters["Alpha"]?.SetValue(Alpha);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // CRTEffect
    //
    // Scanlines + slight barrel distortion for a retro CRT feel.
    //
    // Shader parameters expected in the .fx file:
    //   float2 Resolution      — screen size in pixels (for scanline spacing)
    //   float  ScanlineStrength — 0 = no lines, 1 = heavy lines
    //   float  Curvature       — 0 = flat, 0.1–0.3 = subtle barrel warp
    //   float  Time            — elapsed seconds (for scrolling scanlines)
    // ─────────────────────────────────────────────────────────────────────────
    public class CRTEffect : PostProcessEffect
    {
        public float ScanlineStrength = 0.2f;   // 0–1
        public float Curvature = 0.05f;  // 0–0.5

        private float _elapsedSeconds = 0f;

        public CRTEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            _elapsedSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Shader.Parameters["Resolution"]?.SetValue(
                new Vector2(source.Width, source.Height));
            Shader.Parameters["ScanlineStrength"]?.SetValue(ScanlineStrength);
            Shader.Parameters["Curvature"]?.SetValue(Curvature);
            Shader.Parameters["Time"]?.SetValue(_elapsedSeconds);
        }
    }
}