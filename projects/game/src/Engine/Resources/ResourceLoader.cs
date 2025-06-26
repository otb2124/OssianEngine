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
    }
}
