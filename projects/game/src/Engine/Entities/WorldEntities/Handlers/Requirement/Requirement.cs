using System;
using System.Linq;

namespace Entities
{
    public class Requirement
    {
        public bool IsNegation;
        public virtual bool Check() => false;
    }

    public class OrRequirement : Requirement
    {
        public Requirement[] Requirements;

        public OrRequirement(Requirement[] requirements, bool negate = false)
        {
            Requirements = requirements;
            IsNegation = negate;
        }

        public override bool Check() => IsNegation ? !Requirements.Any(r => r.Check()) : Requirements.Any(r => r.Check());
    }

    public class AndRequirement : Requirement
    {
        public Requirement[] Requirements;

        public AndRequirement(Requirement[] requirements, bool negate = false)
        {
            Requirements = requirements;
            IsNegation = negate;
        }

        public override bool Check() => IsNegation ? !Requirements.All(r => r.Check()) : Requirements.All(r => r.Check());
    }
}