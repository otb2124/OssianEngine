using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;

namespace Graphics
{
    public class ColorGradeEffect : ProcessEffect
    {
        public Color TintColor = Color.White;
        public float Intensity = 0f;

        public ColorGradeEffect(Color tintColor, float intensity) : base() 
        {
            TintColor = tintColor;
            Intensity = intensity;

            ShaderType = Shaders.FX_COLOR_GRADE;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["TintColor"]?.SetValue(TintColor.ToVector4());
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
        }
    }

    public class ScreenFadeEffect : ProcessEffect
    {
        public Color FadeColor = Color.Black;
        public float Alpha = 0f; // 0 = fully visible, 1 = fully faded

        public ScreenFadeEffect(Color fadeColor, float alpha) : base() 
        {
            FadeColor = fadeColor;
            Alpha = alpha;

            ShaderType = Shaders.FX_SCREEN_FADE;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["FadeColor"]?.SetValue(FadeColor.ToVector4());
            Shader.Parameters["Alpha"]?.SetValue(Alpha);
        }
    }

    public class CRTEffect : ProcessEffect
    {
        public float ScanlineStrength = 0.25f; // 0–1
        public float Curvature = 0.08f;        // 0–0.3 recommended
        private float _elapsedSeconds = 0f;

        public CRTEffect(float scanlineStrength, float curvature) : base() 
        {
            ScanlineStrength = scanlineStrength;
            Curvature = curvature;

            ShaderType = Shaders.FX_CRT;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            _elapsedSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Shader.Parameters["Resolution"]?.SetValue(new Vector2(source.Width, source.Height));
            Shader.Parameters["ScanlineStrength"]?.SetValue(ScanlineStrength);
            Shader.Parameters["Curvature"]?.SetValue(Curvature);
            Shader.Parameters["Time"]?.SetValue(_elapsedSeconds);
        }
    }

    public class SaturationEffect : ProcessEffect
    {
        public float Saturation = 1.0f; // 0 = grayscale, 1 = normal, >1 = vibrant

        public SaturationEffect(float saturation) : base() 
        {
            Saturation = saturation;

            ShaderType = Shaders.FX_SATURATION;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Saturation"]?.SetValue(Saturation);
        }
    }

    public class BrightnessContrastEffect : ProcessEffect
    {
        public float Brightness = 1.0f; // 0 = dark, 1 = normal, >1 = bright
        public float Contrast = 1.0f; // 0 = flat, 1 = normal, >1 = high contrast

        public BrightnessContrastEffect(float brightness, float contrast) : base() 
        {
            Brightness = brightness;
            Contrast = contrast;

            ShaderType = Shaders.FX_BRIGHTNESS_CONTRAST;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Brightness"]?.SetValue(Brightness);
            Shader.Parameters["Contrast"]?.SetValue(Contrast);
        }
    }

    public class VignetteEffect : ProcessEffect
    {
        public float Intensity = 0.6f;   // 0 = none, 1 = strong
        public float Radius = 0.85f;  // 0.5–1.0 (how large the vignette area is)

        public VignetteEffect(float intensity, float radius) : base() 
        {
            Intensity = intensity;
            Radius = radius;

            ShaderType = Shaders.FX_VIGNETTE;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
            Shader.Parameters["Radius"]?.SetValue(Radius);
        }
    }

    public class SimpleShadowEffect : ProcessEffect
    {
        public Color ShadowColor = new Color(10, 10, 30); // dark bluish tint
        public float Intensity = 0f; // 0–1

        public SimpleShadowEffect(Color shadowColor, float intensity) : base() 
        {
            ShadowColor = shadowColor;
            Intensity = intensity;

            ShaderType = Shaders.FX_SIMPLE_SHADOW;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["ShadowColor"]?.SetValue(ShadowColor.ToVector4());
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
        }
    }

    public class GammaCorrectionEffect : ProcessEffect
    {
        public float Gamma = 2.2f; // Standard monitor gamma (usually 2.2)

        public GammaCorrectionEffect(float gamma) : base() 
        {
            Gamma = gamma;

            ShaderType = Shaders.FX_GAMMA_Correction;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Gamma"]?.SetValue(Gamma);
        }
    }

