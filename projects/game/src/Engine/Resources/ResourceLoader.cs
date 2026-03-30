using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using Utils;

namespace Resources
{
    public static class ResourceLoader
    {
        public static readonly string GLOBAL_RES_PATH = "../../../../res/";
        public static readonly string ContentFolderPath = "Content";

        public static Font[] fonts;
        public static Dictionary<SpriteSheets, SpriteSheet> spriteSheets;
        public static Dictionary<Sounds, SoundResource> soundResources;
        public static Dictionary<UITemplates, UITemplate> uiTemplates;
        public static Dictionary<Shaders, ShaderResource> shaders;      // ← new

        public static bool ResourcesLoaded = false;
        public static bool MapLoaded = false;
        public static bool ContentLoaded = false;

        public static void LoadResources()
        {
            ResourcesLoaded = false;

            LoadSprites();
            LoadFonts();
            LoadSounds();
            LoadUITemplates();
            LoadShaders();                                               // ← new

            ResourcesLoaded = true;
        }

        public static void LoadSprites()
        {
            spriteSheets = new Dictionary<SpriteSheets, SpriteSheet>();

            foreach (SpriteSheets spriteEnum in Enum.GetValues(typeof(SpriteSheets)))
            {
                if (spriteEnum == SpriteSheets.NONE)
                    continue;

                spriteSheets[spriteEnum] = new SpriteSheet(spriteEnum);
            }
        }

        public static void LoadFonts()
        {
            string[][] fontAttributesToUse = new string[][]
            {
                new string[] { "Roboto", "12", "Regular" },
                new string[] { "Roboto", "12", "Regular" },
                new string[] { "Roboto", "12", "Regular" },
            };

            fonts = new Font[fontAttributesToUse.Length];
            for (int i = 0; i < fonts.Length; i++)
            {
                fonts[i] = new Font(fontAttributesToUse[i]);
            }
        }

        public static void LoadSounds()
        {
            soundResources = new Dictionary<Sounds, SoundResource>();

            foreach (Sounds soundEnum in Enum.GetValues(typeof(Sounds)))
            {
                if (soundEnum == Sounds.NONE) continue;

                soundResources[soundEnum] = new SoundResource(soundEnum);
            }
        }

        public static void LoadUITemplates()
        {
            uiTemplates = new Dictionary<UITemplates, UITemplate>();

            foreach (UITemplates uiTemplateEnum in Enum.GetValues(typeof(UITemplates)))
            {
                if (uiTemplateEnum == UITemplates.NONE) continue;

                uiTemplates[uiTemplateEnum] = new UITemplate(uiTemplateEnum);
            }
        }

        public static void LoadShaders()
        {
            shaders = new Dictionary<Shaders, ShaderResource>();

            foreach (Shaders shaderEnum in Enum.GetValues(typeof(Shaders)))
            {
                if (shaderEnum == Shaders.NONE) continue;

                shaders[shaderEnum] = new ShaderResource(shaderEnum);
            }
        }
    }
}