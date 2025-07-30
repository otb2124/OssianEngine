using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public enum Sounds
    {
        NONE,
        HUMANOID_HURT,
        HUMANOID_FOOTSTEP,
    }

    public enum EntitySounds
    {
        STEP,
        RECEIVEDAMAGE,
        JUMP,
    }

    public class SoundResource
    {

        public SoundEffect Effect;
        public string SoundPath;

        public SoundResource(Sounds key)
        {
            SoundPath = GetSoundPath(key);
            Load();
        }

        public string GetSoundPath(Sounds key)
        {
            switch (key)
            {
                case Sounds.HUMANOID_HURT:
                    return "sfx/humanoid_hurt";
                case Sounds.HUMANOID_FOOTSTEP:
                    return "sfx/humanoid_footstep";
                default:
                    return "sfx/humanoid_hurt";
            }
        }

        public void Load()
        {
            string soundsDirectory = Path.Combine("Content", "res", "sounds");
            string path = Path.Combine("res", "sounds", SoundPath);

            Console.WriteLine(path);

            Effect = Graphics.Graphics.contentManager.Load<SoundEffect>(path);
        }
    }
}
