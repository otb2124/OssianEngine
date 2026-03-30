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
            // Build full path to the .mgfxo file
            string fullPath = Path.Combine(
                Graphics.Graphics.GraphicsDeviceManager.GraphicsDevice?.Adapter?.Description ?? "", // not needed
                                                                                                   // Better way:
                AppDomain.CurrentDomain.BaseDirectory,   // or use Content.RootDirectory if you prefer
                "Content",
                ShaderPath + ".mgfxo"
            );

            if (!File.Exists(fullPath))
            {
                // Fallback: try without "Content\" if your .mgfxo files are directly in bin/Debug/net6.0-windows/res/shaders/
                fullPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    ShaderPath + ".mgfxo"
                );
            }

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Could not find shader file: {fullPath}");

            byte[] effectData = File.ReadAllBytes(fullPath);
            Shader = new Effect(Graphics.Graphics.GraphicsDeviceManager.GraphicsDevice, effectData);
            Console.WriteLine("shader: " + Shader);
        }

        public static Dictionary<Shaders, string> PathMap = new Dictionary<Shaders, string>()
        {
            { Shaders.NONE,          "res/shaders/None" },
            { Shaders.FX_COLOR_GRADE,"res/shaders/ColorGrade" },
            { Shaders.FX_SCREEN_FADE,"res/shaders/ScreenFade" },
            { Shaders.FX_CRT,        "res/shaders/CRT" },
        };
    }
}