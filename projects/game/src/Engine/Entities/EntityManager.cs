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

        public List<Entity> entities;

        public EntityManager()
        {
            entities = new List<Entity>();
        }

        public void Init()
        {
            EntitySetter.setEntities(this.entities);
        }

        public void Update()
        {
            var entitiesSnapshot = entities.ToList();

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

            // Determine the entity's height based on its body shape
            float entityHeight = (phent.model.body.BodyShapeType == BodyShapeType.Box)
                ? phent.model.body.Height
                : phent.model.body.Radius * 2;

            foreach (var plent in entities.OfType<PlatformEntity>())
            {
                float platformTopY = plent.body.Position.Y + plent.body.Height;

                // Apply condition for X position before considering this platform
                if (phent.model.body.Position.X < plent.body.Position.X + plent.body.Height / 2 &&
                    platformTopY > (phent.model.body.Position.Y + entityHeight) &&
                    platformTopY < closestPlatformY)
                {
                    closestPlatformY = platformTopY;
                    closestPlatformZ = plent.spriteZ;
                }
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
            var sortedEntities = entities
                .OrderBy(e =>
                    (e is PlatformEntity plent) ? plent.spriteZ
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
            foreach (var entity in entities.Where(e => e is PhysicalEntity || e is PlatformEntity))
            {
                entity.DrawCollider();
            }
        }

        
        //hitboxes
        public void DrawHitboxes()
        {
            foreach (var entity in entities.Where(e => e is LivingEntity || e is InteractiveEntity))
            {
                entity.DrawHitbox();
            }
        }

    }
}
