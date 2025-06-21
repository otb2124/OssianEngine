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
        public SpriteFont[] fonts;
        public string[] fontNames;
        public Font(string[] fontAttr) : base(fontAttr[0] + "_" + fontAttr[1] + "_" + fontAttr[2]) 
        {
            SetFonts();
        }

        public void SetFonts()
        {
            string fontsDirectory = Path.Combine("Content", "res", "fonts");

            string[] fontFiles = Directory.GetFiles(fontsDirectory, "*.xnb");

            if (fontFiles.Length > 0)
            {
                fonts = new SpriteFont[fontFiles.Length];
                fontNames = new string[fontFiles.Length];

                for (int i = 0; i < fontFiles.Length; i++)
                {
                    string fontPath = Path.Combine("res", "fonts", Path.GetFileNameWithoutExtension(fontFiles[i]));

                    fonts[i] = Graphics.Graphics.contentManager.Load<SpriteFont>(fontPath);
                    fontNames[i] = Path.GetFileNameWithoutExtension(fontFiles[i]);
                }
            }
        }

        public SpriteFont GetCurrentFont()
        {
            for (int i = 0; i < fonts.Length; i++)
            {
                if (fontNames[i] == this.path)
                {
                    return fonts[i];
                }
            }

            return null;
        }


        public void Draw(string text, Vector2 position, float rotation, Vector2 origin, Vector2 scale, Color color)
        {
            Graphics.Graphics.sprites.DrawString(this.GetCurrentFont(), text, position, rotation, origin, scale, color, SpriteEffects.None, 0f);
        }
    }

    
}
