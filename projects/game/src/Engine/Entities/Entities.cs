using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class Entities
    {

        public static EntityManager entityManager;
        public static EntityMapManager entityMapManager;

        public static void Init()
        {
            entityMapManager = new EntityMapManager();
            entityMapManager.Init();

            entityManager = new EntityManager();
            entityManager.Init();
        }

        public static Player player;
    }
}
