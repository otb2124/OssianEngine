using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Graphics.Particle;

namespace Graphics
{
    public class ParticleSet
    {

        public enum ParticleSets
        {
            NONE,
            HUMAN_BLOOD_SPLASH,
            SLIME_BLOOD_SPLASH,
        }


        public List<Particle> Particles;
        public List<Particle> ParticlesToRemove;

        public ParticleSets Type;

        public Vector2 Position;
        public Vector2 Velocity;


        public ParticleSet(ParticleSets set) 
        {
            Particles = new List<Particle>();
            ParticlesToRemove = new List<Particle>();
            Type = set;

            Init();
        }

        public ParticleSet(ParticleSets set, Vector2 pos, Vector2 vel)
        {
            Particles = new List<Particle>();
            ParticlesToRemove = new List<Particle>();
            Type = set;

            Position = pos;
            Velocity = vel;

            Init();
        }

        public void Init()
        {
            switch(Type)
            {
                case ParticleSets.HUMAN_BLOOD_SPLASH:
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.HUMAN_BLOOD_DROP));
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.HUMAN_BLOOD_DROP));
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.HUMAN_BLOOD_DROP));
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.HUMAN_BLOOD_DROP));
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.HUMAN_BLOOD_DROP));
                    break;
                case ParticleSets.SLIME_BLOOD_SPLASH:
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.SLIME_BLOOD_DROP));
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.SLIME_BLOOD_DROP));
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.SLIME_BLOOD_DROP));
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.SLIME_BLOOD_DROP));
                    Particles.Add(new Particle(new Vector2(Position.X, Position.Y), Particle.Particles.SLIME_BLOOD_DROP));
                    break;
            }
        }


        public void HandleTypeUpdate()
        {
            switch (Type)
            {
                case ParticleSets.HUMAN_BLOOD_SPLASH:
                    Particles[0].Postion += new Vector2(Velocity.X, Velocity.Y + 0.1f)  * Particles[0].VelocityMultiplier;
                    Particles[1].Postion += new Vector2(Velocity.X, Velocity.Y + 0.05f) * Particles[1].VelocityMultiplier;
                    Particles[2].Postion += new Vector2(Velocity.X, Velocity.Y)         * Particles[2].VelocityMultiplier;
                    Particles[3].Postion += new Vector2(Velocity.X, Velocity.Y - 0.05f) * Particles[3].VelocityMultiplier;
                    Particles[4].Postion += new Vector2(Velocity.X, Velocity.Y - 0.1f)  * Particles[4].VelocityMultiplier;
                    break;
                case ParticleSets.SLIME_BLOOD_SPLASH:
                    Particles[0].Postion += new Vector2(Velocity.X, Velocity.Y + 0.1f)  * Particles[0].VelocityMultiplier;
                    Particles[1].Postion += new Vector2(Velocity.X, Velocity.Y + 0.05f) * Particles[1].VelocityMultiplier;
                    Particles[2].Postion += new Vector2(Velocity.X, Velocity.Y)         * Particles[2].VelocityMultiplier;
                    Particles[3].Postion += new Vector2(Velocity.X, Velocity.Y - 0.05f) * Particles[3].VelocityMultiplier;
                    Particles[4].Postion += new Vector2(Velocity.X, Velocity.Y - 0.1f)  * Particles[4].VelocityMultiplier;
                    break;
            }
        }

        public void Update()
        {
            foreach (Particle particle in Particles)
            {
                HandleTypeUpdate();
                HandleParticleDuration(particle);
            }


            foreach (Particle particle in ParticlesToRemove)
            {
                Particles.Remove(particle);
            }
        }


        


        public void HandleParticleDuration(Particle particle)
        {
            if (particle.DurationCounter < particle.DurationSec * 60f)
            {
                particle.DurationCounter++;
            }
            else
            {
                ParticlesToRemove.Add(particle);
            }
        }

        public void Draw()
        {
            foreach (Particle particle in Particles)
            {
                particle.Draw();
            }
        }
    }
}