    public class BloomEffect : ProcessEffect
    {
        public float Threshold { get; set; } = 0.4f;
        public float Intensity { get; set; } = 1.5f;
        public float Radius { get; set; } = 3.0f;

        public BloomEffect(float threshold, float intensity, float radius) : base() 
        {
            Threshold = threshold;
            Intensity = intensity;
            Radius = radius;

            ShaderType = Shaders.FX_BLOOM;
        }

        // No Apply() override needed — bloom is always multipass

        public override void ApplyMultiPass(Sprites sprites,
            RenderTarget2D source, RenderTarget2D target, RenderTarget2D scratch)
        {
            var gd = Graphics.GraphicsDeviceManager.GraphicsDevice;

            Vector2 resolution = new Vector2(source.Width, source.Height);

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
            Shader.Parameters["Radius"]?.SetValue(Radius);
            Shader.Parameters["Resolution"]?.SetValue(resolution);
            Shader.Parameters["ScreenTexture"]?.SetValue(scratch);
            Shader.CurrentTechnique.Passes[1].Apply();
            sprites.Begin(null, BlendState.Opaque, Shader);
            sprites.DrawRT(scratch, fullRect, Color.White);
            sprites.End();

            // Pass 3: blur V + composite, reuse scratch as intermediate then blit to target
            gd.SetRenderTarget(scratch);
            gd.Clear(new Color(0, 0, 0, 0));
            Shader.Parameters["Radius"]?.SetValue(Radius);
            Shader.Parameters["Resolution"]?.SetValue(resolution);  // ← set again before Apply()
            Shader.Parameters["ScreenTexture"]?.SetValue(target);
            Shader.Parameters["OriginalTexture"]?.SetValue(source);
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
            Shader.CurrentTechnique.Passes[2].Apply();
            sprites.Begin(null, BlendState.Opaque, Shader);
            sprites.DrawRT(target, fullRect, Color.White);
            sprites.End();

            // Copy scratch → target so result is always in target
            gd.SetRenderTarget(target);
            gd.Clear(new Color(0, 0, 0, 0));
            sprites.Begin(null, BlendState.Opaque);
            Shader.Parameters["Resolution"]?.SetValue(resolution);
            Shader.Parameters["Radius"]?.SetValue(Radius);
            sprites.DrawRT(scratch, fullRect, Color.White, SpriteEffects.FlipVertically);
            sprites.End();
        }
    }

    public class RimLightEffect : ProcessEffect
    {
        public float Intensity = 0.75f;     // Overall strength of the rim glow
        public float Power = 3.2f;      // Higher = thinner, sharper rim
        public Color RimColor = new Color(255, 245, 210); // Warm golden rim (Fable 2 style)

        public RimLightEffect(float intensity, float power, Color rimColor) : base() 
        {
            Intensity = intensity;
            Power = power;
            RimColor = rimColor;

            ShaderType = Shaders.FX_RIM_LIGHT_COMPOSITE;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["RimIntensity"]?.SetValue(Intensity);
            Shader.Parameters["RimPower"]?.SetValue(Power);
            Shader.Parameters["RimColor"]?.SetValue(RimColor.ToVector4());
        }
    }


    public class EntityLightingEffect : ProcessEffect
    {
        public Color AmbientColor = new Color(70, 65, 100);

        private static readonly int MAX_LIGHTS = 3;

        private readonly Vector2[] _lightPositions = new Vector2[MAX_LIGHTS];
        private readonly Vector4[] _lightColors = new Vector4[MAX_LIGHTS];
        private readonly float[] _lightRadii = new float[MAX_LIGHTS];
        private readonly float[] _lightIntensity = new float[MAX_LIGHTS];
        private int _activeLights = 0;

        public EntityLightingEffect() : base() 
        {
            ShaderType = Shaders.FX_ENTITY_LIGHT;
        }

