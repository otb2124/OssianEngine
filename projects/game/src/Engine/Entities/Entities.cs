using Microsoft.Xna.Framework;
using System;

namespace Entities
{
    public static class Entities
    {

        public static EntityManager EntityManager;
        public static EntityMapManager EntityMapManager;
        public static EventManager EventManager;

        public static readonly float GlobalStartTime = 18f;

        public static DialogueManager DialogueManager;

        public static void Init()
        {
            EntityMapManager = new EntityMapManager();
            EntityManager = new EntityManager();
            EventManager = new EventManager();

            DialogueManager = new DialogueManager();

            EntityMapManager.Init();
        }

        public static void Update()
        {
            EntityManager.Update();
            EventManager.Update();
            EntityMapManager.Update();
        }

        public static Player Player;
    }
}
