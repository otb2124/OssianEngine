using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class Font : Resource
    {
        public SpriteFont[] fontTypes;
        public string[] fontTypeNames;


        public Font(string[] fontAttr) : base(fontAttr[0] + "_" + fontAttr[1] + "_" + fontAttr[2]) 
        {
            LoadFontTypes();
        }

        public void LoadFontTypes()
        {
            string fontsDirectory = Path.Combine("Content", "res", "fonts");

            string[] fontFiles = Directory.GetFiles(fontsDirectory, "*.xnb");

            if (fontFiles.Length > 0)
            {
                fontTypes = new SpriteFont[fontFiles.Length];
                fontTypeNames = new string[fontFiles.Length];

                for (int i = 0; i < fontFiles.Length; i++)
                {
                    string fontPath = Path.Combine("res", "fonts", Path.GetFileNameWithoutExtension(fontFiles[i]));

                    fontTypes[i] = Graphics.Graphics.contentManager.Load<SpriteFont>(fontPath);
                    fontTypeNames[i] = Path.GetFileNameWithoutExtension(fontFiles[i]);
                }
            }
        }

        public SpriteFont GetCurrentFont()
        {
            for (int i = 0; i < fontTypes.Length; i++)
            {
                if (fontTypeNames[i] == this.path)
                {
                    return fontTypes[i];
                }
            }

            return null;
        }


        public void Draw(string text, Vector2 position, float rotation, Vector2 origin, Vector2 scale, Color color)
        {
            Graphics.Graphics.sprites.DrawString(GetCurrentFont(), text, position, rotation, origin, scale, color, SpriteEffects.None, 0f);
        }
    }

    
}
