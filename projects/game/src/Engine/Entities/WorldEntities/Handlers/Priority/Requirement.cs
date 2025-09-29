using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{


    public class Requirement
    {
        public Requirement(){}

        public virtual bool Check() {  return false; }
    }
}
