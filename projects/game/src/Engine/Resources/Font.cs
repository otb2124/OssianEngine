using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class Font : Resource
    {
        public SpriteFont font;
        public Font(string path, int id) : base(path) 
        {

        }

        public override void Load()
        {
            string path1 = ResourceLoader.GLOBAL_RES_PATH + "fonts/" + path;
        }
    }

    
}
