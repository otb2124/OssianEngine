using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Resources
{
    public enum Shaders
    {
        NONE,
        FX_COLOR_GRADE,
        FX_SCREEN_FADE,
        FX_CRT,
        FX_SATURATION,
        FX_BRIGHTNESS_CONTRAST,
        FX_GAMMA_Correction,
        FX_SIMPLE_SHADOW,
        FX_VIGNETTE,
        FX_BLOOM,
        FX_RIM_LIGHT_COMPOSITE,
        FX_ENTITY_LIGHT,
    }

    public class ShaderResource
    {
        public Effect Shader { get; private set; }
        public string ShaderPath { get; private set; }


        public ShaderResource(Shaders key)
        {
            ShaderPath = GetPath(key);
            Load();
        }

        public string GetPath(Shaders key) => PathMap[key];

        public void Load()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(baseDir, "Content", "res", "shaders", ShaderPath + ".mgfxo");

            if (!File.Exists(fullPath))
                fullPath = Path.Combine(baseDir, ShaderPath + ".mgfxo");

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Could not find shader: {ShaderPath}.mgfxo");

            byte[] data = File.ReadAllBytes(fullPath);

            try
            {
                Shader = new Effect(Graphics.Graphics.GraphicsDeviceManager.GraphicsDevice, data);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load shader '{ShaderPath}'. Make sure it was compiled with mgfxc /Profile:DirectX_11\nError: {ex.Message}");
            }
        }

        public static Dictionary<Shaders, string> PathMap = new Dictionary<Shaders, string>()
        {
            { Shaders.NONE,          "None" },
            { Shaders.FX_COLOR_GRADE,"ColorGrade" },
            { Shaders.FX_SCREEN_FADE,"ScreenFade" },
            { Shaders.FX_CRT,        "CRT" },
            { Shaders.FX_SATURATION, "Saturation" },
            { Shaders.FX_BRIGHTNESS_CONTRAST, "BrightnessContrast" },
            { Shaders.FX_SIMPLE_SHADOW, "SimpleShadow" },
            { Shaders.FX_GAMMA_Correction, "GammaCorrection" },
            { Shaders.FX_VIGNETTE, "Vignette" },
            { Shaders.FX_BLOOM,    "Bloom" },
            { Shaders.FX_RIM_LIGHT_COMPOSITE, "RimLightComposite" },
            { Shaders.FX_ENTITY_LIGHT, "EntityLight" },
        };
    }
}