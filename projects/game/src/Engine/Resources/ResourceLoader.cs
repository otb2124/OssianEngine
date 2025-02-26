using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public static class ResourceLoader
    {

        public static readonly string GLOBAL_RES_PATH = "../../../../res/";

        public static Font[] fonts;
        public static Dictionary<SpriteFactory.Sprites, Sprite> sprites;


        public static void LoadResources()
        {
            LoadSprites();
            fonts = new Font[10];
            fonts[0] = new Font("font0.ttf", 0);
        }

        public static void LoadSprites()
        {
            SpriteFactory.Sprites[] spritesToUse = new SpriteFactory.Sprites[]
            {
                SpriteFactory.Sprites.PLATFORM,
                SpriteFactory.Sprites.CIRCLE,
                SpriteFactory.Sprites.CRATE,
                SpriteFactory.Sprites.HERO,
                SpriteFactory.Sprites.MOB,
                SpriteFactory.Sprites.BACKGROUND,
                SpriteFactory.Sprites.DRAGON,
                SpriteFactory.Sprites.CURSOR
            };
            sprites = new Dictionary<SpriteFactory.Sprites, Sprite>();
            foreach (var spriteEnum in spritesToUse)
            {
                sprites[spriteEnum] = SpriteFactory.CreateSprite(spriteEnum);
            }
        }
    }
}
