using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class Resource
    {
        public string path;
        public int id;

        public Resource(string path, int id) 
        {
            this.path = path;
            this.id = id;
        }

        public virtual void Load() { }

    }
}