        /// <summary>
        /// Add a light that affects this entity.
        /// lightWorldPos = position in world coordinates
        /// entityWorldPos = this entity's position in world
        /// entitySize = approximate size of entity in world units
        /// </summary>
        public void AddLight(Vector2 lightWorldPos, Vector2 entityWorldPos, Vector2 entitySize,
                             Color color, float radius, float intensity = 1.0f)
        {
            if (_activeLights >= 6) return;

            // Convert light position to UV space relative to this entity
            Vector2 relative = (lightWorldPos - entityWorldPos) / entitySize;
            relative = relative * 0.5f + new Vector2(0.5f);   // convert to 0..1 UV

            _lightPositions[_activeLights] = relative;
            _lightColors[_activeLights] = color.ToVector4();
            _lightRadii[_activeLights] = radius / entitySize.Length(); // normalize radius
            _lightIntensity[_activeLights] = intensity;

            _activeLights++;
        }

        public void ClearLights() => _activeLights = 0;

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["AmbientColor"]?.SetValue(AmbientColor.ToVector4());
            Shader.Parameters["ActiveLights"]?.SetValue(_activeLights);
            Shader.Parameters["LightPositions"]?.SetValue(_lightPositions);
            Shader.Parameters["LightColors"]?.SetValue(_lightColors);
            Shader.Parameters["LightRadii"]?.SetValue(_lightRadii);
            Shader.Parameters["LightIntensity"]?.SetValue(_lightIntensity);
        }
    }


    public class ChromaticAberrationEffect : ProcessEffect
    {
        public float Intensity = 0.0f;  // animate this on hit/screenshake

        public ChromaticAberrationEffect(float intensity) : base() 
        {
            Intensity = intensity;

            ShaderType = Shaders.FX_CHROMATIC_ABERRATION;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
        }
    }

    public class DistortionEffect : ProcessEffect
    {
        public float Intensity = 0.0f;  // set to ~0.01 during screenshake, then decay
        private float _elapsed = 0f;

        public DistortionEffect(float intensity) : base() 
        {
            Intensity = intensity;

            ShaderType = Shaders.FX_DISTORTION;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            _elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
            Shader.Parameters["Time"]?.SetValue(_elapsed);
        }
    }

    public class OutlineEffect : ProcessEffect
    {
        public Color OutlineColor = Color.White;
        public float OutlineThickness = 1.5f;

        public OutlineEffect(Color outlineColor, float outlineThickness) : base() 
        {
            OutlineColor = outlineColor;
            OutlineThickness = outlineThickness;

            ShaderType = Shaders.FX_OUTLINE;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["OutlineColor"]?.SetValue(OutlineColor.ToVector4());
            Shader.Parameters["OutlineThickness"]?.SetValue(OutlineThickness);
            Shader.Parameters["TextureSize"]?.SetValue(
                new Vector2(source.Width, source.Height));
        }
    }

    public class DissolveEffect : ProcessEffect
    {
        public float Progress = 0.0f;
        public Color EdgeColor = new Color(210, 180, 140, 255);  // ash/sand
        public float EdgeWidth = 0.08f;

        private float _elapsed = 0f;

        public DissolveEffect(Color edgeColor, float edgeWidth) : base() 
        {
            EdgeColor = edgeColor;
            EdgeWidth = edgeWidth;

            ShaderType = Shaders.FX_DISSOLVE;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["Progress"]?.SetValue(Progress);
            Shader.Parameters["EdgeColor"]?.SetValue(EdgeColor.ToVector4());
            Shader.Parameters["EdgeWidth"]?.SetValue(EdgeWidth);
            Shader.Parameters["Time"]?.SetValue(_elapsed);
        }

        public bool IsComplete => Progress >= 1.0f;
    }

    public class HitFlashEffect : ProcessEffect
    {
        public Color FlashColor = Color.White;
        public float FlashIntensity = 0.0f;     // set to 1 on hit, decay each frame

        private float _decaySpeed = 8.0f;       // units per second

        public HitFlashEffect(Color flashColor, float flashIntensity) : base() 
        {
            FlashColor = flashColor;
            FlashIntensity = flashIntensity;

            ShaderType = Shaders.FX_HIT_FLASH;
        }

        public override void Trigger()
        {
            FlashColor = Color.White;
            FlashIntensity = 1.0f;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            // Auto-decay so you only need to call Trigger() on hit
            FlashIntensity = Math.Max(0f,
                FlashIntensity - _decaySpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            Shader.Parameters["FlashColor"]?.SetValue(FlashColor.ToVector4());
            Shader.Parameters["FlashIntensity"]?.SetValue(FlashIntensity);
        }
    }

    public class ColorIsolationEffect : ProcessEffect
    {
        public Color TargetColor = Color.Red;
        public float Tolerance = 0.3f;   // higher = more colors preserved
        public float Smoothness = 0.05f;  // soft edge width

        public ColorIsolationEffect(Color target, float tolerance, float smoothness) : base() 
        {
            TargetColor = target;
            Tolerance = tolerance;
            Smoothness = smoothness;

            ShaderType = Shaders.FX_COLOR_ISOLATION;
        }

        public override void Apply(Texture2D source, GameTime gameTime)
        {
            Shader.Parameters["TargetColor"]?.SetValue(TargetColor.ToVector4());
            Shader.Parameters["Tolerance"]?.SetValue(Tolerance);
            Shader.Parameters["Smoothness"]?.SetValue(Smoothness);
        }
    }


    public class BurningEffect : ProcessEffect
    {
        public float Intensity = 1.0f;
        public Vector2 Radius = new Vector2(40f, 40f);
        public bool FlipX = false;   // set from Model.Direction

        private float _elapsed = 0f;

        public BurningEffect(float intensity, float radius) : base() 
        {
            Intensity = intensity;
            Radius = new Vector2(radius, radius);

            ShaderType = Shaders.FX_BURNING;
        }

        public override void ApplyMultiPass(
            Sprites sprites,
            RenderTarget2D source,
            RenderTarget2D target,
            RenderTarget2D scratch)
        {
            var gd = Graphics.GraphicsDeviceManager.GraphicsDevice;
            Rectangle fullRect = new Rectangle(0, 0, source.Width, source.Height);
            Vector2 resolution = new Vector2(source.Width, source.Height);

            _elapsed += (float)Graphics._lastGameTime.ElapsedGameTime.TotalSeconds;

            // Pass 1: expand silhouette — source → scratch
            gd.SetRenderTarget(scratch);
            gd.Clear(new Color(0, 0, 0, 0));
            sprites.Begin(null, BlendState.AlphaBlend, Shader);
            Shader.Parameters["ScreenTexture"]?.SetValue(source);
            Shader.Parameters["RadiusX"]?.SetValue(Radius.X / source.Width);
            Shader.Parameters["RadiusY"]?.SetValue(Radius.Y / source.Height);
            Shader.Parameters["Resolution"]?.SetValue(resolution);
            Shader.CurrentTechnique.Passes[0].Apply();
            sprites.DrawRT(source, fullRect, Color.White);
            sprites.End();

            // Pass 2: color fire — scratch → target
            gd.SetRenderTarget(target);
            gd.Clear(new Color(0, 0, 0, 0));
            sprites.Begin(null, BlendState.AlphaBlend, Shader);
            Shader.Parameters["ScreenTexture"]?.SetValue(scratch);
            Shader.Parameters["Time"]?.SetValue(_elapsed);
            Shader.Parameters["Intensity"]?.SetValue(Intensity);
            Shader.CurrentTechnique.Passes[1].Apply();
            sprites.DrawRT(scratch, fullRect, Color.White);
            sprites.End();

            // Final: copy flame result to scratch flipped, then draw original entity on top
            gd.SetRenderTarget(scratch);
            gd.Clear(new Color(0, 0, 0, 0));
            sprites.Begin(null, BlendState.Opaque);
            sprites.DrawRT(target, fullRect, Color.White);  // flame aura
            sprites.End();

            // Draw original entity on top — flipped to match
            sprites.Begin(null, BlendState.NonPremultiplied);
            sprites.DrawRT(source, fullRect, Color.White);  // entity over flame
            sprites.End();

            // Copy scratch → target so caller reads rt[dst]
            gd.SetRenderTarget(target);
            gd.Clear(new Color(0, 0, 0, 0));
            sprites.Begin(null, BlendState.Opaque);
            sprites.DrawRT(scratch, fullRect, Color.White);
            sprites.End();
        }
    }
}