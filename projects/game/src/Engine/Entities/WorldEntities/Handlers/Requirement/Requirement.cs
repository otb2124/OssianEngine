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


    public class OrRequirement : Requirement
    {

        public Requirement[] Requirements;

        public OrRequirement(Requirement[] requirements)
        {
            Requirements = requirements;
        }

        public override bool Check()
        {
            //at least one is true
            foreach (Requirement requirement in Requirements)
            {
                if(requirement.Check())
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class AndRequirement : Requirement
    {

        public Requirement[] Requirements;

        public AndRequirement(Requirement[] requirements)
        {
            Requirements = requirements;
        }

        public override bool Check()
        {
            //all are true
            foreach (Requirement requirement in Requirements)
            {
                if (!requirement.Check())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
