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
        
        

        


        



        public bool AllowPickup = true;
        public int PickupCounter = 0;
        public float PickupLockSec = 0.25f;

        public bool FlyingUpwards = true;
        public int FlyingCounter = 0;
        public float MaxFlyTimeSec = 0.5f;
        public float FlyHeightPointOverHead = 50f;
        public float CurrentFlyHeightPointOverHead = 50f;
        public float LandPoint;


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
            LedgeHangingHandler.UpdateLedgeHanging(model, GCSRectanglesStatesHandler.IsTouchingWalls);
        }


        public void UpdateGCSStates(Resources.Model model)
        {
            GCSRectanglesStatesHandler.Update(model);
        }

        public void UpdateDescending(Resources.Model model)
        {
            DescencionHandler.UpdateDescending(model, GCSRectanglesStatesHandler);
        }


        public void UpdateFly(StatsEntity ent)
        {
            if(GCSRectanglesStatesHandler.IsGrounded)
            {
                LandPoint = ent.Model.Body.Position.Y;
                CurrentFlyHeightPointOverHead = FlyHeightPointOverHead;
            }

            if (ent.Model.ModelState != ModelStates.FLYING && ent.Model.ModelState != ModelStates.FLYING_AND_MOVING)
            {
                FlyingCounter = 0;
                return;
            }
                

            if(ent.Model.Body.Position.Y < LandPoint + CurrentFlyHeightPointOverHead && FlyingUpwards)
            {
                FlyingUpwards = true;
                return;
            }

            if(FlyingUpwards)
            {
                FlyingCounter++;

                if (FlyingCounter >= MaxFlyTimeSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    FlyingUpwards = false;
                    FlyingCounter = (int)(MaxFlyTimeSec * Graphics.Graphics.UpdatesPerSecond);
                }
            }
            else
            {
                FlyingCounter--;
                if(FlyingCounter <= 0)
                {
                    FlyingUpwards = true;
                    FlyingCounter = 0;
                }
            }

            
        }


        public void UpdatePickup()
        {
            if(!AllowPickup)
            {
                PickupCounter++;
                if(PickupCounter > PickupLockSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    AllowPickup = true;
                    PickupCounter = 0;
                }
            }
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
