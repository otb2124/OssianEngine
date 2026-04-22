using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class CurrentHPRequirement : Requirement
    {

        public int MinHP;
        public int MaxHP;

        public CurrentHPRequirement(int minHp, int maxHp) 
        {
            MinHP = minHp;
            MaxHP = maxHp;
        }

        public override bool Check(StatsEntity Entity)
        {
            return Entity.StatsManager.GetStat(EntityStats.HP).CurrentValue > MinHP && Entity.StatsManager.GetStat(EntityStats.HP).CurrentValue <= MaxHP;
        }
    }
}
