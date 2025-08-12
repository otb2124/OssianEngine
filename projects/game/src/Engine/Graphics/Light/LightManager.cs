using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class LightManager
    {
        public List<LightSource> lightSources;
        public List<LightSource> lightSourcesToRemove;

        public LightManager()
        {
            lightSources = new List<LightSource>();
            lightSourcesToRemove = new List<LightSource>();
        }

        public void Init()
        {
            ClearLightSources();

            foreach (Entity ent in Entities.Entities.entityMapManager.maps[Entities.Entities.entityMapManager.CurrentMapId].Entities)
            {
                if (ent is PhysicalEntity phent && phent.Emission != null)
                {
                    AddLightSource(new EntityEmissionLightSource(ent.Id, phent.Emission));
                }
            }
        }

        public void AddLightSource(LightSource light)
        {
            lightSources.Add(light);
        }

        public void ClearLightSources()
        {
            lightSources.Clear();
        }

        public void Update()
        {
            foreach (var light in lightSources)
            {
                if (light != null)
                {
                    if (light is EntityEmissionLightSource eesource)
                    {
                        if (Entities.Entities.entityManager.GetEntityById(eesource.EntityId) == null)
                        {
                            lightSourcesToRemove.Add(light);
                            continue;
                        }
                    }
                    light.Update();
                }
            }

            foreach (var light in lightSourcesToRemove)
            {
                lightSources.Remove(light);
            }
            lightSourcesToRemove.Clear();
        }


        public void Draw()
        {
            foreach (var light in lightSources)
            {
                if (light != null)
                {
                    light.Draw();
                }
            }
        }
    }
}
