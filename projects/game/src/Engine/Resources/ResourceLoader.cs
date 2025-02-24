using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class ResourceLoader
    {

        public Font[] fonts;
        public Sprite[] sprites;

        public ResourceLoader() { }
        
        public void LoadResources()
        {
            sprites = new Sprite[10];
            sprites[0] = new Sprite("sprite0.png", 0);

            fonts = new Font[10];
            fonts[0] = new Font("font0.ttf", 0);
        }
    }
}
