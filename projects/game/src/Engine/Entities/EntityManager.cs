using Microsoft.Xna.Framework;
using Physics;
using System.Collections.Generic;

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
            entities.Add(new PhysicalEntity(FlatBodyFactory.FlatBodyPreset.PLATFORM, Resources.Sprite.Sprites.PLATFORM, new Vector2(0, -50), 0.2f));
            entities.Add(new PhysicalEntity(FlatBodyFactory.FlatBodyPreset.BOX, Resources.Sprite.Sprites.PLATFORM, new Vector2(-100, -50)));
            entities.Add(new LivingEntity(FlatBodyFactory.FlatBodyPreset.CIRCLE, Resources.Sprite.Sprites.CIRCLE, new Vector2(0, 0)));
            entities.Add(new Player(new Vector2(0, 20), 0f));
        }

        public void Update()
        {
            foreach (var entity in entities)
            {
                if(entity is Player)
                {
                    entity.Update();
                }
            }
            
        }


        public void Draw()
        {
            foreach (var entity in entities)
            {
                entity.Draw();
            }
        }
    }
}
