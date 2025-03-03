using Entities;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using static Graphics.Animation;
using Utils;
using Microsoft.Xna.Framework;

namespace Graphics
{
    public class AnimationManager
    {
        public Dictionary<Tuple<Directions, AnimationStates>, Animation> anims = new Dictionary<Tuple<Directions, AnimationStates>, Animation>();
        public Tuple<Directions, AnimationStates> lastKey;



        public void AddStaticAnimation(StaticSprites sprite)
        {
            float frameSpeed = 0.0f;
            StaticSpriteFactory.SpriteData data = StaticSpriteFactory.spriteMappings[sprite];
            AddAnimation(sprite, Directions.LEFT, AnimationStates.IDLE, 1, data.srcRect.Location.ToVector2(), data.srcRect.Size.ToVector2(), frameSpeed, SpriteEffects.None);
        }
        public void AddAnimation(StaticSprites sprite, Directions Directions, AnimationStates animationState, int framesCount, Vector2 startPos, Vector2 frameSize, float eachFrameDuration, SpriteEffects effect)
        {
            StaticSpriteFactory.SpriteData data = StaticSpriteFactory.spriteMappings[sprite];
            AddAnimation(new Tuple<Directions, AnimationStates>(Directions, animationState), new Animation(data.sheet, framesCount, startPos, frameSize, eachFrameDuration, effect));
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
