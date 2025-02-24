using Microsoft.Xna.Framework;
using Physics;

namespace Entities
{
    public class EntityManager
    {

        Entity ent1;
        Entity ent2;

        Player player;

        public void Init()
        {
            ent1 = new LivingEntity(FlatBodyFactory.FlatBodyPreset.PLATFORM, new Vector2(0, -50), 0.2f);
            ent2 = new LivingEntity(FlatBodyFactory.FlatBodyPreset.CIRCLE, new Vector2(0, 0));

            player = new Player(new Vector2(0, 20), 0f);
        }

        public void Update()
        {
            player.Update();
        }


        public void Draw()
        {
            ent1.Draw();
            ent2.Draw();

            player.Draw();
        }
    }
}
