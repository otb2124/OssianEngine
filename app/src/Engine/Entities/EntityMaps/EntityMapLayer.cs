using Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EntityMapLayer
    {

        public int Id;
        public List<WorldEntity> Entities;
        public List<Event> Events;
        public List<FilterLayer> FilterLayers;
        public Point Size;


        public EntityMapLayer(int id, Point size)
        {
            Id = id;
            Size = size;
            Entities = new List<WorldEntity>();
            Events = new List<Event>();
            FilterLayers = new List<FilterLayer>();
        }
    }
}
