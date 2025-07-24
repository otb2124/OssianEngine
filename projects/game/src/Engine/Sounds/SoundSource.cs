using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sounds
{
    public class SoundSource
    {
        
        public Vector2 Position;
        public Resources.Sounds Key;
        public float MaxDistance;
        public float Volume;
        public bool IsLooping;

        public SoundSource(Resources.Sounds soundKey, Vector2 position, float maxDistance, float volume, bool isLooping = true)
        {
            Key = soundKey;
            Position = position;
            MaxDistance = maxDistance;
            Volume = volume;
            IsLooping = isLooping;
        }
    }
}
