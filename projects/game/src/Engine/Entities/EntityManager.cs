using Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Utils;

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
            entities.Add(new PlatformEntity(new Vector2(0, -50), 0.2f));
            entities.Add(new PlatformEntity(new Vector2(150, -50), new Vector2(3, 2)));
            entities.Add(new PlatformEntity(new Vector2(150, 100), new Vector2(3, 3), 0.75f));
            entities.Add(new PlatformEntity(new Vector2(300, 100), new Vector2(3, 3)));

            entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-100, -50)));
            entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-200, -50)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 0)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 30)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 50)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(-20, 100)));
            entities.Add(new Player(new Vector2(0, 20)));

            entities.Add(new PlatformEntity(new Vector2(-100, -200), 0f));
            entities.Add(new PlatformEntity(new Vector2(50, -200), 0f));
            entities.Add(new PlatformEntity(new Vector2(200, -200), 0f));

            entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-50, -150)));
            entities.Add(new PhysicalEntity(Models.CRATE_1, new Vector2(-65, -150)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(-70, -150)));

            entities.Add(new Mob(new Vector2(-60, -140), 0f));
            entities.Add(new Mob(new Vector2(-150, -140), 0f));
        }

        public void Update()
        {

            //hitboxes
            foreach (var entA in entities)
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
            }
            
        }


        //models
        public void Draw()
        {
            var sortedEntities = entities
               .OrderBy(e =>
               {
                   if (e is PlatformEntity platform)
                   {
                       return -5;
                   }
                   else if (e is PhysicalEntity physical)
                   {
                       return StaticSpriteFactory.spriteMappings[((PhysicalEntity)e).model.sprite].z;
                   }
                   return float.MaxValue; // Default for other entities
               });

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
