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

        public static void Init()
        {
            SoundManager = new SoundManager();
        }
    }
}
