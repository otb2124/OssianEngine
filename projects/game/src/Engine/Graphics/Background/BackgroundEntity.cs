using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.PhysicalEntity;
using static Graphics.Animation;
using UI;
using System.Diagnostics;

namespace Graphics
{
    public class BackgroundEntity
    {

        public Vector2 pos;
        public Vector2 origin;
        public StaticSpriteFactory.StaticSprites sprite;
        public AnimationManager aManager;

        public bool isStickToCamera;
        public bool isStickToZoom;

        public BackgroundEntity(StaticSpriteFactory.StaticSprites spritePreset, Vector2 pos) 
        {
            sprite = spritePreset;
            this.pos = pos;
            this.origin = Vector2.Zero;

            this.aManager = new AnimationManager();
            this.aManager.AddStaticAnimation(this.sprite);
        }

        public void Update()
        {
            
        }

        public void Draw()
        {

            Rectangle srcRect = aManager.GetCurrent().GetCurrentFrame();

            Vector2 adjustedPos = pos;
            Vector2 adjustedOrigin = (srcRect.Size.ToVector2() / 2);

            if (isStickToCamera)
            {
                adjustedPos += Graphics.camera.position;
            }

            Graphics.sprites.Draw(
                 ResourceLoader.spriteSheets[aManager.GetCurrent().spriteSheet].texture,
                 adjustedPos,
                 aManager.GetCurrent().GetCurrentFrame(),
                 Color.White,
                 0f,
                 adjustedOrigin,
                 Vector2.One,
                 SpriteEffects.FlipVertically, 0f);
        }
    }
}
