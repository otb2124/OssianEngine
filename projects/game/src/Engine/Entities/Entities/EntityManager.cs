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

            foreach (var entA in entitiesSnapshot)
            {
                if (entA is PhysicalEntity phent)
                {
                    if (entA is LivingEntity || entA is InteractiveEntity)
                    {
                        entA.Update();

                        foreach (var entB in entitiesSnapshot)
                        {
                            if (entB is LivingEntity livingB && entA != entB)
                            {
                                // Check for attack hit
                                if (entA is LivingEntity livingA)
                                {
                                    HitboxChecker.CheckForCollisions(livingA, livingB);
                                }

                                // Check for interaction
                                if (entA is InteractiveEntity interactiveA && livingB is Player)
                                {
                                    HitboxChecker.CheckForInterraction(interactiveA, livingB);
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

                if (entity is LivingEntity livingEntity)
                {
                    livingEntity.DrawWeapon();
                }
            }
        }



        //collisions
        public void DrawColliders()
        {
            foreach (var entity in Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities)
            {
                entity.DrawCollider();
            }
        }

        
        //hitboxes
        public void DrawHitboxes()
        {
            foreach (var entity in Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities)
            {
                entity.DrawHitbox();
            }
        }

    }
}
