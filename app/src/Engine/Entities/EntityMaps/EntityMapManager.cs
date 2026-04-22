using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EntityMapManager
    {

        public EntityMap[] maps;
        public int CurrentMapId;
        public int CurrentMapLayerId = 0; //TODO: RE HARDCODE

        public int MapsCount;
        public GlobalMapTime GlobalMapTime;

        public EntityMapManager()
        {
            MapsCount = 2;
            GlobalMapTime = new GlobalMapTime();
        }

        public void Init()
        {
            maps = new EntityMap[MapsCount];

            for (int i = 0; i < MapsCount; i++)
            {
                maps[i] = new EntityMap(i);

                for (global::System.Int32 j = 0; j < maps[i].Layers.Count; j++)
                {
                    maps[i].Layers[j].Entities = EntityMapSetter.FillEntityMapLayer(i, j);
                    maps[i].Layers[j].Events = EventMapSetter.FillEventMap(i, j);
                    maps[i].Layers[j].FilterLayers = FilterLayerMapSetter.FillFilterLayerMap(i, j);
                }
            }
        }

        public void Update()
        {
            GlobalMapTime.Update();
        }

        public void LoadLayer(int nextMapId, int nextLayerId, Vector2 playerPos)
        {
            ResourceLoader.MapLoaded = false;

            if (Entities.Player == null)
            {
                Entities.Player = new Player();
            }
            else
            {
                Entities.EntityManager.RemoveEntity(Entities.Player);
            }

            CurrentMapId = nextMapId;
            Entities.Player.Model.Body.MoveTo(PhysicalConverter.ToPhysicalVector(playerPos));
            maps[nextMapId].Layers[nextLayerId].Entities.Add(Entities.Player);

            Physics.Physics.flatWorld.RefreshList(maps[nextMapId].Layers[nextLayerId].Entities);

            Graphics.Graphics.BackgroundManager.RemoveAll();
            Graphics.Graphics.BackgroundManager.Init();

            Graphics.Graphics.LightManager.Init();
            Graphics.Graphics.FilterManager.UpdateLayers();

            ResourceLoader.MapLoaded = true;
        }

        public void LoadPreviousLayer()
        {
            CurrentMapLayerId--;
            ReloadLayer();
        }

        public void LoadNextLayer()
        {
            CurrentMapLayerId++;
            ReloadLayer();
        }

        public void ReloadLayer()
        {
            LoadLayer(CurrentMapId, CurrentMapLayerId, Entities.Player.Model.Body.Position.ToVector2());
        }

        public void LoadInitialMap()
        {
            LoadLayer(0, 0, new Vector2(0, 1000));
        }


        public EntityMap GetCurrentMap()
        {
            return maps[CurrentMapId];
        }

        public EntityMapLayer GetCurrentMapLayer()
        {
            return GetCurrentMap().GetLayer(CurrentMapLayerId);
        }
    }
}
