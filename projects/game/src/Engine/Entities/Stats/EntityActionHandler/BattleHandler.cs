using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class BattleHandler
    {


        public static void HandleHit(StatsEntity toEnt, float damage)
        {
            Console.WriteLine(toEnt.Stats.HP + "/" + toEnt.Stats.maxHP);

            if (toEnt.Stats.HP > 0)
            {
                toEnt.Stats.ReceiveDamage(damage);
            }
            else
            {
                HandleDeath(toEnt);
            }
        }

        public static void HandleDeath(Entity ent)
        {
            Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Entities.Remove(ent);
        }
    }
}
