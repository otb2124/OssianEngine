using Microsoft.Xna.Framework.Audio;
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


        public static void LoadResources()
        {
            LoadSprites();
            LoadFonts();
            LoadSounds();
        }

        public static void LoadSprites()
        {
            SpriteSheets[] spritesToUse = new SpriteSheets[]
            {
                SpriteSheets.NONE,
                //--------
                //GRAPHICS
                //parallax
                SpriteSheets.GRAPHICS_BG0_CANVAS,
                SpriteSheets.GRAPHICS_BG0_B0,
                SpriteSheets.GRAPHICS_BG0_B1,
                SpriteSheets.GRAPHICS_BG0_B2,
                SpriteSheets.GRAPHICS_BG0_F0,

                SpriteSheets.GRAPHICS_BG1_CANVAS,
                SpriteSheets.GRAPHICS_BG1_B0,
                SpriteSheets.GRAPHICS_BG1_B1,
                SpriteSheets.GRAPHICS_BG1_B2,
                SpriteSheets.GRAPHICS_BG1_B3,
                SpriteSheets.GRAPHICS_BG1_B4,
                SpriteSheets.GRAPHICS_BG1_B5,
                SpriteSheets.GRAPHICS_BG1_F0,


                //static
                SpriteSheets.GRAPHICS_STATIC,
        
                //sun
                SpriteSheets.GRAPHICS_SUN,
                SpriteSheets.GRAPHICS_MOON,

                //rain
                SpriteSheets.GRAPHICS_CLOUDS,

                //--------
                //ENTITIES
                //livingentities
                SpriteSheets.ENTITIES_PLAYER,
                SpriteSheets.ENTITIES_BANDIT,
                SpriteSheets.ENTITIES_SLIME,

                //physicalentities
                SpriteSheets.ENTITIES_STATIC,

                //platforms
                SpriteSheets.ENTITIES_PLATFORMS,
                SpriteSheets.ENTITIES_TILES,
                SpriteSheets.ENTITIES_LEDGES,

                //equipment
                SpriteSheets.ENTITIES_WEAPONS,

                //particles
                SpriteSheets.ENTITIES_PARTICLES,

                //light
                SpriteSheets.LIGHT_DARKNESS_FULL,
                SpriteSheets.LIGHT_DARKNESS_MIN,

                //--
                //UI
                SpriteSheets.UI_CURSOR,
                SpriteSheets.UI_FRAMES,
                SpriteSheets.UI_GAME_ICON,
                SpriteSheets.UI_ICONS,
                SpriteSheets.UI_HUD,
                SpriteSheets.UI_ITEMS,
            };

            spriteSheets = new Dictionary<SpriteSheets, SpriteSheet>();
            foreach (var spriteEnum in spritesToUse)
            {
                spriteSheets[spriteEnum] = new SpriteSheet(spriteEnum);
            }
        }



        public static void LoadFonts()
        {
            string[][] fontAttributesToUse = new string[][]
            {
                new string[]
                {
                    "Roboto", "12", "Regular"
                },
                new string[]
                {
                    "Roboto", "12", "Regular"
                },
                new string[]
                {
                    "Roboto", "12", "Regular"
                },
            };

            fonts = new Font[fontAttributesToUse.Length];
            for (int i = 0; i < fonts.Length; i++)
            {
                fonts[i] = new Font(fontAttributesToUse[i]);
            }
        }


        public static void LoadSounds()
        {
            Sounds[] soundsToUse =
            {
                Sounds.BODY_ARMOR_1,
                Sounds.BODY_ARMOR_2,
                Sounds.BODY_ARMOR_3,
                Sounds.BODY_ARMOR_4,
                Sounds.BODY_HAUBERK_1,
                Sounds.BODY_HAUBERK_2,
                Sounds.BODY_HAUBERK_3,
                Sounds.BODY_HAUBERK_4,
                Sounds.BODY_LOBE_1,
                Sounds.BODY_LOBE_2,
                Sounds.BODY_LOBE_3,
                Sounds.BODY_LOBE_4,
                Sounds.BOW_SHOT1,
                Sounds.BOW_SHOT2,
                Sounds.BOW_SHOT3,
                Sounds.BOW_STANCE1,
                Sounds.BREATH,
                Sounds.DAMAGE1,
                Sounds.DAMAGE2,
                Sounds.DAMAGE3,
                Sounds.DOWNS_KNEE,
                Sounds.FOOT_SOIL_R1,
                Sounds.FOOT_SOIL_R2,
                Sounds.FOOT_SOIL_R3,
                Sounds.FOOT_SOIL_R4,
                Sounds.FOOT_STONE_W1,
                Sounds.FOOT_STONE_W2,
                Sounds.FOOT_STONE_W3,
                Sounds.HUMANOID_FOOTSTEP0,
                Sounds.HUMANOID_FOOTSTEP1,
                Sounds.HUMANOID_FOOTSTEP2,
                Sounds.HUMANOID_HURT,
                Sounds.IRON_CUT_IRON,
                Sounds.IRON_CUT_IRON2,
                Sounds.IRON_CUT_IRON3,
                Sounds.IRON_CUT_MEAT,
                Sounds.IRON_CUT_MEAT2,
                Sounds.MAGIC_FIRE,
                Sounds.MAGIC_FORCE23,
                Sounds.SWING_KATANA,
                Sounds.SWING_SWORD,
                Sounds.SWING_SWORD2,
                Sounds.SWING_SWORD_CHARGE,
                Sounds.TORCH
            };

            soundResources = new Dictionary<Sounds, SoundResource>();
            foreach (var soundsEnum in soundsToUse)
            {
                soundResources[soundsEnum] = new SoundResource(soundsEnum);
            }
        }
    }
}
