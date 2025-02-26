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
        public static Dictionary<Sprite.Sprites, Sprite> sprites;


        public static void LoadResources()
        {

            sprites = new Dictionary<Sprite.Sprites, Sprite>
            {

                //ENTITIES
                { Sprite.Sprites.PLATFORM, new Sprite("entities/platform.png") },
                { Sprite.Sprites.CIRCLE, new Sprite("entities/ball.png") },
                { Sprite.Sprites.HERO, new Sprite("entities/hero.png") },
                { Sprite.Sprites.MOB, new Sprite("entities/mob.png") },

                //BG
                { Sprite.Sprites.BACKGROUND, new Sprite("entities/bg.png") { zIndex = -100 } },

                //DECOR
                { Sprite.Sprites.DRAGON, new Sprite("entities/dragon.png") { zIndex = -100 } },

                //UI
                { Sprite.Sprites.CURSOR, new Sprite("ui/sprite0.png") },
            };

            fonts = new Font[10];
            fonts[0] = new Font("font0.ttf", 0);
        }
    }
}
