using Graphics;
using Microsoft.Xna.Framework;
using Physics;
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
        public int MapsCount;

        public EntityMapManager()
        {
            MapsCount = 2;
        }

        public void Init()
        {
            maps = new EntityMap[MapsCount];

            for (int i = 0; i < MapsCount; i++)
            {
                maps[i] = new EntityMap(i);
                maps[i].Entities = EntityMapSetter.FillEntityMap(i);
                maps[i].Events = EventMapSetter.FillEventMap(i);
                maps[i].FilterLayers = FilterLayerMapSetter.FillFilterLayerMap(i);
            }
        }

        public void LoadMap(int nextId, Vector2 playerPos)
        {
            if(Entities.Player == null)
            {
                Entities.Player = new Player();
            }
            else
            {
                Entities.entityManager.RemoveEntity(Entities.Player);
            }

            CurrentMapId = nextId;
            Entities.Player.Model.Body.MoveTo(FlatConverter.ToFlatVector(playerPos));
            maps[nextId].Entities.Add(Entities.Player);

            Physics.Physics.flatWorld.RefreshList(maps[nextId].Entities);

            Graphics.Graphics.backgroundManager.RemoveAll();
            Graphics.Graphics.backgroundManager.Init();

            Graphics.Graphics.lightManager.Init();
            Graphics.Graphics.filterManager.Init();
        }

        public void LoadInitialMap()
        {
            LoadMap(0, new Vector2(0, 1000));
        }


        public EntityMap GetCurrentMap()
        {
            return maps[CurrentMapId];
        }

    }
}
