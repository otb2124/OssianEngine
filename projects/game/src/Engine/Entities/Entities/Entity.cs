using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public Entity() 
        {
            Id = Entities.entityManager.GenerateId();
        }
        public Entity(int id)
        {
            Id = id;
        }
        public virtual void Update() {}
        public virtual void Draw() {}
    }
}
