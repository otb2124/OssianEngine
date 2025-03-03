using System.Collections.Generic;
using Utils;

namespace Resources
{
    public static class ResourceLoader
    {

        public static readonly string GLOBAL_RES_PATH = "../../../../res/";

        public static Font[] fonts;
        public static Dictionary<SpriteSheets, SpriteSheet> spriteSheets;


        public static void LoadResources()
        {
            LoadSprites();
        }

        public static void LoadSprites()
        {
            SpriteSheets[] spritesToUse = new SpriteSheets[]
            {
                SpriteSheets.DECOR,
                SpriteSheets.HERO,
                SpriteSheets.MOB,
                SpriteSheets.BACKGROUND,
                SpriteSheets.BG_CLOUDS,
                SpriteSheets.BG_SUN,
                SpriteSheets.DRAGON,
                SpriteSheets.UI,
                SpriteSheets.WEAPONS
            };
            spriteSheets = new Dictionary<SpriteSheets, SpriteSheet>();
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
