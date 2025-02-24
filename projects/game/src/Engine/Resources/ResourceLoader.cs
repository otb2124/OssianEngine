using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public static class ResourceLoader
    {

        public static Font[] fonts;
        public static Sprite[] sprites;
        public static void LoadResources()
        {
            sprites = new Sprite[10];
            sprites[0] = new Sprite("sprite0.png", 0);

            fonts = new Font[10];
            fonts[0] = new Font("font0.ttf", 0);
        }
    }
}
