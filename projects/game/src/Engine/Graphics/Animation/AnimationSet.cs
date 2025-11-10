using Entities;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using Utils;
using Microsoft.Xna.Framework;
using System.Reflection;
using System.Linq;

namespace Graphics
{
    public class AnimationSet
    {
        public List<Animation> Anims;
        public SpriteSheets SpriteSheet;

        public AnimationKey LastKey;

        public AnimationSet(SpriteSheets spriteSheet, List<Animation> anims) 
        {
            SpriteSheet = spriteSheet;

            Anims = new List<Animation>();
            foreach (Animation anim in anims)
            {
                AddAnimation(anim);
            }
        }

        //for static ones
        public AnimationSet(StaticSprite spriteData)
        {
            SpriteSheet = spriteData.SpriteSheet;
            Anims = new List<Animation>();
            AddAnimation(
                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT), new AnimationFramesData(1, spriteData.SrcRect.Location.ToVector2(), spriteData.SrcRect.Size.ToVector2(), 0f, spriteData.Effect))
                );
            AddAnimation(
                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT), new AnimationFramesData(1, spriteData.SrcRect.Location.ToVector2(), spriteData.SrcRect.Size.ToVector2(), 0f, spriteData.Effect))
                );
        }

        public void AddAnimation(Animation animation)
        {
            Anims.Add(animation);
            LastKey ??= animation.AnimationKey;
        }

        public void Update(AnimationKey key)
        {

            if (ContainsAnimationKey(key))
            {
                GetAnimation(key).Start();
                GetAnimation(key).Update();
                LastKey = key;
            }
            else
            {
                if(GetAnimation(LastKey) != null)
                {
                    GetAnimation(LastKey).Stop();
                    GetAnimation(LastKey).Reset();
                }
                
            }
        }

        public void Update()
        {
            Update(AnimationKey.IdleKey);
        }

        public Animation GetCurrent()
        {
            return GetAnimation(LastKey);
        }

        public Animation GetAnimation(AnimationKey animationKey)
        {
            return Anims.FirstOrDefault(animation => animation.AnimationKey.AnimationState == animationKey.AnimationState && animation.AnimationKey.Direction == animationKey.Direction);
        }

        public bool ContainsAnimationKey(AnimationKey key)
        {
            return Anims.Any(animation => animation.AnimationKey.AnimationState == key.AnimationState && animation.AnimationKey.Direction == key.Direction);
        }


        public void DrawCurrent(Vector2 pos, Color color, float angle, Vector2 origin, Vector2 scale, float layerDepth, bool revertVerticalDraw = false)
        {
            GetCurrent().Draw(SpriteSheet, pos, color, angle, origin, scale, layerDepth, revertVerticalDraw);
        }

        public void DrawCurrent(Vector2 pos, Color color, float angle, Vector2 origin, Vector2 scale, float layerDepth, SpriteEffects effect)
        {
            GetCurrent().Draw(SpriteSheet, pos, color, angle, origin, scale, layerDepth, effect);
        }


        public bool IsCurrentAnimationLastFrame()
        {
            var currentAnim = GetCurrent();
            return currentAnim != null && currentAnim.IsLastFrame();
        }
    }
}
