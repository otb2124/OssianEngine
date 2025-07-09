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
                maps[i].Entities = EntitySetter.FillEntityMap(i);
                maps[i].Events = EventSetter.FillEventMap(i);
            }
        }

        public void ChangeMap(int nextId, Vector2 playerPos)
        {
            if(Entities.player == null)
            {
                Entities.player = new Player();
            }
            else
            {
                Entities.entityManager.RemoveEntity(Entities.player);
            }

            CurrentMapId = nextId;
            Entities.player.Model.body.MoveTo(FlatConverter.ToFlatVector(playerPos));
            Entities.entityMapManager.maps[nextId].Entities.Add(Entities.player);

            Physics.Physics.flatWorld.RefreshList(Entities.entityMapManager.maps[nextId].Entities);

            Graphics.Graphics.backgroundManager.RemoveAll();
            Graphics.Graphics.backgroundManager.Init();
        }

    }
}
