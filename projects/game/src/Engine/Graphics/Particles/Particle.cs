using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Graphics
{
    public class Particle
    {


        public enum Particles
        {
            NONE,
            HUMAN_BLOOD_DROP,
            SLIME_BLOOD_DROP,
            FIRE_PART,
        }

        public Vector2 Postion;
        public Vector2 VelocityMultiplier;

        public StaticSpriteFactory.SpriteData sprite;
        public AnimationSet aManager;
        public Particles Type;

        public float DurationSec;
        public int DurationCounter;

        public Vector2 AdjustedScale;

        public Particle(Vector2 pos, Particles particle)
        {
            Type = particle;
            Postion = pos;
            AdjustedScale = Vector2.One;

            DurationCounter = 0;

            Init();
        }


        public void Init()
        {
            switch(Type)
            {
                case Particles.HUMAN_BLOOD_DROP:
                    sprite = StaticSpriteFactory.GetEntityParticle(new Vector2(0, 0));
                    DurationSec = 0.5f;
                    VelocityMultiplier = new Vector2(0.25f, 1);
                    AdjustedScale = new Vector2(0.25f, 0.25f);
                    break;
                case Particles.SLIME_BLOOD_DROP:
                    sprite = StaticSpriteFactory.GetEntityParticle(new Vector2(1, 0));
                    DurationSec = 0.5f;
                    VelocityMultiplier = new Vector2(0.25f, 1);
                    AdjustedScale = new Vector2(0.25f, 0.25f);
                    break;
                case Particles.FIRE_PART:
                    sprite = StaticSpriteFactory.GetEntityParticle(new Vector2(0, 1));
                    DurationSec = 0.5f;
                    VelocityMultiplier = new Vector2(0.25f, 1);
                    AdjustedScale = new Vector2(0.25f, 0.25f);
                    break;
            }

            aManager = new AnimationSet(sprite);
        }

        public void Draw()
        {
            Rectangle srcRect = aManager.GetCurrent().GetCurrentFrame();

            Vector2 adjustedPos = Postion;
            Vector2 adjustedOrigin = (srcRect.Size.ToVector2() / 2);
            Vector2 adjustedScale = AdjustedScale;

            Graphics.sprites.Draw(
                 ResourceLoader.spriteSheets[aManager.SpriteSheet].texture,
                 adjustedPos,
                 aManager.GetCurrent().GetCurrentFrame(),
                 Color.White,
                 0f,
                 adjustedOrigin,
                 adjustedScale,
                 SpriteEffects.FlipVertically, 0f);
        }
    }
}
