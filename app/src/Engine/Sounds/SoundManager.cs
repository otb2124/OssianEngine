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

        public int NextId;

        public List<SoundSource> Sounds;
        public List<SoundSource> SoundsToRemove;


        public SoundManager() 
        {
           Sounds = new List<SoundSource>();
           SoundsToRemove = new List<SoundSource>();
        }

        public void Update()
        {
            SoundsToRemove.Clear();

            foreach (var sound in Sounds)
            {
                if (sound.IsLooping || sound.DurationSec == 0)
                {
                    sound.Update();
                }
                else
                {
                    if (sound.DurationCounter < sound.DurationSec * Graphics.Graphics.GraphicsFrameRate)
                    {
                        sound.DurationCounter++;
                        sound.Update();
                    }
                    else
                    {
                        sound.Stop();
                        SoundsToRemove.Add(sound);
                    }
                }
            }

            foreach (var sound in SoundsToRemove)
            {
                Sounds.Remove(sound);
            }
        }

        public void AddSoundSource(SoundSource source)
        {
            if (Sounds.Any(s => s.Id == source.Id))
            {
                return;
            }

            Sounds.Add(source);
        }

            public void PlaySound(Resources.Sounds key, float volume = 1f, float pitch = 0f, float pan = 0f)
        {
            if (ResourceLoader.SoundResources.TryGetValue(key, out SoundResource soundEffect))
            {
                soundEffect.Effect.Play(volume, pitch, pan);
            }
        }


        public int GenerateId()
        {
            if (Sounds != null)
            {
                return NextId++;
            }

            var entities = Sounds;
            while (entities.Any(e => e.Id == NextId))
            {
                NextId++;
                if (NextId < 0)
                {
                    NextId = 1;
                }
            }
            return NextId++;
        }

        public SoundSource GetSoundSourceById(int id)
        {
            return Sounds.FirstOrDefault(e => e.Id == id);
        }
    }
}
