using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class Event
    {

        public int Id;

        public Event(int Id)
        {
        }

        public virtual void Init() { }
        public virtual void Update() { }
    }
}
