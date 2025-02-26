using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.IO;

namespace Resources
{
    public class Sprite : Resource
    {
        public enum Sprites
        {
            PLATFORM,
            CIRCLE,
            HERO,
            CURSOR
        }

        public Texture2D texture;

        public Sprite(string path): base(path)
        {
            this.Load();
        }

        public override void Load()
        {
            using (FileStream fileStream = new FileStream(ResourceLoader.GLOBAL_RES_PATH + "sprites/" + this.path, FileMode.Open))
            {
                this.texture = Texture2D.FromStream(Graphics.Graphics.graphicsDeviceManager.GraphicsDevice, fileStream);
            }
        }

        public void Draw()
        {

        }
    }
}
