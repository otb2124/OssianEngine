namespace Entities
{
    public class StatsManager
    {



        public ExperienceStats ExperienceStats;
        public BattleHitStatsSet BodyHitStatsSet;

        /*
        public EntityStat Health;
        public EntityStat Mana;
        public EntityStat Stamina;
        public EntityStat RollSpeedMultiplier;
        public EntityStat MovementSpeedMultiplier;
        public EntityStat JumpSpeedMultiplier;
        public EntityStat FlySpeedMultiplier;
        public EntityStat SprintSpeedMultiplier;
        public EntityStat Poise;
        public EntityStat AggroDistance
        public EntityStat UnAggroDistance
        */

        public EntityStat[] Stats;

        public StatsBattleHitSpendHandler StatsBattleHitSpendHandler;
        public StaminaRegenerationHandler StaminaRegenerationHandler;
        public InvincibleFramesHandler InvincibleFramesHandler;
        public FallStatesHandler FallStatesHandler;
        public LedgeHangingHandler LedgeHangingHandler;
        public DescencionHandler DescencionHandler;
        public GCSRectanglesStatesHandler GCSRectanglesStatesHandler;
        public ItemPickupHandler ItemPickupHandler;
        public FlyHandler FlyHandler;
        

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

        public void ReceiveIndicatorDamage(float amount)
        {
            GetStat(EntityStats.HP).ModifyCurrent(amount);
        }

        public void ReceivePoiseDamage(float amount)
        {
            GetStat(EntityStats.POISE).ModifyCurrent(amount);
        }

        public void UpdateStaminaRegeneration()
        {
            StaminaRegenerationHandler.Update(GetStat(EntityStats.STAMINA), StatsBattleHitSpendHandler);
        }

        public void UpdateInvincibleFrames()
        {
            InvincibleFramesHandler.Update();
        }

        public void UpdateFallStates(Resources.Model model)
        {
            FallStatesHandler.Update(model, GetStat(EntityStats.POISE));
        }

        public void UpdateLedgeHanging(Resources.Model model)
        {
            LedgeHangingHandler.Update(model, GCSRectanglesStatesHandler);
        }


        public void UpdateGCSRectanglesStates(Resources.Model model)
        {
            GCSRectanglesStatesHandler.Update(model);
        }

        public void UpdateDescencion(Resources.Model model)
        {
            DescencionHandler.Update(model, GCSRectanglesStatesHandler);
        }


        public void UpdateFly(Resources.Model model)
        {
            FlyHandler.Update(model, GCSRectanglesStatesHandler);
        }


        public void UpdatePickup()
        {
            ItemPickupHandler.Update();
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
