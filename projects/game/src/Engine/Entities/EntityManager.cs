using Microsoft.Xna.Framework;
using Physics;
using System.Collections.Generic;
using System.Linq;

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
            entities.Add(new SpriteEntity(Resources.Sprite.Sprites.BACKGROUND, Vector2.Zero));

            entities.Add(new SpriteEntity(Resources.Sprite.Sprites.DRAGON, new Vector2(-300, 0)));

            entities.Add(new PhysicalEntity(FlatBodyFactory.FlatBodyPreset.PLATFORM, Resources.Sprite.Sprites.PLATFORM, new Vector2(0, -50), 0.2f));
            entities.Add(new PhysicalEntity(FlatBodyFactory.FlatBodyPreset.BLOCK, Resources.Sprite.Sprites.CRATE, new Vector2(-100, -50)));
            entities.Add(new PhysicalEntity(FlatBodyFactory.FlatBodyPreset.CIRCLE, Resources.Sprite.Sprites.CIRCLE, new Vector2(0, 0)));
            entities.Add(new Player(new Vector2(0, 20)));

            entities.Add(new PhysicalEntity(FlatBodyFactory.FlatBodyPreset.PLATFORM, Resources.Sprite.Sprites.PLATFORM, new Vector2(-100, -200)));
            entities.Add(new PhysicalEntity(FlatBodyFactory.FlatBodyPreset.PLATFORM, Resources.Sprite.Sprites.PLATFORM, new Vector2(100, -200)));
            entities.Add(new PhysicalEntity(FlatBodyFactory.FlatBodyPreset.BOX, Resources.Sprite.Sprites.CRATE, new Vector2(-50, -150)));
            entities.Add(new Mob(new Vector2(-60, -140), 0f));
            entities.Add(new PhysicalEntity(FlatBodyFactory.FlatBodyPreset.BOX, Resources.Sprite.Sprites.CRATE, new Vector2(-65, -150)));

            
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
            foreach (var entity in entities.OfType<SpriteEntity>().OrderBy(e => e.sprite.zIndex))
            {
                entity.Draw();
            }
        }

    }
}
