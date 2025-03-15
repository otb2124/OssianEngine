using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources;
using SharpDX.Direct2D1.Effects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using UI;
using Utils;

namespace Graphics
{
    public class Animation
    {

        public SpriteSheets spriteSheet;
        public List<Rectangle> sourceRectangles = new List<Rectangle>();
        public int frames;
        public int currentFrame;
        public float frameTime;
        public float frameTimeLeft;
        public bool active;
        public SpriteEffects effect;

        public Animation(SpriteSheets spriteSheet, int framesCountX, Vector2 startPos, Vector2 frameSize, float frameTime, SpriteEffects neweffect)
        {
            this.spriteSheet = spriteSheet;
            this.frames = framesCountX;
            this.frameTime = frameTime;
            this.frameTimeLeft = frameTime;
            currentFrame = 0;
            active = true;
            this.effect = neweffect;

            for (int i = 0; i < frames; i++)
            {
                sourceRectangles.Add(new Rectangle(i * (int)frameSize.X + (int)startPos.X, (int)startPos.Y, (int)frameSize.X, (int)frameSize.Y));
            }
        }

        public void Start()
        {
            active = true;
        }

        public void Stop()
        {
            active = false;
        }

        public void Reset()
        {
            currentFrame = 0;
            frameTimeLeft = frameTime;
        }

        public void Update()
        {
            if (!active) return;

            //possible issue
            frameTimeLeft -= (float)Graphics.gameTime.ElapsedGameTime.TotalSeconds;

            if (frameTimeLeft <= 0)
            {
                frameTimeLeft += frameTime;
                currentFrame = (currentFrame + 1) % frames;
            }
        }


        public void Draw(Vector2 position, Color color, float angle, Vector2 origin, Vector2 scale, float layerDepth)
        {
            Graphics.sprites.Draw(
                ResourceLoader.spriteSheets[spriteSheet].texture,
                position,
                GetCurrentFrame(),
                color,
                angle,
                origin,
                scale,
                effect | SpriteEffects.FlipVertically,
                layerDepth);
        }

        public void Draw(Vector2 position, Color color, float angle, Vector2 origin, Vector2 scale, SpriteEffects newEffect, float layerDepth)
        {
            Graphics.sprites.Draw(
                ResourceLoader.spriteSheets[spriteSheet].texture,
                position,
                GetCurrentFrame(),
                color,
                angle,
                origin,
                scale,
                newEffect,
                layerDepth);
        }

        public Rectangle GetCurrentFrame()
        {
            return sourceRectangles[currentFrame];
        }
    }
}
