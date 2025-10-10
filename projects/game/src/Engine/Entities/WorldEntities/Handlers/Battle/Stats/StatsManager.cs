namespace Entities
{
    public class StatsManager
    {
        public ExperienceStats ExperienceStats;
        public BattleHitStatsSet BodyHitStatsSet;

        public EntityStat[] Stats;
        public EntityStatFeature[] StatFeatures;

        public StatsBattleHitSpendHandler StatsBattleHitSpendHandler;

        //TODO: ???
        public bool AllowJumpDescendingLock;
        public bool AllowJumpDescending;

        public bool FlyingUpwards;

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


        public void UpdateFeatures(Resources.Model model)
        {
            foreach (EntityStatFeature feature in StatFeatures)
            {
                if(feature != null)
                {
                    feature.Update(this, model);
                }
            }
        }


        public EntityStatFeature GetStatFeature(EntityStatFeatures type)
        {
            foreach (EntityStatFeature feature in StatFeatures)
            {
                if (feature.Type == type)
                {
                    return feature;
                }
            }

            return null;
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
