using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Resources.StaticSpriteFactory;

namespace Graphics
{
    public class ParallaxLayer
    {

        public SpriteData Sprite;
        public float LayerSpeed;
        public Vector2 Position;
        public AnimationManager aManager;

        public bool StickToCameraY;

        //wider = faster
        public ParallaxLayer(SpriteData spriteData, float speed, bool stickToCameraY = false)
        {
            Sprite = spriteData;
            LayerSpeed = speed;
            Position = new Vector2(0, -Graphics.screen.Height/2f);
            StickToCameraY = stickToCameraY;
        }

        public void Init()
        {
            aManager = new AnimationManager();
            aManager.AddStaticAnimation(Sprite);
        }

        public void Update()
        {
            float cameraposX = Graphics.camera.Position.X;

            Position.X = cameraposX * LayerSpeed;

            if(StickToCameraY)
            {
                float cameraposY = Graphics.camera.Position.Y;
                Position.Y = cameraposY;
            }
        }

        public void Draw()
        {
            Rectangle srcRect = aManager.GetCurrent().GetCurrentFrame();

            Vector2 adjustedPos = Position;
            Vector2 adjustedOrigin = new Vector2(srcRect.Width/2, srcRect.Height/2);
            Vector2 adjustedScale = Vector2.One;

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
