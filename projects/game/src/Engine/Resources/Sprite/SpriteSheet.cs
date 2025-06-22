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
                //graphics
                case SpriteSheets.GRAPHICS_PARALLAX_0:
                    return "graphics/parallax/bg0";
                case SpriteSheets.GRAPHICS_STATIC:
                    return "graphics/static";
                case SpriteSheets.GRAPHICS_SUN:
                    return "graphics/sun";
                case SpriteSheets.GRAPHICS_CLOUDS:
                    return "graphics/rain/clouds";

                //entities
                case SpriteSheets.ENTITIES_PLAYER:
                    return "entities/dynamic/player";
                case SpriteSheets.ENTITIES_MOB0:
                    return "entities/dynamic/mob0";
                case SpriteSheets.ENTITIES_STATIC:
                    return "entities/static/static";
                case SpriteSheets.ENTITIES_PLATFORMS:
                    return "entities/static/platforms";
                case SpriteSheets.ENTITIES_WEAPONS:
                    return "entities/equipment/weapons";

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
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }

        public void Load(string path)
        {
            using (FileStream fileStream = new FileStream(ResourceLoader.GLOBAL_RES_PATH + "sprites/" + path + ".png", FileMode.Open))
            {
                this.texture = Texture2D.FromStream(Graphics.Graphics.graphicsDeviceManager.GraphicsDevice, fileStream);
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
