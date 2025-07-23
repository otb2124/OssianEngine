using Microsoft.Xna.Framework;

namespace Entities
{
    public static class Entities
    {

        public static EntityManager entityManager;
        public static EntityMapManager entityMapManager;
        public static EventManager eventManager;

        public static void Init()
        {
            entityMapManager = new EntityMapManager();
            entityManager = new EntityManager();
            eventManager = new EventManager();

            entityMapManager.Init();
        }

        public static Player player;
    }
}
