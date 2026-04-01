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

            foreach (WorldEntity ent in Entities.Entities.EntityMapManager.GetCurrentMapLayer().Entities)
            {
                if (ent is PhysicalEntity phent && phent.Emission != null)
                {
                    AddLightSource(new EntityEmissionLightSource(ent.Id, phent.Emission));
                }
            }
        }


        public void AddEntityEmissionLightSource(PhysicalEntity ent)
        {
            AddLightSource(new EntityEmissionLightSource(ent.Id, ent.Emission));
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
                        if (Entities.Entities.EntityManager.GetEntityById(eesource.EntityId) == null)
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
            if (Entities.Entities.EntityMapManager == null || Entities.Entities.EntityMapManager.maps == null || Entities.Entities.EntityMapManager.CurrentMapId < 0 || Entities.Entities.EntityMapManager.CurrentMapId >= Entities.Entities.EntityMapManager.maps.Length)
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

        public LightSource GetLightById(int id)
        {
            return lightSources.FirstOrDefault(e => e.Id == id);
        }

        public List<LightSource> GetNearbyLights(Vector2 worldPosition, float maxDistance = 1000f)
        {
            var nearby = new List<LightSource>();

            foreach (var light in lightSources)
            {
                if (light == null) continue;

                float distance = Vector2.Distance(worldPosition, light.Position);

                if (distance <= maxDistance)
                {
                    nearby.Add(light);
                }
            }

            // Optional: Sort by distance (closest first)
            nearby.Sort((a, b) =>
                Vector2.DistanceSquared(worldPosition, a.Position)
                       .CompareTo(Vector2.DistanceSquared(worldPosition, b.Position)));

            return nearby;
        }

        public List<LightSource> GetNearbyLights(Vector2 worldPosition, float maxDistance, int maxCount)
        {
            var nearby = GetNearbyLights(worldPosition, maxDistance);

            if (nearby.Count > maxCount)
                nearby.RemoveRange(maxCount, nearby.Count - maxCount);

            return nearby;
        }


        public void Draw()
        {
            DrawInScreenSpace();
        }

        public void DrawInScreenSpace()
        {
            foreach (var light in lightSources)
            {
                if (light == null || light.Data == null || light.texture == null)
                    continue;

                light.Draw();
            }
        }


    }
}
