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
        private readonly float EntityOutOfBoudsDeletionMapSizeMultiplier = 1.5f;

        public EntityManager()
        {
            
        }

        public void Init()
        {
            //Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities = EntityMapSetter.FillEntityMap(Entities.entityMapManager.CurrentMapId);
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
                                    if(entFrom is BattleEntity && entTo is BattleEntity)
                                    {
                                        HitboxChecker.CheckWeaponToBodyCollision((BattleEntity)entFrom, (BattleEntity)entTo);
                                        HitboxChecker.CheckWeaponToWeaponCollision((BattleEntity)entFrom, (BattleEntity)entTo);
                                    }
                                    else if (entFrom is InteractiveEntity && entTo is Player)
                                    {
                                        HitboxChecker.CheckForInterraction((InteractiveEntity)entFrom, (EquipmentEntity)entTo);
                                    }
                                }
                            }

                            
                        }
                    }

                    RemovePhysicalEntityWhenOutOfBounds((PhysicalEntity)entFrom);
                }
            }
        }


        public bool HasPlayer()
        {
            return Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Contains(Entities.Player);
        }

        public void AddEntity(Entity ent)
        {
            if (ent is PhysicalEntity physicalEntity)
            {
                Physics.Physics.flatWorld.AddBody(physicalEntity.Model.Body);
            }
            Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Add(ent);

        }
        public void RemoveEntity(Entity ent)
        {
            if (ent is PhysicalEntity physicalEntity)
            {
                Physics.Physics.flatWorld.RemoveBody(physicalEntity.Model.Body);
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

        public void RemovePhysicalEntityWhenOutOfBounds(PhysicalEntity ent)
        {
            Vector2 entPos = ent.Model.Body.Position.ToVector2();
            Vector2 mapSize = Entities.entityMapManager.GetCurrentMap().Size.ToVector2() * EntityOutOfBoudsDeletionMapSizeMultiplier;
            Rectangle worldBounds = new Rectangle((int)-mapSize.X/2, (int)-mapSize.Y/2, (int)mapSize.X, (int)mapSize.Y);

            if (!worldBounds.Contains(entPos))
            {
                Console.WriteLine("Deleted at: " + entPos + " , MapSize: " + worldBounds);
                RemoveEntity(ent);
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
                if (entity is BattleEntity bEnt)
                {
                    bEnt.DrawHitboxes();
                }
            }
        }

    }
}
