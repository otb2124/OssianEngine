using Physics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using Utils;

namespace Entities
{
    public class StatsManager
    {


        public IndicatorStats IndicatorStats;
        public SprintStats SprintStats;
        public RollStats RollStats;
        public ExperienceStats ExperienceStats;
        public MovementSpeedStats MovementSpeedStats;
        public JumpStats JumpStats;
        public FlyStats FlyStats;
        public AggroStats AggroStats;
        public PoiseStats PoiseStats;
        public BattleHitStatsSet BodyHitStatsSet;

        public StatsBattleHitSpendHandler StatsBattleHitSpendHandler;
        public StaminaRegenerationHandler StaminaRegenerationHandler;
        public InvincibleFramesHandler InvincibleFramesHandler;
        public FallStatesHandler FallStatesHandler;
        public LedgeHangingHandler LedgeHangingHandler;
        public DescencionHandler DescencionHandler;
        public GCSRectanglesStatesHandler GCSRectanglesStatesHandler;
        public ItemPickupHandler ItemPickupHandler;
        public FlyHandler FlyHandler;
        

        public void Refill()
        {
            if(IndicatorStats != null)
            {
                IndicatorStats.Refill();
            }
            if(MovementSpeedStats != null)
            {
                MovementSpeedStats.Refill();
            }
            if(PoiseStats != null)
            {
                PoiseStats.Refill();
            }
        }

        public void RegenStamina()
        {
            if (StatsBattleHitSpendHandler != null)
            {
                if(StatsBattleHitSpendHandler.StatsPerAttackHitSpent)
                {
                    return;
                }
            }

            StaminaRegenerationHandler.RegenStamina(IndicatorStats);
        }

        public void SpendStatsForBattleHit(BattleEntity ent)
        {
            StatsBattleHitSpendHandler.SpendStatsForBattleHit(IndicatorStats, ent);
        }

        public void UpdateInvincibleFrames()
        {
            InvincibleFramesHandler.UpdateInvincibleFrames();
        }

        public void ReceiveDamage(float amount)
        {
            IndicatorStats.HP -= amount;
        }

        public void ReceivePoiseDamage(float amount)
        {
            PoiseStats.Poise -= amount;
        }

        public void UpdateFallen(Resources.Model model)
        {
            FallStatesHandler.UpdateFallen(model, PoiseStats);
        }

        public void UpdateLedgeHanging(Resources.Model model)
        {
            LedgeHangingHandler.UpdateLedgeHanging(model, GCSRectanglesStatesHandler);
        }


        public void UpdateGCSStates(Resources.Model model)
        {
            GCSRectanglesStatesHandler.Update(model);
        }

        public void UpdateDescending(Resources.Model model)
        {
            DescencionHandler.UpdateDescending(model, GCSRectanglesStatesHandler);
        }


        public void UpdateFly(Resources.Model model)
        {
            FlyHandler.UpdateFly(model, GCSRectanglesStatesHandler);
        }


        public void UpdatePickup()
        {
            ItemPickupHandler.Update();
        }

        public bool LostPoise()
        {
            return PoiseStats.Poise <= 0;
        }

        public bool CheckDead()
        {
            return IndicatorStats.HP <= 0;
        }
    }
}
