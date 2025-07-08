using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EntityMapManager
    {

        public EntityMap[] maps;
        public int CurrentMapId;
        public int MapsCount;

        public EntityMapManager()
        {
            MapsCount = 2;
            CurrentMapId = 1;
        }

        public void Init()
        {
            maps = new EntityMap[MapsCount];

            for (int i = 0; i < MapsCount; i++)
            {
                maps[i] = new EntityMap(i);
            }
        }
    }
}
