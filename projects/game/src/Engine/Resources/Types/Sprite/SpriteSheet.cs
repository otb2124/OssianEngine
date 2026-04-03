using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using System.IO;
using Utils;


namespace Resources
{

    public enum SpriteSheets
    {
        NONE,
        //--------
        //GRAPHICS
        //parallax
        GRAPHICS_BG0_CANVAS,
        GRAPHICS_BG0_B0,
        GRAPHICS_BG0_B1,
        GRAPHICS_BG0_B2,
        GRAPHICS_BG0_F0,

        GRAPHICS_BG1_CANVAS,
        GRAPHICS_BG1_B0,
        GRAPHICS_BG1_B1,
        GRAPHICS_BG1_B2,
        GRAPHICS_BG1_B3,
        GRAPHICS_BG1_B4,
        GRAPHICS_BG1_B5,
        GRAPHICS_BG1_F0,

        //static
        GRAPHICS_STATIC,

        //sun
        GRAPHICS_SUN,
        GRAPHICS_MOON,

        //rain
        GRAPHICS_CLOUDS,

        //--------
        //ENTITIES
        //livingentities
        ENTITIES_HUMAN_M,
        ENTITIES_HUMAN_M_ARMOR_CHESTPLATE_0,
        ENTITIES_HUMAN_M_ARMOR_HELMET_0,
        ENTITIES_HUMAN_M_ARMOR_BOOTS_0,
        ENTITIES_HUMAN_M_ARMOR_GLOVES_0,

        ENTITIES_SLIME, ENTITIES_BAT,

        ENITIES_FIREBALL, ENITIES_ARROW,

        //physicalentities
        ENTITIES_STATIC,

        //platforms
        ENTITIES_PLATFORMS,
        ENTITIES_TILES,
        ENTITIES_LEDGES,
        ENTITIES_LADDERS,
        ENTITIES_SPIKES,

        //equipment
        ENTITIES_WEAPONS_TERRABLADE,
        ENTITIES_WEAPONS_TORCH,

        //particles
        ENTITIES_PARTICLES,

        //vfxs
        VFX_EXPLOSION,
        VFX_WATER_STEP,

        //light
        LIGHT_DARKNESS_FULL,
        LIGHT_DARKNESS_MIN,

        //--
        //UI
        UI_CURSOR,
        UI_FRAMES,
        UI_GAME_ICON,
        UI_ICONS,
        UI_HUD,
        UI_ITEMS,


        //utils
        UTILS_TILE_GRID,
    }


    public class SpriteSheet
    {
        
        public SpriteSheets Type;
        public Texture2D Texture;

        public SpriteSheet(string texturePath)
        {
           Load(texturePath);
        }
        public SpriteSheet(SpriteSheets spriteSheetType)
        {
            Type = spriteSheetType;
            Load(GetSpriteSheetTexturePath());
        }

        public string GetSpriteSheetTexturePath()
        {
            return SpritesheetPathMap[Type];
        }

        public void Load(string path)
        {
            using (FileStream fileStream = new FileStream(ResourceLoader.GLOBAL_RES_PATH + "Sprites/" + path + ".png", FileMode.Open))
            {
                Texture = Texture2D.FromStream(Graphics.Graphics.GraphicsDeviceManager.GraphicsDevice, fileStream);
            }
        }




        public int GetTotalNumberOfSprites(Vector2 gridItemSize)
        {
            return GetTotalCols((int)gridItemSize.X) + GetTotalRows((int)gridItemSize.Y);
        }

        public int GetTotalNumberOfSpritesWithoutEmpty(Vector2 gridItemSize)
        {
            int totalSprites = 0;
            int cols = GetTotalCols((int)gridItemSize.X);
            int rows = GetTotalRows((int)gridItemSize.Y);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Rectangle sourceRectangle = new Rectangle(col * (int)gridItemSize.X, row * (int)gridItemSize.Y, (int)gridItemSize.X, (int)gridItemSize.Y);
                    if (!IsSourceRectangleEmpty(sourceRectangle))
                    {
                        totalSprites++;
                    }
                }
            }

