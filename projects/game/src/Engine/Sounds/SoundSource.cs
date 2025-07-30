using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Physics;
using Resources;
using SharpDX.Direct3D9;
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
        public int Id;
        
        public Vector2 Position;
        public Resources.Sounds Key;
        public float MaxDistance;
        public float Volume;
        public bool IsLooping;

        public float DurationSec;
        public int DurationCounter = 0;

        public bool WasPlayed = false;

        public SoundEffectInstance Instance;

        public SoundSource(Resources.Sounds soundKey, Vector2 position, float durationSec = 0, float maxDistance = 350f, float volume = 1f, bool isLooping = false)
        {
            Key = soundKey;
            Position = position;
            DurationSec = durationSec;
            MaxDistance = maxDistance;
            Volume = volume;
            IsLooping = isLooping;
            Id = Sounds.SoundManager.GenerateId();

            if (ResourceLoader.soundResources.TryGetValue(Key, out var soundResource))
            {
                Instance = soundResource.Effect.CreateInstance();
                Instance.IsLooped = IsLooping;
            }
        }

        public SoundSource(int id, Resources.Sounds soundKey, Vector2 position, float durationSec = 0, float maxDistance = 350f, float volume = 1f, bool isLooping = false)
        {
            Key = soundKey;
            Position = position;
            DurationSec = durationSec;
            MaxDistance = maxDistance;
            Volume = volume;
            IsLooping = isLooping;
            Id = id;

            if (ResourceLoader.soundResources.TryGetValue(Key, out var soundResource))
            {
                Instance = soundResource.Effect.CreateInstance();
                Instance.IsLooped = IsLooping;
            }
        }

        public void Play()
        {
            Instance.Play();
            WasPlayed = true;
        }

        public void Update()
        {
            if (Instance == null)
            {
                return;
            }

            Vector2 cameraWorldPosition = Entities.Entities.player.Model.body.Position.ToVector2();

            float distance = Vector2.Distance(cameraWorldPosition, Position);
            float volume = MathHelper.Clamp(1f - (distance / MaxDistance), 0f, 1f) * Volume;
            Instance.Volume = volume;

            float xDifference = Position.X - cameraWorldPosition.X;
            float pan = MathHelper.Clamp(xDifference / MaxDistance, -1f, 1f);
            Instance.Pan = pan;

            if (!WasPlayed)
            {
                Play();
            }
        }

        public void Stop()
        {
            if (Instance != null)
            {
                Instance.Stop();
                Instance.Dispose();
                Instance = null;
            }
        }
    }
}
