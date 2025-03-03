using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using Utils;

namespace Graphics
{
    public class BackgroundEntity
    {
        public enum BGEntityDynamics
        {
            STATIC,
            CLOUD,
        }

        public Vector2 pos;
        public Vector2 origin;
        public StaticSprites sprite;
        public BGEntityDynamics type;
        public AnimationManager aManager;

        public bool isStickToCamera;
        public bool isStickToZoom;

        public BackgroundEntity(StaticSprites spritePreset, Vector2 pos, BGEntityDynamics type) 
        {
            sprite = spritePreset;
            this.pos = pos;
            this.origin = Vector2.Zero;

            this.aManager = new AnimationManager();
            this.aManager.AddStaticAnimation(this.sprite);

            this.type = type;
        }

        public void Draw()
        {

            Rectangle srcRect = aManager.GetCurrent().GetCurrentFrame();

            Vector2 adjustedPos = pos;
            Vector2 adjustedOrigin = (srcRect.Size.ToVector2() / 2);
            Vector2 adjustedScale = Vector2.One;

            if (this.sprite == StaticSprites.GRAPHICS_SUN)
            {
                adjustedScale = new Vector2(10, 10);
            }
            

            if (isStickToCamera)
            {
                adjustedPos += Graphics.camera.position;
            }
            if (isStickToZoom)
            {
                float currentZoom = (float)Graphics.camera.Z;
                float baseZoom = (float)Graphics.camera.GetZFromHeight(Graphics.screen.Height);
                adjustedScale *= currentZoom / baseZoom;
            }

            Graphics.sprites.Draw(
                 ResourceLoader.spriteSheets[aManager.GetCurrent().spriteSheet].texture,
                 adjustedPos,
                 aManager.GetCurrent().GetCurrentFrame(),
                 Color.White,
                 0f,
                 adjustedOrigin,
                 adjustedScale,
                 SpriteEffects.FlipVertically, 0f);
        }
    }
}
