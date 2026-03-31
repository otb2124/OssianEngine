using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics
{
    // ─────────────────────────────────────────────────────────────────────────
    // ColorGradeEffect
    // ─────────────────────────────────────────────────────────────────────────
    public class ColorGradeEffect : PostProcessEffect
    {
        public Color TintColor = Color.White;
        public float Intensity = 0f; // 0 = no tint, 1 = full tint

        public ColorGradeEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["TintColor"]?.SetValue(TintColor.ToVector4());
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // ScreenFadeEffect
    // ─────────────────────────────────────────────────────────────────────────
    public class ScreenFadeEffect : PostProcessEffect
    {
        public Color FadeColor = Color.Black;
        public float Alpha = 0f; // 0 = fully visible, 1 = fully faded

        public ScreenFadeEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["FadeColor"]?.SetValue(FadeColor.ToVector4());
            Shader.Parameters["Alpha"]?.SetValue(Alpha);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // CRTEffect
    // ─────────────────────────────────────────────────────────────────────────
    public class CRTEffect : PostProcessEffect
    {
        public float ScanlineStrength = 0.25f; // 0–1
        public float Curvature = 0.08f;        // 0–0.3 recommended
        private float _elapsedSeconds = 0f;

        public CRTEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            _elapsedSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Shader.Parameters["Resolution"]?.SetValue(new Vector2(source.Width, source.Height));
            Shader.Parameters["ScanlineStrength"]?.SetValue(ScanlineStrength);
            Shader.Parameters["Curvature"]?.SetValue(Curvature);
            Shader.Parameters["Time"]?.SetValue(_elapsedSeconds);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // SaturationEffect
    // ─────────────────────────────────────────────────────────────────────────
    public class SaturationEffect : PostProcessEffect
    {
        public float Saturation = 1.0f; // 0 = grayscale, 1 = normal, >1 = vibrant

        public SaturationEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Saturation"]?.SetValue(Saturation);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // BrightnessContrastEffect
    // ─────────────────────────────────────────────────────────────────────────
    public class BrightnessContrastEffect : PostProcessEffect
    {
        public float Brightness = 1.0f; // 0 = dark, 1 = normal, >1 = bright
        public float Contrast = 1.0f; // 0 = flat, 1 = normal, >1 = high contrast

        public BrightnessContrastEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Brightness"]?.SetValue(Brightness);
            Shader.Parameters["Contrast"]?.SetValue(Contrast);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // VignetteEffect
    // ─────────────────────────────────────────────────────────────────────────
    public class VignetteEffect : PostProcessEffect
    {
        public float Intensity = 0.6f;   // 0 = none, 1 = strong
        public float Radius = 0.85f;  // 0.5–1.0 (how large the vignette area is)

        public VignetteEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
            Shader.Parameters["Radius"]?.SetValue(Radius);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // SimpleShadowEffect
    // ─────────────────────────────────────────────────────────────────────────
    public class SimpleShadowEffect : PostProcessEffect
    {
        public Color ShadowColor = new Color(10, 10, 30); // dark bluish tint
        public float Intensity = 0f; // 0–1

        public SimpleShadowEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["ShadowColor"]?.SetValue(ShadowColor.ToVector4());
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // GammaCorrectionEffect
    // ─────────────────────────────────────────────────────────────────────────
    public class GammaCorrectionEffect : PostProcessEffect
    {
        public float Gamma = 2.2f; // Standard monitor gamma (usually 2.2)

        public GammaCorrectionEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Gamma"]?.SetValue(Gamma);
        }
    }

    public class BloomEffect : PostProcessEffect
    {
        public float Threshold { get; set; } = 0.4f;
        public float Intensity { get; set; } = 1.5f;
        public float Radius { get; set; } = 3.0f;

        public BloomEffect(Effect shader) : base(shader) { }

        // No Apply() override needed — bloom is always multipass

        public void ApplyMultiPass(
            GraphicsDevice gd, Sprites sprites,
            RenderTarget2D source, RenderTarget2D target, RenderTarget2D scratch,
            GameTime gameTime)
        {
            Shader.Parameters["Threshold"]?.SetValue(Threshold);
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
            Shader.Parameters["Radius"]?.SetValue(Radius);

            Rectangle fullRect = new Rectangle(0, 0, source.Width, source.Height);

            // Pass 1: extract → scratch
            gd.SetRenderTarget(scratch);
            gd.Clear(new Color(0, 0, 0, 0));
            Shader.Parameters["ScreenTexture"]?.SetValue(source);
            Shader.CurrentTechnique.Passes[0].Apply();
            sprites.Begin(null, BlendState.Opaque, Shader);
            sprites.DrawRT(source, fullRect, Color.White);
            sprites.End();

            // Pass 2: blur H scratch → target
            gd.SetRenderTarget(target);
            gd.Clear(new Color(0, 0, 0, 0));
            Shader.Parameters["ScreenTexture"]?.SetValue(scratch);
            Shader.CurrentTechnique.Passes[1].Apply();
            sprites.Begin(null, BlendState.Opaque, Shader);
            sprites.DrawRT(scratch, fullRect, Color.White);
            sprites.End();

            // Pass 3: blur V + composite, reuse scratch as intermediate then blit to target
            gd.SetRenderTarget(scratch);
            gd.Clear(new Color(0, 0, 0, 0));
            Shader.Parameters["ScreenTexture"]?.SetValue(target);
            Shader.Parameters["OriginalTexture"]?.SetValue(source);
            Shader.CurrentTechnique.Passes[2].Apply();
            sprites.Begin(null, BlendState.Opaque, Shader);
            sprites.DrawRT(target, fullRect, Color.White);
            sprites.End();

            // Copy scratch → target so result is always in target
            gd.SetRenderTarget(target);
            gd.Clear(new Color(0, 0, 0, 0));
            sprites.Begin(null, BlendState.Opaque);
            sprites.DrawRT(scratch, fullRect, Color.White, SpriteEffects.FlipVertically);
            sprites.End();
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // RimLightEffect - Adds soft glowing halo around bright edges (makes entities pop)
    // ─────────────────────────────────────────────────────────────────────────
    public class RimLightEffect : PostProcessEffect
    {
        public float Intensity = 0.75f;     // Overall strength of the rim glow
        public float Power = 3.2f;      // Higher = thinner, sharper rim
        public Color RimColor = new Color(255, 245, 210); // Warm golden rim (Fable 2 style)

        public RimLightEffect(Effect shader) : base(shader) { }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["RimIntensity"]?.SetValue(Intensity);
            Shader.Parameters["RimPower"]?.SetValue(Power);
            Shader.Parameters["RimColor"]?.SetValue(RimColor.ToVector4());
        }
    }
}