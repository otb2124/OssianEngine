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


        //wider = faster
        public ParallaxLayer(SpriteData spriteData)
        {
            Sprite = spriteData;
            LayerSpeed = spriteData.srcRect.Width / 2560f;

            Position = new Vector2();
        }

        public ParallaxLayer(SpriteData spriteData, float speed)
        {
            Sprite = spriteData;
            LayerSpeed = speed;
            Position = new Vector2();
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
        }

        public void Draw()
        {
            Rectangle srcRect = aManager.GetCurrent().GetCurrentFrame();

            Vector2 adjustedPos = Position;
            Vector2 adjustedOrigin = (srcRect.Size.ToVector2() / 2);
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
