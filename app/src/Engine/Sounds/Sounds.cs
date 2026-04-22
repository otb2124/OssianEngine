using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sounds
{
    public static class Sounds
    {

        public static SoundManager SoundManager;
        public static float GlobalSoundVolume = 0.1f;

        public static void Init()
        {
            SoundManager = new SoundManager();
        }

        public static void Update()
        {
            SoundManager.Update();
        }
    }
}
