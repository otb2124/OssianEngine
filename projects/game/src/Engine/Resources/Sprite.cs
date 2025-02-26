using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            CURSOR,
            BACKGROUND,
            MOB,
            DRAGON,
        }

        public Texture2D texture;
        public int zIndex;

        public Sprite(string path): base(path)
        {
            this.Load();
        }

        public override void Load()
        {
            using (FileStream fileStream = new FileStream(ResourceLoader.GLOBAL_RES_PATH + "sprites/" + this.path, FileMode.Open))
            {
                this.texture = Texture2D.FromStream(Graphics.Graphics.graphicsDeviceManager.GraphicsDevice, fileStream);
                this.zIndex = 0;
            }
        }

        public void Draw(Vector2 pos, Rectangle srcRect, Color color, float rot, Vector2 origin, Vector2 scale, SpriteEffects effect, float layerDepth)
        {
            Graphics.Graphics.sprites.Draw(
                texture,
                pos,
                srcRect,
                color,
                rot,
                origin,
                scale,
                effect,
                layerDepth
             );
        }


        public void Draw(Vector2 pos)
        {
            Draw(
                pos,
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                0f,
                new Vector2(texture.Width/2, texture.Height/2),
                Vector2.One,
                SpriteEffects.FlipVertically,
                0f
            );
        }
    }
}
