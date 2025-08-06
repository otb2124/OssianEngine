using Microsoft.Xna.Framework;
using Physics;
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


        public static void HandleHit(StatsEntity toEnt, float damage, float knockBackPower, FlatVector fromEntPos)
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


        public static void HandleTakingDamage(StatsEntity toEnt, float damage, float knockBackPower, FlatVector fromEntPos)
        {
            if (!(toEnt is Player && GameStateManager.IsGod))
            {
                toEnt.Stats.ReceiveDamage(damage);
            }

            Console.WriteLine(toEnt.Stats.HP + "/" + toEnt.Stats.maxHP);
            

            FlatVector direction = FlatMath.Normalize(toEnt.Model.Body.Position - fromEntPos);
            FlatVector knockbackForce = direction * knockBackPower;
            toEnt.Model.Body.ApplyForce(knockbackForce);

            Console.WriteLine(knockbackForce);

            if (toEnt.BloodDropParticle != Graphics.ParticleSet.ParticleSets.NONE)
            {
                Graphics.Graphics.particleManager.ParticleSets.Add(new Graphics.ParticleSet(toEnt.BloodDropParticle, toEnt.Model.Body.Position.ToVector2(), knockbackForce.ToVector2() / 2));
            }

            if(toEnt.soundSet[Resources.EntitySounds.RECEIVEDAMAGE][0] != Resources.Sounds.NONE)
            {
                Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(toEnt.soundSet[Resources.EntitySounds.RECEIVEDAMAGE][0], toEnt.Model.Body.Position.ToVector2(), 1f));
            }
        }


        public static void HandleTakingKnockback()
        { }

        public static void HandleDeath(Entity ent)
        {
            Entities.entityManager.RemoveEntity(ent);
        }
    }
}
