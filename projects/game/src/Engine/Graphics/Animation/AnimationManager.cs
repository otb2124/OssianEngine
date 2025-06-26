using Entities;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using Utils;
using Microsoft.Xna.Framework;

namespace Graphics
{
    public class AnimationManager
    {
        public Dictionary<Tuple<Directions, AnimationStates>, Animation> anims = new Dictionary<Tuple<Directions, AnimationStates>, Animation>();
        public Tuple<Directions, AnimationStates> lastKey;

        public void AddStaticAnimation(StaticSpriteFactory.SpriteData spriteData)
        {
            AddAnimation(new Tuple<Directions, AnimationStates>(Directions.LEFT, AnimationStates.IDLE), new Animation(spriteData.sheet, 1, spriteData.srcRect.Location.ToVector2(), spriteData.srcRect.Size.ToVector2(), 0f, spriteData.effect));
        }

        public void AddAnimation(StaticSpriteFactory.SpriteData spriteData, Directions Directions, AnimationStates animationState, int framesCount, Vector2 startPos, Vector2 frameSize, float eachFrameDuration, SpriteEffects effect)
        {
            AddAnimation(new Tuple<Directions, AnimationStates>(Directions, animationState), new Animation(spriteData.sheet, framesCount, startPos, frameSize, eachFrameDuration, effect));
        }

        public void AddAnimation(Tuple<Directions, AnimationStates> key, Animation animation)
        {
            anims.Add(key, animation);
            lastKey ??= key;
        }

        public void Update(Tuple<Directions, AnimationStates> key)
        {
            if (anims.ContainsKey(lastKey))
            {
                anims[key].Start();
                anims[key].Update();
                lastKey = key;
            }
            else
            {
                anims[lastKey].Stop();
                anims[lastKey].Reset();
            }
        }

        public Animation GetCurrent()
        {
            return anims[lastKey];
        }
    }
}
