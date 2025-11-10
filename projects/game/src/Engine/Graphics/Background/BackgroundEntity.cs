using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using Utils;

namespace Graphics
{
    public class BackgroundEntity
    {

        public Vector2 pos;
        public Vector2 origin;
        public StaticSprites sprite;
        public AnimationSet aManager;

        public bool isStickToCamera;
        public bool isStickToZoom;

        public int LayerToDrawOn;

        public BackgroundEntity(StaticSprites spritePreset, Vector2 pos, int layerToDrawOn) 
        {
            sprite = spritePreset;
            this.pos = pos;
            this.origin = Vector2.Zero;

            aManager = new AnimationSet(StaticSpriteFactory.StaticSpriteMappings[this.sprite]);

            LayerToDrawOn = layerToDrawOn;
        }

        public void Draw()
        {
            Rectangle srcRect = aManager.GetCurrent().GetCurrentFrame();

            Vector2 adjustedPos = pos;
            Vector2 adjustedOrigin = (srcRect.Size.ToVector2() / 2);
            Vector2 adjustedScale = Vector2.One;

            if (this.sprite == StaticSprites.GRAPHICS_SUN || this.sprite ==  StaticSprites.GRAPHICS_MOON)
            {
                adjustedScale = new Vector2(10, 10);
            }
            

            if (isStickToCamera)
            {
                adjustedPos += Graphics.Camera.position;
            }
            if (isStickToZoom)
            {
                float currentZoom = (float)Graphics.Camera.Z;
                float baseZoom = (float)Graphics.Camera.GetZFromHeight(Graphics.Screen.Height);
                adjustedScale *= currentZoom / baseZoom;
            }

            Graphics.Sprites.Draw(
                 ResourceLoader.spriteSheets[aManager.SpriteSheet].Texture,
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
