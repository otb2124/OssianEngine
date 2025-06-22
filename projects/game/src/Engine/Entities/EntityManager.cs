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

        List<Entity> entities;

        public EntityManager()
        {
            entities = new List<Entity>();
        }

        public void Init()
        {
            entities.Add(new PlatformEntity(new Vector2(150, 100), 50, 3, 3, 0.75f));
            entities.Add(new PlatformEntity(new Vector2(300, 100), 50, 3, 3));
            entities.Add(new PlatformEntity(new Vector2(0,   -50), 49, 3, 2, 0.2f));
            entities.Add(new PlatformEntity(new Vector2(150, -50), 49, 3, 2));
            

            entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-100, -50)));
            entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-200, -50)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 0)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 30)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 50)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(-20, 100)));

            Entities.player = new Player(new Vector2(0, 20));
            entities.Add(Entities.player);

            entities.Add(new PlatformEntity(new Vector2(-200, -200), 47));
            entities.Add(new PlatformEntity(new Vector2(-100, -200), 47));
            entities.Add(new PlatformEntity(new Vector2(50, -200),   47));
            entities.Add(new PlatformEntity(new Vector2(200, -200),  47));
            entities.Add(new PlatformEntity(new Vector2(400, -200), 47));
            entities.Add(new PlatformEntity(new Vector2(600, -200), 47));
            entities.Add(new PlatformEntity(new Vector2(800, -200), 47));
            entities.Add(new PlatformEntity(new Vector2(1000, -200), 47));
            entities.Add(new PlatformEntity(new Vector2(1200, -200), 47));

            entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-50, -150)));
            entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-65, -150)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(-70, -150)));

            entities.Add(new Mob(new Vector2(-60, -140), 0f));
            entities.Add(new Mob(new Vector2(-150, -140), 0f));

            entities.Add(new PlatformEntity(new Vector2(0, -350), 46, 3, 2));
        }

        public void Update()
        {

            foreach (var entA in entities)
            {

                if (entA is PhysicalEntity phent)
                {
                    if (entA is LivingEntity livingA)
                    {
                        entA.Update();

                        foreach (var entB in entities)
                        {
                            if (entB is LivingEntity livingB && entA != entB)
                            {
                                Entities.hitboxHandler.CheckForCollisions(livingA, livingB);
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
            foreach (var entity in entities.Where(e => e is LivingEntity))
            {
                entity.DrawHitbox();
            }
        }

    }
}
