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

        public EntityManager()
        {
            
        }

        public void Init()
        {
            Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities = EntitySetter.FillEntityMap(Entities.entityMapManager.CurrentMapId);
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

                                    

                                    
                                }
                                else
                                {
                                    // Check for interaction
                                    if (entFrom is InteractiveEntity && entTo is Player)
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
