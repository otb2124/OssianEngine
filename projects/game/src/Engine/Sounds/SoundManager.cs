using Entities;
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

        public int nextId;

        public List<SoundSource> sounds;
        public List<SoundSource> soundsToRemove;


        public SoundManager() 
        {
           sounds = new List<SoundSource>();
           soundsToRemove = new List<SoundSource>();
        }

        public void Update()
        {
            soundsToRemove.Clear();

            foreach (var sound in sounds)
            {
                if (sound.IsLooping || sound.DurationSec == 0)
                {
                    sound.Update();
                }
                else
                {
                    if (sound.DurationCounter < sound.DurationSec * Graphics.Graphics.UpdatesPerSecond)
                    {
                        sound.DurationCounter++;
                        sound.Update();
                    }
                    else
                    {
                        sound.Stop();
                        soundsToRemove.Add(sound);
                    }
                }
            }

            foreach (var sound in soundsToRemove)
            {
                sounds.Remove(sound);
            }
        }

        public void AddSoundSource(SoundSource source)
        {
            if (sounds.Any(s => s.Id == source.Id))
            {
                return;
            }

            sounds.Add(source);
        }

            public void PlaySound(Resources.Sounds key, float volume = 1f, float pitch = 0f, float pan = 0f)
        {
            if (ResourceLoader.soundResources.TryGetValue(key, out SoundResource soundEffect))
            {
                soundEffect.Effect.Play(volume, pitch, pan);
            }
        }


        public int GenerateId()
        {
            if (sounds != null)
            {
                return nextId++;
            }

            var entities = sounds;
            while (entities.Any(e => e.Id == nextId))
            {
                nextId++;
                if (nextId < 0)
                {
                    nextId = 1;
                }
            }
            return nextId++;
        }

        public SoundSource GetSoundSourceById(int id)
        {
            return sounds.FirstOrDefault(e => e.Id == id);
        }
    }
}
