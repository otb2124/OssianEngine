using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Utils;


namespace Resources
{
    public class SpriteSheet
    {
        
        public SpriteSheets id;
        public Texture2D texture;

        public SpriteSheet(string texturePath)
        {
           Load(texturePath);
        }
        public SpriteSheet(SpriteSheets spriteSheetId)
        {
            this.id = spriteSheetId;
            Load(GetSpriteSheetTexturePath());
        }

        public string GetSpriteSheetTexturePath()
        {

            switch (id)
            {
                case SpriteSheets.NONE:
                    return "utils/none";
                //graphics
                case SpriteSheets.GRAPHICS_BG0_CANVAS:
                    return "graphics/parallax/bg0/canvas";
                case SpriteSheets.GRAPHICS_BG0_B0:
                    return "graphics/parallax/bg0/B0";
                case SpriteSheets.GRAPHICS_BG0_B1:
                    return "graphics/parallax/bg0/B1";
                case SpriteSheets.GRAPHICS_BG0_B2:
                    return "graphics/parallax/bg0/B2";
                case SpriteSheets.GRAPHICS_BG0_F0:
                    return "graphics/parallax/bg0/F0";



                //graphics
                case SpriteSheets.GRAPHICS_BG1_CANVAS:
                    return "graphics/parallax/bg1/canvas";
                case SpriteSheets.GRAPHICS_BG1_B0:
                    return "graphics/parallax/bg1/B0";
                case SpriteSheets.GRAPHICS_BG1_B1:
                    return "graphics/parallax/bg1/B1";
                case SpriteSheets.GRAPHICS_BG1_B2:
                    return "graphics/parallax/bg1/B2";
                case SpriteSheets.GRAPHICS_BG1_B3:
                    return "graphics/parallax/bg1/B3";
                case SpriteSheets.GRAPHICS_BG1_B4:
                    return "graphics/parallax/bg1/B4";
                case SpriteSheets.GRAPHICS_BG1_B5:
                    return "graphics/parallax/bg1/B5";
                case SpriteSheets.GRAPHICS_BG1_F0:
                    return "graphics/parallax/bg1/F0";


                case SpriteSheets.GRAPHICS_STATIC:
                    return "graphics/static";
                case SpriteSheets.GRAPHICS_SUN:
                    return "graphics/sun";
                case SpriteSheets.GRAPHICS_MOON:
                    return "graphics/moon";
                case SpriteSheets.GRAPHICS_CLOUDS:
                    return "graphics/rain/clouds";

                //entities
                case SpriteSheets.ENTITIES_HUMAN_M:
                    return "entities/dynamic/human_m_draft";
                case SpriteSheets.ENTITIES_HUMAN_M_DEBUG:
                    return "entities/dynamic/human_m_debug";
                case SpriteSheets.ENTITIES_HUMAN_M_ARMOR_CHESTPLATE_0:
                    return "entities/dynamic/human_m_armor_chestplate_0";
                case SpriteSheets.ENTITIES_HUMAN_M_ARMOR_HELMET_0:
                    return "entities/dynamic/human_m_armor_helmet_0";
                case SpriteSheets.ENTITIES_HUMAN_M_ARMOR_BOOTS_0:
                    return "entities/dynamic/human_m_armor_boots_0";
                case SpriteSheets.ENTITIES_HUMAN_M_ARMOR_GLOVES_0:
                    return "entities/dynamic/human_m_armor_gloves_0";

                case SpriteSheets.ENTITIES_SLIME:
                    return "entities/dynamic/slime";
                case SpriteSheets.ENTITIES_BAT:
                    return "entities/dynamic/bat";

                case SpriteSheets.ENITIES_FIREBALL:
                    return "entities/dynamic/fireball";
                case SpriteSheets.ENITIES_ARROW:
                    return "entities/dynamic/arrow";

                case SpriteSheets.ENTITIES_STATIC:
                    return "entities/static/static";
                case SpriteSheets.ENTITIES_TILES:
                    return "entities/static/tiles";
                case SpriteSheets.ENTITIES_PLATFORMS:
                    return "entities/static/platforms";
                case SpriteSheets.ENTITIES_LEDGES:
                    return "entities/static/ledges";

                case SpriteSheets.ENTITIES_PARTICLES:
                    return "entities/static/particles";
                case SpriteSheets.ENTITIES_WEAPONS_TERRABLADE:
                    return "entities/equipment/terrablade";
                case SpriteSheets.ENTITIES_WEAPONS_TORCH:
                    return "entities/equipment/torch";

                //light
                case SpriteSheets.LIGHT_DARKNESS_FULL:
                    return "graphics/light/light_darkness_full";
                case SpriteSheets.LIGHT_DARKNESS_MIN:
                    return "graphics/light/light_darkness_min";

                //UI
                case SpriteSheets.UI_CURSOR:
                    return "ui/cursor";
                case SpriteSheets.UI_FRAMES:
                    return "ui/frames";
                case SpriteSheets.UI_GAME_ICON:
                    return "ui/gameicon";
                case SpriteSheets.UI_ICONS:
                    return "ui/icons";
                case SpriteSheets.UI_HUD:
                    return "ui/hud";
                case SpriteSheets.UI_ITEMS:
                    return "ui/items";
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }

        public void Load(string path)
        {
            using (FileStream fileStream = new FileStream(ResourceLoader.GLOBAL_RES_PATH + "sprites/" + path + ".png", FileMode.Open))
            {
                texture = Texture2D.FromStream(Graphics.Graphics.graphicsDeviceManager.GraphicsDevice, fileStream);
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
            return texture.Width / spriteWidth;
        }

        public int GetTotalRows(int spriteHeight)
        {
            return texture.Height / spriteHeight;
        }




        private bool IsSourceRectangleEmpty(Rectangle sourceRectangle)
        {
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);

            for (int y = sourceRectangle.Y; y < sourceRectangle.Y + sourceRectangle.Height; y++)
            {
                for (int x = sourceRectangle.X; x < sourceRectangle.X + sourceRectangle.Width; x++)
                {
                    if (textureData[y * texture.Width + x].A != 0)
                    {
                        return false; // Found a non-transparent pixel
                    }
                }
            }
            return true; // All pixels are transparent
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
    }
}