            return totalSprites;
        }

        public int GetTotalCols(int spriteWidth)
        {
            return Texture.Width / spriteWidth;
        }

        public int GetTotalRows(int spriteHeight)
        {
            return Texture.Height / spriteHeight;
        }




        private bool IsSourceRectangleEmpty(Rectangle sourceRectangle)
        {
            Color[] textureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(textureData);

            for (int y = sourceRectangle.Y; y < sourceRectangle.Y + sourceRectangle.Height; y++)
            {
                for (int x = sourceRectangle.X; x < sourceRectangle.X + sourceRectangle.Width; x++)
                {
                    if (textureData[y * Texture.Width + x].A != 0)
                    {
                        return false; //found a non-transparent pixel
                    }
                }
            }
            return true;
        }




        public int GetTotalNumberOfSpritesInRow(int rowIndex, Vector2 gridItemSize)
        {
            int totalSprites = 0;
            int cols = GetTotalCols((int)gridItemSize.X);

            for (int col = 0; col < cols; col++)
            {
                Rectangle sourceRectangle = new Rectangle(col * (int)gridItemSize.X, rowIndex * (int)gridItemSize.Y, (int)gridItemSize.X, (int)gridItemSize.Y);
                if (!IsSourceRectangleEmpty(sourceRectangle))
                {
                    totalSprites++;
                }
            }

            return totalSprites;
        }



        public int GetTotalNumberOfSpritesInCol(int colIndex, Vector2 gridItemSize)
        {
            int totalSprites = 0;
            int rows = GetTotalRows((int)gridItemSize.Y);

            for (int row = 0; row < rows; row++)
            {
                Rectangle sourceRectangle = new Rectangle(colIndex * (int)gridItemSize.X, row * (int)gridItemSize.Y, (int)gridItemSize.X, (int)gridItemSize.Y);
                if (!IsSourceRectangleEmpty(sourceRectangle))
                {
                    totalSprites++;
                }
            }

            return totalSprites;
        }

        public static Dictionary<SpriteSheets, string> SpritesheetPathMap = new Dictionary<SpriteSheets, string>
            {
                // graphics – parallax
                { SpriteSheets.GRAPHICS_BG0_CANVAS,                 "graphics/parallax/bg0/canvas" },
                { SpriteSheets.GRAPHICS_BG0_B0,                     "graphics/parallax/bg0/B0" },
                { SpriteSheets.GRAPHICS_BG0_B1,                     "graphics/parallax/bg0/B1" },
                { SpriteSheets.GRAPHICS_BG0_B2,                     "graphics/parallax/bg0/B2" },
                { SpriteSheets.GRAPHICS_BG0_F0,                     "graphics/parallax/bg0/F0" },

                { SpriteSheets.GRAPHICS_BG1_CANVAS,                 "graphics/parallax/bg1/canvas" },
                { SpriteSheets.GRAPHICS_BG1_B0,                     "graphics/parallax/bg1/B0" },
                { SpriteSheets.GRAPHICS_BG1_B1,                     "graphics/parallax/bg1/B1" },
                { SpriteSheets.GRAPHICS_BG1_B2,                     "graphics/parallax/bg1/B2" },
                { SpriteSheets.GRAPHICS_BG1_B3,                     "graphics/parallax/bg1/B3" },
                { SpriteSheets.GRAPHICS_BG1_B4,                     "graphics/parallax/bg1/B4" },
                { SpriteSheets.GRAPHICS_BG1_B5,                     "graphics/parallax/bg1/B5" },
                { SpriteSheets.GRAPHICS_BG1_F0,                     "graphics/parallax/bg1/F0" },

                { SpriteSheets.GRAPHICS_STATIC,                     "graphics/static" },
                { SpriteSheets.GRAPHICS_SUN,                        "graphics/sun" },
                { SpriteSheets.GRAPHICS_MOON,                       "graphics/moon" },
                { SpriteSheets.GRAPHICS_CLOUDS,                     "graphics/rain/clouds" },

                // entities – living
                { SpriteSheets.ENTITIES_HUMAN_M,                    "entities/dynamic/male" },
                { SpriteSheets.ENTITIES_HUMAN_M_ARMOR_CHESTPLATE_0, "entities/dynamic/human_m_armor_chestplate_0" },
                { SpriteSheets.ENTITIES_HUMAN_M_ARMOR_HELMET_0,     "entities/dynamic/human_m_armor_helmet_0" },
                { SpriteSheets.ENTITIES_HUMAN_M_ARMOR_BOOTS_0,      "entities/dynamic/human_m_armor_boots_0" },
                { SpriteSheets.ENTITIES_HUMAN_M_ARMOR_GLOVES_0,     "entities/dynamic/human_m_armor_gloves_0" },

                { SpriteSheets.ENTITIES_SLIME,                      "entities/dynamic/slime" },
                { SpriteSheets.ENTITIES_BAT,                        "entities/dynamic/bat" },

                { SpriteSheets.ENITIES_FIREBALL,                    "entities/dynamic/fireball" },
                { SpriteSheets.ENITIES_ARROW,                       "entities/dynamic/arrow" },

                // entities – static / platforms
                { SpriteSheets.ENTITIES_STATIC,                     "entities/static/static" },
                { SpriteSheets.ENTITIES_TILES,                      "entities/static/tiles" },
                { SpriteSheets.ENTITIES_PLATFORMS,                  "entities/static/platforms" },
                { SpriteSheets.ENTITIES_LEDGES,                     "entities/static/ledges" },
                { SpriteSheets.ENTITIES_LADDERS,                    "entities/static/ladders" },
                { SpriteSheets.ENTITIES_SPIKES,                     "entities/static/spikes" },

                // particles 
                { SpriteSheets.ENTITIES_PARTICLES,                  "entities/static/particles" },

                // vfx
                { SpriteSheets.VFX_EXPLOSION,                       "entities/dynamic/vfxs_explosion" },
                { SpriteSheets.VFX_WATER_STEP,                      "entities/dynamic/vfxs_water_step" },

                // weapon bodies
                { SpriteSheets.ENTITIES_WEAPONS_TERRABLADE,         "entities/equipment/terrablade" },
                { SpriteSheets.ENTITIES_WEAPONS_TORCH,              "entities/equipment/torch" },

                // light
                { SpriteSheets.LIGHT_DARKNESS_FULL,                 "graphics/light/light_darkness_full" },
                { SpriteSheets.LIGHT_DARKNESS_MIN,                  "graphics/light/light_darkness_min" },

                // UI
                { SpriteSheets.UI_CURSOR,                           "ui/cursor" },
                { SpriteSheets.UI_FRAMES,                           "ui/frames" },
                { SpriteSheets.UI_GAME_ICON,                        "ui/gameicon" },
                { SpriteSheets.UI_ICONS,                            "ui/icons" },
                { SpriteSheets.UI_HUD,                              "ui/hud" },
                { SpriteSheets.UI_ITEMS,                            "ui/items" },


                //utils
                { SpriteSheets.NONE,                                "utils/none" },
                { SpriteSheets.UTILS_TILE_GRID,                     "utils/tile_grid" }
            };
    }
}
