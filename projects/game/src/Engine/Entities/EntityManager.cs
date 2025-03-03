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
            entities.Add(new PhysicalEntity(Models.PLATFORM, new Vector2(0, -50), 0.2f));
            entities.Add(new PhysicalEntity(Models.PLATFORM, new Vector2(150, -50)));

            entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-100, -50)));
            entities.Add(new PhysicalEntity(Models.CRATE_0, new Vector2(-200, -50)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 0)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 30)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(0, 50)));
            entities.Add(new PhysicalEntity(Models.BALL, new Vector2(-20, 100)));
            entities.Add(new Player(new Vector2(0, 20)));

            entities.Add(new PhysicalEntity(Models.PLATFORM, new Vector2(-100, -200)));
            entities.Add(new PhysicalEntity(Models.PLATFORM, new Vector2(50, -200)));
            entities.Add(new PhysicalEntity(Models.PLATFORM, new Vector2(200, -200)));
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
            foreach (var entity in entities
                .Where(e => e is PhysicalEntity)
                .OrderBy(e => StaticSpriteFactory.spriteMappings[((PhysicalEntity)e).model.sprite].z))
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
            foreach (var entity in entities.Where(e => e is PhysicalEntity))
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
