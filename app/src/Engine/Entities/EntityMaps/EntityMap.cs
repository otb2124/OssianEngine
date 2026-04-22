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
        public int Id;
        public List<EntityMapLayer> Layers;

        public EntityMap(int id)
        {
            Id = id;
            Layers = new List<EntityMapLayer>();
            Init();
        }

        public void Init()
        {
            switch(Id)
            {
                case 0:
                    Layers.Add(new EntityMapLayer(0, new Point(2560, 1440)));
                    Layers.Add(new EntityMapLayer(1, new Point(2560, 1440)));
                    break;
                case 1:
                    Layers.Add(new EntityMapLayer(0, new Point(2560, 1440)));
                    break;
            }
        }

        public EntityMapLayer GetLayer(int id)
        {
            return Layers.Where(layer => layer.Id == id).FirstOrDefault();
        }
    }
}
