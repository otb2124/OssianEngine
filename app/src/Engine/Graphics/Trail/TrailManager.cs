using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;

namespace Graphics
{
    public class TrailSnapshot
    {
        public Vector2 Position;
        public float Rotation;
        public float Age;        // seconds since spawned
        public float Lifetime;
        public float Alpha => 1f - (Age / Lifetime);
        public bool IsDead => Age >= Lifetime;

        // Capture what the entity looked like at this moment
        public SpriteSheets SpriteSheet;
        public Rectangle SourceRect;
        public Vector2 Origin;
        public Vector2 Scale;
        public SpriteEffects Effect;

        public TrailSnapshot(
            Vector2 position, float rotation,
            SpriteSheets sheet, Rectangle srcRect,
            Vector2 origin, Vector2 scale, SpriteEffects effect,
            float lifetime)
        {
            Position = position;
            Rotation = rotation;
            SpriteSheet = sheet;
            SourceRect = srcRect;
            Origin = origin;
            Scale = scale;
            Effect = effect;
            Lifetime = lifetime;
            Age = 0f;
        }
    }

    public class TrailRenderer
    {
        private readonly List<TrailSnapshot> _snapshots = new();

        // ── Config ────────────────────────────────────────────────────────
        public bool Enabled = true;
        public float SnapshotInterval = 0.04f;   // seconds between snapshots
        public float SnapshotLifetime = 0.3f;    // how long each ghost lasts
        public Color TintColor = Color.CornflowerBlue;
        public float TintStrength = 0.6f;    // 0 = white ghost, 1 = full tint

        private float _timeSinceLastSnapshot = 0f;

        public void Update(float deltaTime)
        {
            _timeSinceLastSnapshot += deltaTime;

            // Age all snapshots
            for (int i = _snapshots.Count - 1; i >= 0; i--)
            {
                _snapshots[i].Age += deltaTime;
                if (_snapshots[i].IsDead)
                    _snapshots.Remove(_snapshots[i]);
            }
        }

        public bool ShouldSnapshot()
        {
            if (!Enabled) return false;
            if (_timeSinceLastSnapshot < SnapshotInterval) return false;
            _timeSinceLastSnapshot = 0f;
            return true;
        }

        public void AddSnapshot(TrailSnapshot snapshot)
        {
            _snapshots.Add(snapshot);
        }

        public void Draw()
        {
            // Draw oldest first so newest ghost is on top
            for (int i = 0; i < _snapshots.Count; i++)
            {
                var s = _snapshots[i];

                // Lerp toward tint color as it fades
                Color drawColor = Color.Lerp(Color.White, TintColor, TintStrength);
                drawColor.A = (byte)(s.Alpha * 180f);   // never fully opaque

                Graphics.Sprites.Draw(
                    ResourceLoader.SpriteSheetResources[s.SpriteSheet].Texture,
                    s.SourceRect,
                    s.Origin,
                    s.Position,
                    s.Rotation,
                    s.Scale,
                    drawColor,
                    s.Effect
                );
            }
        }

        public void Clear() => _snapshots.Clear();
    }
}