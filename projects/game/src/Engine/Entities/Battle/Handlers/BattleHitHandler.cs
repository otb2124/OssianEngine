using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public static class BattleHitHandler
    {
        
        public static void HandleHit(BattleEntity toEnt, BattleEntity fromEnt, RotatedRectangle toEntHitboxExtends, RotatedRectangle fromEntHitboxExtends)
        {
            if (!toEnt.Stats.IsInvincible)
            {
                if (toEnt.Stats.HP > 0)
                {
                    BattleTakingDamageHandler.HandleTakingDamage(toEnt, fromEnt, toEntHitboxExtends, fromEntHitboxExtends);
                }
            }

            HandleInvincibility(toEnt);
        }

        public static void HandleHit(BattleEntity toEnt, ProjectileEntity fromEnt)
        {
            if (!toEnt.Stats.IsInvincible)
            {
                if (toEnt.Stats.HP > 0)
                {
                    BattleTakingDamageHandler.HandleTakingDamage(toEnt, fromEnt);
                }
            }

            HandleInvincibility(toEnt);
        }


        //TODO: HANDLE DAMAGE DATA AS HANDLEHIT()
        public static void HandleBlockHit(StatsEntity toEnt, float damage, float knockBackPower, Vector2 fromEntPos)
        {
            if (!toEnt.Stats.IsInvincible)
            {
                BattleTakingDamageHandler.ReceiveKnockBack(toEnt, knockBackPower, fromEntPos);
            }

            HandleInvincibility(toEnt);
        }

        public static void HandleInvincibility(StatsEntity entity)
        {
            if (entity.Stats.IsInvincible)
                return;
            else
                entity.Stats.IsInvincible = true;
        }


        
    }
}
