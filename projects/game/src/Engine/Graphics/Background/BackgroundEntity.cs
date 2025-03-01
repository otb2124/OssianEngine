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
            setSingleAnimation();
        }

        public void setSingleAnimation()
        {
            aManager = new AnimationManager();
            float frameSpeed = 0.0f;
            StaticSpriteFactory.SpriteData data = StaticSpriteFactory.spriteMappings[this.sprite];
            AddAnimation(Directions.LEFT, AnimationStates.IDLE, 1, data.srcRect.Location.ToVector2(), data.srcRect.Size.ToVector2(), frameSpeed, SpriteEffects.None);
        }

        public void AddAnimation(Directions Directions, AnimationStates animationState, int framesCount, Vector2 startPos, Vector2 frameSize, float eachFrameDuration, SpriteEffects effect)
        {
            StaticSpriteFactory.SpriteData data = StaticSpriteFactory.spriteMappings[this.sprite];
            aManager.AddAnimation(new Tuple<Directions, AnimationStates>(Directions, animationState), new Animation(data.sheet, framesCount, startPos, frameSize, eachFrameDuration, effect));
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
