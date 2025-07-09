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
            Console.WriteLine(toEnt.Stats.HP + "/" + toEnt.Stats.maxHP);

            if(!GameStateManager.IsGod && toEnt.Stats.HP <= 0)
            {
                HandleDeath(toEnt);
            }

            if (GameStateManager.IsGod || toEnt.Stats.IsInvincible)
            {
                return;
            }
            
            if (toEnt.Stats.HP > 0)
            {
                toEnt.Stats.ReceiveDamage(damage);
            }

            if(toEnt.Stats.IsInvincible != true)
            {
                toEnt.Stats.IsInvincible = true;
            }
        }

        public static void HandleDeath(Entity ent)
        {
            Entities.entityManager.RemoveEntity(ent);
        }
    }
}
