using Entities;
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

        public static IOConfigManager IOConfigManager;
        public static IOIniConfigManager IOIniConfigManager;

        public static Font[] FontResources;
        public static Dictionary<SpriteSheets, SpriteSheetResource> SpriteSheetResources;
        public static Dictionary<Sounds, SoundResource> SoundResources;
        public static Dictionary<UITemplates, UITemplateResource> UITemplateResources;
        public static Dictionary<Shaders, ShaderResource> ShaderResources;

        public static Dictionary<EquatableKey, ItemConfig> ItemResources;

        public static bool ResourcesLoaded = false;
        public static bool MapLoaded = false;
        public static bool ContentLoaded = false;

        public static void Init()
        {
            
            //fix the lifecycle to move load and apply to LoadResources()
            IOIniConfigManager = new IOIniConfigManager();
            IOIniConfigManager.Init();
            IOIniConfigManager.Load();
            IOIniConfigManager.ApplyAll();

            IOConfigManager = new IOConfigManager();
            IOConfigManager.Init();
            IOConfigManager.Load();
            IOConfigManager.ApplyAll();
        }

        public static void LoadResources()
        {
            ResourcesLoaded = false;

            

            LoadSprites();
            LoadFonts();
            LoadSounds();
            LoadUITemplates();
            LoadShaders();

            ResourcesLoaded = true;

            ItemResources[new EquatableKey(ItemLib.Weapons.TERRABLADE)].ToItem();
        }

        public static void LoadSprites()
        {
            SpriteSheetResources = new Dictionary<SpriteSheets, SpriteSheetResource>();

            foreach (SpriteSheets spriteEnum in Enum.GetValues(typeof(SpriteSheets)))
            {
                if (spriteEnum == SpriteSheets.NONE)
                    continue;

                SpriteSheetResources[spriteEnum] = new SpriteSheetResource(spriteEnum);
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

            FontResources = new Font[fontAttributesToUse.Length];
            for (int i = 0; i < FontResources.Length; i++)
            {
                FontResources[i] = new Font(fontAttributesToUse[i]);
            }
        }

        public static void LoadSounds()
        {
            SoundResources = new Dictionary<Sounds, SoundResource>();

            foreach (Sounds soundEnum in Enum.GetValues(typeof(Sounds)))
            {
                if (soundEnum == Sounds.NONE) continue;

                SoundResources[soundEnum] = new SoundResource(soundEnum);
            }
        }

        public static void LoadUITemplates()
        {
            UITemplateResources = new Dictionary<UITemplates, UITemplateResource>();

            foreach (UITemplates uiTemplateEnum in Enum.GetValues(typeof(UITemplates)))
            {
                if (uiTemplateEnum == UITemplates.NONE) continue;

                UITemplateResources[uiTemplateEnum] = new UITemplateResource(uiTemplateEnum);
            }
        }

        public static void LoadShaders()
        {
            ShaderResources = new Dictionary<Shaders, ShaderResource>();

            foreach (Shaders shaderEnum in Enum.GetValues(typeof(Shaders)))
            {
                if (shaderEnum == Shaders.NONE) continue;

                ShaderResources[shaderEnum] = new ShaderResource(shaderEnum);
            }
        }
    }
}