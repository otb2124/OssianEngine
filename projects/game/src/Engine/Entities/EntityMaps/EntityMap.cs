using Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EntityMap
    {

        public Point Size;
        public int Id;
        public List<Entity> Entities;
        public List<Event> Events;
        public List<FilterLayer> FilterLayers;

        public EntityMap(int id)
        {
            Id = id;
            Init();
        }

        public void Init()
        {
            switch(Id)
            {
                case 0:
                    Size = new Point(2560, 1440);
                    break;
                case 1:
                    Size = new Point(2560, 1440);
                    break;
            }
            
        }
    }
}
