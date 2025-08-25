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
                    BattleDamageHandler.HandleTakingDamage(toEnt, fromEnt, toEntHitboxExtends, fromEntHitboxExtends);
                }
            }

            HandleInvincibility(toEnt);

            if (!GameStateManager.IsGod && toEnt.Stats.HP <= 0)
            {
                HandleDeath(toEnt);
            }
        }

        public static void HandleBlockHit(StatsEntity toEnt, float damage, float knockBackPower, Vector2 fromEntPos)
        {
            if (!toEnt.Stats.IsInvincible)
            {
                BattleDamageHandler.ReceiveKnockBack(toEnt, knockBackPower, fromEntPos);
            }

            HandleInvincibility(toEnt);
        }

        public static void HandleDeath(StatsEntity ent)
        {
            if(!ent.DropInventory.IsEmpty())
            {
                List<Item> droppedItems = ent.DropInventory.TryDrop();

                foreach(Item item in droppedItems)
                {
                    InteractiveItemEntity itemEnt = EntityHelper.CreateItemDrop(item, ent.Model.Body.Position.ToVector2());
                    Entities.entityMapManager.GetCurrentMap().Entities.Add(itemEnt);
                    Graphics.Graphics.lightManager.AddEntityEmissionLightSource(itemEnt);
                }
            }

            Entities.entityManager.RemoveEntity(ent);
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
