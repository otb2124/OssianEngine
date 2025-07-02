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

                    HandlePlatformOverlay(phent);
                }
            }
        }

        public void HandlePlatformOverlay(PhysicalEntity phent)
        {
            float lowestZ = phent.baseSpriteZ;
            float closestPlatformY = float.MaxValue;
            float closestPlatformZ = phent.baseSpriteZ;

            float entityHeight = (phent.model.body.BodyShapeType == BodyShapeType.Box)
                ? phent.model.body.Height
                : phent.model.body.Radius * 2;

            foreach (var plent in Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.OfType<PlatformEntity>())
            {
                float platformTopY = plent.Body.Position.Y + plent.Body.Height;

                /*
                if (phent.model.Body.Position.X < plent.Body.Position.X + plent.Body.Height / 2 &&
                    platformTopY > (phent.model.Body.Position.Y + entityHeight) &&
                    platformTopY < closestPlatformY)
                {
                    closestPlatformY = platformTopY;
                    closestPlatformZ = plent.spriteZ;
                }*/
            }

            if (closestPlatformY != float.MaxValue)
            {
                lowestZ = closestPlatformZ - 1;
            }

            phent.spriteZ = lowestZ;

        }



        //models
        public void Draw()
        {
            var sortedEntities = Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities
                .OrderBy(e =>
                    (e is PlatformEntity plent) ? 0
                    : (e is PhysicalEntity phent) ? phent.spriteZ
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
            foreach (var entity in Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Where(e => e is PhysicalEntity || e is PlatformEntity))
            {
                entity.DrawCollider();
            }
        }

        
        //hitboxes
        public void DrawHitboxes()
        {
            foreach (var entity in Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Where(e => e is LivingEntity || e is InteractiveEntity))
            {
                entity.DrawHitbox();
            }
        }

    }
}
