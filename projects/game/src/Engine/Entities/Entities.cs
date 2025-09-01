using Microsoft.Xna.Framework;
using System;

namespace Entities
{
    public static class Entities
    {

        public static EntityManager entityManager;
        public static EntityMapManager entityMapManager;
        public static EventManager eventManager;

        public static readonly float GlobalStartTime = 12f;

        public static void Init()
        {
            entityMapManager = new EntityMapManager();
            entityManager = new EntityManager();
            eventManager = new EventManager();

            entityMapManager.Init();
        }

        public static void Update()
        {
            entityManager.Update();
            eventManager.Update();
            entityMapManager.Update();
        }

        public static Player Player;
    }
}
