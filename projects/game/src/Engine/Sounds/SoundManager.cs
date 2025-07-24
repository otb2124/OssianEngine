using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sounds
{
    public class SoundManager
    {


        public SoundManager() 
        {
           
        }

        public void PlaySound(Resources.Sounds key, float volume = 1f, float pitch = 0f, float pan = 0f)
        {
            if (ResourceLoader.soundResources.TryGetValue(key, out SoundResource soundEffect))
            {
                soundEffect.Effect.Play(volume, pitch, pan);
            }
        }
    }
}
