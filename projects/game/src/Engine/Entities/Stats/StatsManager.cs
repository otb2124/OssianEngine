using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatsManager
    {
        public StatsManager() { }


        public void DealDamage(LivingEntity from, LivingEntity to) 
        {
            to.stats.HP = to.stats.HP - from.stats.dmg;
        }
    }
}
