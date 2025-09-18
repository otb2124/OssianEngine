using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class WorldEntity
    {
        public int Id { get; set; }
        public WorldEntity() 
        {
            Id = Entities.entityManager.GenerateId();
        }
        public WorldEntity(int id)
        {
            Id = id;
        }
        public virtual void Update() {}
        public virtual void Draw() {}
    }
}
