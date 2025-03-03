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
                    return "graphics/parallax/bg0.png";
                case SpriteSheets.GRAPHICS_STATIC:
                    return "graphics/static.png";
                case SpriteSheets.GRAPHICS_SUN:
                    return "graphics/sun.png";
                case SpriteSheets.GRAPHICS_CLOUDS:
                    return "graphics/rain/clouds.png";

                //entities
                case SpriteSheets.ENTITIES_PLAYER:
                    return "entities/dynamic/player.png";
                case SpriteSheets.ENTITIES_MOB0:
                    return "entities/dynamic/mob0.png";
                case SpriteSheets.ENTITIES_STATIC:
                    return "entities/static/static.png";
                case SpriteSheets.ENTITIES_WEAPONS:
                    return "entities/equipment/weapons.png";

                //UI
                case SpriteSheets.UI_CURSOR:
                    return "ui/cursor.png";
                case SpriteSheets.UI_GAME_ICON:
                    return "ui/gameicon.png";
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }

        public void Load(string path)
        {
            using (FileStream fileStream = new FileStream(ResourceLoader.GLOBAL_RES_PATH + "sprites/" + path, FileMode.Open))
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
