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
                //--------
                //GRAPHICS
                //parallax
                SpriteSheets.GRAPHICS_PARALLAX_0,

                //static
                SpriteSheets.GRAPHICS_STATIC,
        
                //sun
                SpriteSheets.GRAPHICS_SUN,

                //rain
                SpriteSheets.GRAPHICS_CLOUDS,

                //--------
                //ENTITIES
                //livingentities
                SpriteSheets.ENTITIES_PLAYER,
                SpriteSheets.ENTITIES_MOB0,

                //physicalentities
                SpriteSheets.ENTITIES_STATIC,

                //platforms
                SpriteSheets.ENTITIES_PLATFORMS,

                //equipment
                SpriteSheets.ENTITIES_WEAPONS,

                //--
                //UI
                SpriteSheets.UI_CURSOR,
                SpriteSheets.UI_GAME_ICON
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
