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

        public Resource(string path) 
        {
            this.path = path;
        }

        public virtual void Load() { }

    }
}
