using Microsoft.Xna.Framework;
using Physics;
using System;
using Utils;

namespace Entities
{

    public static class BattleTakingDamageHandler
    {

        public static void HandleTakingDamage(BattleEntity toEnt, BattleEntity fromEnt, RotatedRectangle toEntHitboxExtends, RotatedRectangle fromEntHitboxExtends)
        {
            ReceivePhysDamage(toEnt, BattleStatsCalculator.GetFinalPhysDamageForBattleEntity(fromEnt));
            ReceiveMagicDamage(toEnt, BattleStatsCalculator.GetFinalMagicDamageForBattleEntity(fromEnt));
            ReceivePoiseDamage(toEnt, BattleStatsCalculator.GetFinalPoiseDamageForBattleEntity(fromEnt));
            ReceiveKnockBack(toEnt, BattleStatsCalculator.GetFinalKnockbackPowerForBattleEntity(fromEnt), fromEntHitboxExtends.Position);

            GenerateParticle(toEnt, BattleStatsCalculator.GetFinalKnockbackPowerForBattleEntity(fromEnt), fromEntHitboxExtends.Position);
            PlayRecivingDamageSound(toEnt);

            Console.WriteLine(toEnt.Stats.HP + "/" + toEnt.Stats.maxHP);
        }

        public static void HandleTakingDamage(BattleEntity toEnt, ProjectileEntity fromEnt)
        {
            ReceivePhysDamage(toEnt, fromEnt.BattleDamageStatsData.DamageSet.PhysDamage);
            ReceiveMagicDamage(toEnt, fromEnt.BattleDamageStatsData.DamageSet.MagicDamage);
            ReceivePoiseDamage(toEnt, fromEnt.BattleDamageStatsData.PoiseDamage);
            ReceiveKnockBack(toEnt, fromEnt.BattleDamageStatsData.KnockbackPower, fromEnt.MoveDirection);

            GenerateParticle(toEnt, fromEnt.BattleDamageStatsData.KnockbackPower, fromEnt.MoveDirection);
            PlayRecivingDamageSound(toEnt);

            Console.WriteLine(toEnt.Stats.HP + "/" + toEnt.Stats.maxHP + ", projectile damage");
        }


        public static void ReceivePhysDamage(BattleEntity toEnt, float damage)
        {
            if (!(toEnt is Player && GameStateManager.IsGod))
            {
                toEnt.Stats.ReceiveDamage(damage);
            }
        }

        public static void ReceiveMagicDamage(BattleEntity toEnt, float damage)
        {
            if (!(toEnt is Player && GameStateManager.IsGod))
            {
                toEnt.Stats.ReceiveDamage(damage);
            }
        }

        public static void ReceivePoiseDamage(BattleEntity toEnt, float poisePower)
        {
            if (!(toEnt is Player && GameStateManager.IsGod))
            {
                toEnt.Stats.ReceivePoiseDamage(poisePower);
            }
        }

        public static void ReceiveKnockBack(StatsEntity toEnt, float knockBackPower, Vector2 fromEntHitboxExtendsPos)
        {
            FlatVector knockbackForce = CalculateKnockBackForce(toEnt.Model.Body.Position, FlatConverter.ToFlatVector(fromEntHitboxExtendsPos), knockBackPower);
            FlatVector fixedKnockbackForce = new FlatVector(knockbackForce.X, knockbackForce.Y + knockBackPower);
            toEnt.Model.Body.ApplyForce(fixedKnockbackForce);
        }

        public static FlatVector CalculateKnockBackForce(FlatVector toEntPos, FlatVector fromEntPos, float knockbackPower)
        {
            FlatVector direction = FlatMath.Normalize(toEntPos - fromEntPos);
            return direction * knockbackPower;
        }

        public static void GenerateParticle(BattleEntity toEnt, float knockbackPower, Vector2 fromEntHitboxExtendsPos)
        {
            if (toEnt.BloodDropParticle != Graphics.ParticleSet.ParticleSets.NONE)
            {
                Graphics.Graphics.particleManager.ParticleSets.Add(new Graphics.ParticleSet(toEnt.BloodDropParticle, toEnt.Model.Body.Position.ToVector2(), CalculateKnockBackForce(toEnt.Model.Body.Position, FlatConverter.ToFlatVector(fromEntHitboxExtendsPos), knockbackPower).ToVector2() / 2));
            }
        }

        public static void PlayRecivingDamageSound(BattleEntity toEnt)
        {
            if (toEnt.soundSet[Resources.EntitySounds.RECEIVEDAMAGE][0] != Resources.Sounds.NONE)
            {
                Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(toEnt.soundSet[Resources.EntitySounds.RECEIVEDAMAGE][0], toEnt.Model.Body.Position.ToVector2(), 1f));
            }
        }
    }
}
