using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using SharpDX.Direct3D9;
using SharpDX.Win32;
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
        public int nextId;

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

        public int GenerateId()
        {
            if (Entities.Entities.entityMapManager == null || Entities.Entities.entityMapManager.maps == null || Entities.Entities.entityMapManager.CurrentMapId < 0 || Entities.Entities.entityMapManager.CurrentMapId >= Entities.Entities.entityMapManager.maps.Length)
            {
                return nextId++;
            }
            var entities = lightSources;
            while (entities.Any(e => e.Id == nextId))
            {
                nextId++;
                if (nextId < 0)
                {
                    nextId = 1;
                }
            }
            return nextId++;
        }

        public LightSource GetEntityById(int id)
        {
            return lightSources.FirstOrDefault(e => e.Id == id);
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
