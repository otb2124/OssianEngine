using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Graphics.ParticleSet;

namespace Graphics
{

    public class ParticleManager
    {


        public List<ParticleSet> ParticleSets;
        public List<ParticleSet> ParticleSetsToRemove;


        public ParticleManager() 
        {
            ParticleSets = new List<ParticleSet>
            {
                new ParticleSet(ParticleSet.ParticleSets.HUMAN_BLOOD_SPLASH)
            };

            ParticleSetsToRemove = new List<ParticleSet>();
        }

        public void SpawnParticleSet(ParticleSets set)
        {
            ParticleSets.Add(new ParticleSet(set));
        }

        public void Update()
        {
            foreach (ParticleSet particleSet in ParticleSets)
            {
                if(particleSet.Particles.Count > 0)
                {
                    particleSet.Update();
                }
                else
                {
                    ParticleSetsToRemove.Add(particleSet);
                }
            }


            foreach (ParticleSet particleSet in ParticleSetsToRemove)
            {
                ParticleSets.Remove(particleSet);
            }
        }

        public void Draw()
        {
            foreach (ParticleSet particle in ParticleSets)
            {
                particle.Draw();
            }
        }

    }
}
