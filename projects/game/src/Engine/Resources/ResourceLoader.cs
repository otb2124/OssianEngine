using System.Collections.Generic;

namespace Resources
{
    public static class ResourceLoader
    {

        public static readonly string GLOBAL_RES_PATH = "../../../../res/";

        public static Font[] fonts;
        public static Dictionary<SpriteSheet.SpriteSheets, SpriteSheet> spriteSheets;


        public static void LoadResources()
        {
            LoadSprites();
        }

        public static void LoadSprites()
        {
            SpriteSheet.SpriteSheets[] spritesToUse = new SpriteSheet.SpriteSheets[]
            {
                SpriteSheet.SpriteSheets.DECOR,
                SpriteSheet.SpriteSheets.HERO,
                SpriteSheet.SpriteSheets.MOB,
                SpriteSheet.SpriteSheets.BACKGROUND,
                SpriteSheet.SpriteSheets.DRAGON,
                SpriteSheet.SpriteSheets.CURSOR,
                SpriteSheet.SpriteSheets.WEAPONS
            };
            spriteSheets = new Dictionary<SpriteSheet.SpriteSheets, SpriteSheet>();
            foreach (var spriteEnum in spritesToUse)
            {
                spriteSheets[spriteEnum] = new SpriteSheet(spriteEnum);
            }
        }

        public static void LoadFonts()
        {
            fonts = new Font[10];
            fonts[0] = new Font("font0.ttf", 0);
        }
    }
}
