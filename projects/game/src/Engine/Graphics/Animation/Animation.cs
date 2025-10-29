using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System.Collections.Generic;
using Utils;

namespace Graphics
{


    public class AnimationKey
    {
        public AnimationStates AnimationState;
        public Directions Direction;

        public AnimationKey(AnimationStates state, Directions direction)
        {
            AnimationState = state; Direction = direction; 
        }
    }


    public class AnimationFramesData
    {
        public int FramesCountX;
        public Vector2 StartPos;
        public Vector2 FrameSize;
        public float FrameTime;
        public SpriteEffects Effect;

        public Vector2 EachFramePositionOffset;
        public Vector2 EachFrameSizeOffset;

        public AnimationFramesData(int framesCountX, Vector2 startPos, Vector2 frameSize, float frameTime, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            FramesCountX = framesCountX;
            StartPos = startPos;
            FrameSize = frameSize;
            FrameTime = frameTime;
            EachFramePositionOffset = Vector2.Zero;
            EachFrameSizeOffset = Vector2.Zero;
            Effect = spriteEffect;  
        }

        public AnimationFramesData(int framesCountX, Vector2 startPos, Vector2 eachframePosOffset, Vector2 frameSize, Vector2 eachframeSizeOffset, float frameTime, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            FramesCountX = framesCountX;
            StartPos = startPos;
            FrameSize = frameSize;
            FrameTime = frameTime;
            EachFramePositionOffset = eachframePosOffset;
            EachFrameSizeOffset = eachframeSizeOffset;
            Effect = spriteEffect;
        }
    }


    public class Animation
    {
        public AnimationFramesData AnimationFramesData;

        public int CurrentFrame;
        public float FrameTimeLeft;
        public bool Active;
        public List<Rectangle> SourceRectangles = new List<Rectangle>();

        public AnimationKey AnimationKey;

        public Animation(AnimationKey animationKey, AnimationFramesData animationFramesData)
        {
            AnimationFramesData = animationFramesData;

            FrameTimeLeft = AnimationFramesData.FrameTime;
            CurrentFrame = 0;
            Active = true;

            for (int i = 0; i < AnimationFramesData.FramesCountX; i++)
            {
                SourceRectangles.Add(new Rectangle(i * (int)AnimationFramesData.FrameSize.X + (int)AnimationFramesData.StartPos.X, (int)AnimationFramesData.StartPos.Y, (int)AnimationFramesData.FrameSize.X, (int)AnimationFramesData.FrameSize.Y));
            }

            AnimationKey = animationKey;
        }


        public void Start()
        {
            Active = true;
        }

        public void Stop()
        {
            Active = false;
        }

        public void Reset()
        {
            CurrentFrame = 0;
            FrameTimeLeft = AnimationFramesData.FrameTime;
        }

        public void Update()
        {
            if (!Active) return;

            FrameTimeLeft -= (float)Graphics.CurrentLogicTime/(float)Graphics.TimeScale;

            if (FrameTimeLeft <= 0)
            {
                FrameTimeLeft += AnimationFramesData.FrameTime;
                CurrentFrame = (CurrentFrame + 1) % AnimationFramesData.FramesCountX;
            }
        }

        public void Draw(SpriteSheets spriteSheet, Vector2 position, Color color, float angle, Vector2 origin, Vector2 scale, float layerDepth, bool revertVerticalDraw = false)
        {
            if(revertVerticalDraw)
            {
                Graphics.sprites.Draw(
                ResourceLoader.spriteSheets[spriteSheet].Texture,
                position,
                GetCurrentFrame(),
                color,
                angle,
                origin,
                scale,
                AnimationFramesData.Effect,
                layerDepth);
            }
            else
            {
                Graphics.sprites.Draw(
                ResourceLoader.spriteSheets[spriteSheet].Texture,
                position,
                GetCurrentFrame(),
                color,
                angle,
                origin,
                scale,
                AnimationFramesData.Effect | SpriteEffects.FlipVertically,
                layerDepth);
            }
        }

        public void Draw(SpriteSheets spriteSheet, Vector2 position, Color color, float angle, Vector2 origin, Vector2 scale, float layerDepth, SpriteEffects newEffect)
        {
            Graphics.sprites.Draw(
                ResourceLoader.spriteSheets[spriteSheet].Texture,
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
            return SourceRectangles[CurrentFrame];
        }
    }
}
