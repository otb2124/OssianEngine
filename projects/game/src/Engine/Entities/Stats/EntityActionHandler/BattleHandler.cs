using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public static class BattleHandler
    {


        public static void HandleHit(StatsEntity toEnt, float damage)
        {
            //Console.WriteLine(toEnt.Stats.HP + "/" + toEnt.Stats.maxHP);

            if(GameStateManager.IsGod)
            {
                return;
            }
            
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
            Entities.entityManager.RemoveEntity(ent);
        }
    }
}
