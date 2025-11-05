using System.Collections.Generic;

namespace Entities
{
    public class StatsManager
    {
        public ExperienceStats ExperienceStats;
        public BattleHitStatsSet BodyHitStatsSet;

        public EntityStat[] Stats;
        public EntityAbility[] Abilities;

        public StatsBattleHitSpendTool StatsBattleHitSpendHandler;

        public List<StatEffect> CurrentStatEffects;

        //TODO: ???
        public bool AllowJumpDescendingLock;
        public bool AllowJumpDescending;

        public bool FlyingUpwards = true;

        public bool OnUsingStamina;
        public bool OnStaminaRegen;

        public bool IsTouchingCeiling;
        public bool IsTouchingWalls;
        public bool IsGrounded;

        public float DescendingMultiplier;

        public bool IsFallen;
        public bool IsFalling;

        public bool IsInvincible = true;

        public bool AllowPickup = true;

        public bool AllowDoubleJump = false;

        public StatsManager()
        {
            CurrentStatEffects = new List<StatEffect>();
        }

        public void RefillAll()
        {
            foreach (EntityStat stat in Stats)
            {
                if(stat != null)
                {
                    stat.Refill();
                }
            }
        }

        public EntityStat GetStat(EntityStats statType)
        {
            foreach (EntityStat stat in Stats)
            {
                if(stat.Type == statType)
                {
                    return stat;
                }
            }

            return null;
        }

        public void SpendStatsForBattleHit(BattleEntity ent)
        {
            StatsBattleHitSpendHandler.SpendStatsForBattleHit(GetStat(EntityStats.STAMINA), GetStat(EntityStats.MANA), ent);
        }

        public void ReceiveHPDamage(float amount)
        {
            GetStat(EntityStats.HP).ModifyCurrent(amount);
        }

        public void ReceivePoiseDamage(float amount)
        {
            GetStat(EntityStats.POISE).ModifyCurrent(amount);
        }


        public void UpdateAbilities(Resources.Model model)
        {
            foreach (EntityAbility feature in Abilities)
            {
                if(feature != null)
                {
                    feature.Update(this, model);
                }
            }
        }


        public EntityAbility GetStatAbilities(EntityStatFeatures type)
        {
            foreach (EntityAbility ability in Abilities)
            {
                if (ability.Type == type)
                {
                    return ability;
                }
            }

            return null;
        }


        public void AddStatEffect(StatEffects effect)
        {
            CurrentStatEffects.Add(StatEffect.StatEffectMap[effect]);
        }


        public void UpdateStatEffects()
        {
            foreach (var item in CurrentStatEffects)
            {
                item.Update(Stats);
            }
        }


        public bool CheckEnoughFinalBattleStamina(BattleEntity ent)
        {
            return GetStat(EntityStats.STAMINA).CurrentValue - BattleStatsCalculator.GetFinalStaminaPerHitCostForBattleEntity(ent) > 0;
        }

        public bool CheckEnoughFinalBattleMana(BattleEntity ent)
        {
            return GetStat(EntityStats.MANA).CurrentValue - BattleStatsCalculator.GetFinalManaPerHitCostForBattleEntity(ent) > 0;
        }


        public bool CheckEnoughStaminaForStat(EntityStats stat)
        {
            return GetStat(EntityStats.STAMINA).CurrentValue - GetStat(stat).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond > 0;
        }

        public bool LostPoise()
        {
            return GetStat(EntityStats.POISE).LessEquealZero();
        }

        public bool CheckDead()
        {
            return GetStat(EntityStats.HP).LessEquealZero();
        }
    }
}
