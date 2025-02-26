using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Entity
    {
        public Entity() {}
        public virtual void Update() {}
        public virtual void Draw() {}

        public virtual void DrawDebug() {}
    }
}
