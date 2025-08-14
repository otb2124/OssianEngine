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
    public static class BattleHandler
    {


        public static void HandleHit(StatsEntity toEnt, float damage, float knockBackPower, Vector2 fromEntPos)
        {
            if(!GameStateManager.IsGod && toEnt.Stats.HP <= 0)
            {
                HandleDeath(toEnt);
            }

            if (toEnt.Stats.IsInvincible)
            {
                return;
            }
            
            if (toEnt.Stats.HP > 0)
            {
                HandleTakingDamage(toEnt, damage, knockBackPower, fromEntPos);
            }

            if(toEnt.Stats.IsInvincible != true)
            {
                toEnt.Stats.IsInvincible = true;
            }
        }

        public static void HandleBlockHit(StatsEntity toEnt, float damage, float knockBackPower, Vector2 fromEntPos)
        {
            if (toEnt.Stats.IsInvincible)
            {
                return;
            }

            HandleTakingKnockback(toEnt, CalculateKnockBackForce(toEnt.Model.Body.Position, FlatConverter.ToFlatVector(fromEntPos), knockBackPower), knockBackPower);

            if (toEnt.Stats.IsInvincible != true)
            {
                toEnt.Stats.IsInvincible = true;
            }
        }


        public static void HandleTakingDamage(StatsEntity toEnt, float damage, float knockBackPower, Vector2 fromEntPos)
        {
            if (!(toEnt is Player && GameStateManager.IsGod))
            {
                toEnt.Stats.ReceiveDamage(damage);
                toEnt.Stats.ReceivePoiseDamage(damage);
            }

            FlatVector knockbackForce = CalculateKnockBackForce(toEnt.Model.Body.Position, FlatConverter.ToFlatVector(fromEntPos), knockBackPower);
            HandleTakingKnockback(toEnt, knockbackForce, knockBackPower);

            if (toEnt.BloodDropParticle != Graphics.ParticleSet.ParticleSets.NONE)
            {
                Graphics.Graphics.particleManager.ParticleSets.Add(new Graphics.ParticleSet(toEnt.BloodDropParticle, toEnt.Model.Body.Position.ToVector2(), knockbackForce.ToVector2() / 2));
            }

            if(toEnt.soundSet[Resources.EntitySounds.RECEIVEDAMAGE][0] != Resources.Sounds.NONE)
            {
                Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(toEnt.soundSet[Resources.EntitySounds.RECEIVEDAMAGE][0], toEnt.Model.Body.Position.ToVector2(), 1f));
            }

            Console.WriteLine(toEnt.Stats.HP + "/" + toEnt.Stats.maxHP);
        }

        public static void HandleTakingKnockback(StatsEntity toEnt, FlatVector knockbackForce, float knockBackPower)
        {
            FlatVector fixedKnockbackForce = new FlatVector(knockbackForce.X, knockbackForce.Y + knockBackPower);
            toEnt.Model.Body.ApplyForce(fixedKnockbackForce);
        }

        public static FlatVector CalculateKnockBackForce(FlatVector toEntPos, FlatVector fromEntPos, float knockbackPower)
        {
            FlatVector direction = FlatMath.Normalize(toEntPos - fromEntPos);
            return direction * knockbackPower;
        }

        public static void HandleDeath(StatsEntity ent)
        {
            if(!ent.DropInventory.IsEmpty())
            {
                List<Item> droppedItems = ent.DropInventory.TryDrop();

                foreach(Item item in droppedItems)
                {
                    Entities.entityMapManager.GetCurrentMap().Entities.Add(EntityHelper.CreateItemDrop(item, ent.Model.Body.Position.ToVector2()));

                    //TODO: change to reinit for the interractive item emission
                    Graphics.Graphics.lightManager.Init();
                }
            }

            Entities.entityManager.RemoveEntity(ent);
        }
    }
}
