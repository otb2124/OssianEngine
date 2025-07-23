using Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Utils;
using System.Diagnostics;
using System;
using Physics;

namespace Entities
{
    public class EntityManager
    {

        private int nextId = 1;
        public EntityManager()
        {
            
        }

        public void Init()
        {
            //Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities = EntitySetter.FillEntityMap(Entities.entityMapManager.CurrentMapId);
        }

        public void Update()
        {
            var entitiesSnapshot = Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.ToList();

            foreach (var entFrom in entitiesSnapshot)
            {

                //if entityFrom is physical
                if (entFrom is PhysicalEntity)
                {

                    //if entityFrom is stats or interactive
                    if (entFrom is StatsEntity || entFrom is InteractiveEntity)
                    {
                        entFrom.Update();

                        //contact A to B
                        foreach (var entTo in entitiesSnapshot)
                        {

                            if(entFrom != entTo)
                            {
                                //if entityTo is also stats 
                                if (entTo is StatsEntity)
                                {
                                    if(entFrom is StatsEntity)
                                    {
                                        HitboxChecker.CheckForCollision((StatsEntity)entFrom, (StatsEntity)entTo);
                                    }
                                    else if (entFrom is InteractiveEntity && entTo is Player)
                                    {
                                        HitboxChecker.CheckForInterraction((InteractiveEntity)entFrom, (EquipmentEntity)entTo);
                                    }
                                }
                            }

                            
                        }
                    }
                }
            }
        }


        public bool HasPlayer()
        {
            return Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Contains(Entities.player);
        }

        public void AddEntity(Entity ent)
        {
            if (ent is PhysicalEntity physicalEntity)
            {
                Physics.Physics.flatWorld.AddBody(physicalEntity.Model.body);
            }
            Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Add(ent);

        }
        public void RemoveEntity(Entity ent)
        {
            if (ent is PhysicalEntity physicalEntity)
            {
                Physics.Physics.flatWorld.RemoveBody(physicalEntity.Model.body);
            }
            Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Remove(ent);
            
        }


        public void RemoveAll()
        {
            var entitiesSnapshot = Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.ToList();
            foreach (Entity entity in entitiesSnapshot)
            {
                if(!(entity is Player))
                {
                    RemoveEntity(entity);
                }
                
            }
        }


        public int GenerateId()
        {
            if (Entities.entityMapManager == null || Entities.entityMapManager.maps == null || Entities.entityMapManager.CurrentMapId < 0 || Entities.entityMapManager.CurrentMapId >= Entities.entityMapManager.maps.Length)
            {
                return nextId++;
            }

            var entities = Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities ?? (Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities = new List<Entity>());
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

        public Entity GetEntityById(int id)
        {
            return Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.FirstOrDefault(e => e.Id == id);
        }


        //models
        public void Draw()
        {
            var sortedEntities = Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities
                .OrderBy(e =>
                    (e is TileEntity plent) ? 0
                    : (e is PhysicalEntity phent) ? phent.spriteZ
                    : (e is PlatformEntity platformEntity) ? 0
                    : float.MaxValue);

            foreach (var entity in sortedEntities)
            {
                entity.Draw();

                if (entity is EquipmentEntity eqEntity)
                {
                    eqEntity.DrawWeapon();
                }
            }
        }



        //collisions
        public void DrawColliders()
        {
            foreach (var entity in Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities)
            {
                if (entity is PhysicalEntity physEnt)
                {
                    physEnt.DrawCollider();

                    if (entity is InteractiveEntity iEnt)
                    {
                        iEnt.DrawInteractionField();
                    }
                }
            }
        }

        
        //hitboxes
        public void DrawHitboxes()
        {
            foreach (var entity in Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities)
            {
                if (entity is StatsEntity statsEntity)
                {
                    statsEntity.DrawHitboxes();
                }
            }
        }

    }
}
