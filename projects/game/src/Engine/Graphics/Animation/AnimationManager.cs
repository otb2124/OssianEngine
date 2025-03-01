using Entities;
using System;
using System.Collections.Generic;

namespace Graphics
{
    public class AnimationManager
    {
        public Dictionary<Tuple<PhysicalEntity.Directions, Animation.AnimationStates>, Animation> anims = new Dictionary<Tuple<PhysicalEntity.Directions, Animation.AnimationStates>, Animation>();
        public Tuple<PhysicalEntity.Directions, Animation.AnimationStates> lastKey;


        public void AddAnimation(Tuple<PhysicalEntity.Directions, Animation.AnimationStates> key, Animation animation)
        {
            anims.Add(key, animation);
            lastKey ??= key;
        }

        public void Update(Tuple<PhysicalEntity.Directions, Animation.AnimationStates> key)
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
