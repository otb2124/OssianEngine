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
                { Sprite.Sprites.PLATFORM, new Sprite("entities/platform.png") },
                { Sprite.Sprites.CIRCLE, new Sprite("entities/circle.png") },
                { Sprite.Sprites.HERO, new Sprite("entities/hero.png") },
                { Sprite.Sprites.CURSOR, new Sprite("ui/sprite0.png") },
            };

            fonts = new Font[10];
            fonts[0] = new Font("font0.ttf", 0);
        }
    }
}
