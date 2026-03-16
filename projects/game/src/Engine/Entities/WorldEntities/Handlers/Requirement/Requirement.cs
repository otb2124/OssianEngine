using System;
using System.Linq;

namespace Entities
{
    public class Requirement
    {
        public StatsEntity Entity;
        public bool IsNegation;

        public virtual bool Check(StatsEntity Entity = null) => false;
    }

    public class OrRequirement : Requirement
    {
        public Requirement[] Requirements;

        public OrRequirement(Requirement[] requirements, bool negate = false)
        {
            Requirements = requirements;
            IsNegation = negate;
        }

        public override bool Check(StatsEntity Entity = null) => IsNegation ? !Requirements.Any(r => r.Check(Entity)) : Requirements.Any(r => r.Check(Entity));
    }

    public class AndRequirement : Requirement
    {
        public Requirement[] Requirements;

        public AndRequirement(Requirement[] requirements, bool negate = false)
        {
            Requirements = requirements;
            IsNegation = negate;
        }

        public override bool Check(StatsEntity Entity = null) => IsNegation ? !Requirements.All(r => r.Check(Entity)) : Requirements.All(r => r.Check(Entity));
    }
}