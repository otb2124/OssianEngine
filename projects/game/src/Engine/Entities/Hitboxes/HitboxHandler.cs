using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class HitboxHandler
    {


        public static void HandleHit(LivingEntity fromEnt, LivingEntity toEnt)
        {
            Debug.WriteLine("hit!");
            fromEnt.sManager.DealDamageTo(toEnt);
        }


        public static void HandleInterraction(InteractiveEntity interractiveEnt, LivingEntity livingEnt)
        {
            Debug.WriteLine("interraction!");
        }
    }
}
