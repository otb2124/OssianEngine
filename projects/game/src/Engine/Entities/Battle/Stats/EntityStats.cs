using Physics;
using System;
using System.Collections.Generic;
using Utils;

namespace Entities
{
    public class EntityStats
    {
        //indicators
        public float maxHP;
        public float HP;

        public float maxMana;
        public float mana;

        public float maxStamina;
        public float stamina;
        public float staminaRegenSec;
        public float staminaSprintCostSec;
        public float staminaJumpCostSec;
        public float staminaRollCostSec;
 
        public int staminaUnlockCounter = 0;
        public float staminaUnlockSec;

        //attack
        public float staminaAttackHitCost;
        public bool staminaPerAttackHitSpent = false;

        //lvl
        public int currentLvl = 0;
        public int currentXP = 0;

        //speed jump roll
        public float maxSpeed;
        public float speed;
        public float jumpSpeed;

        public float DescendingMultiplier;
        public bool IsJumpDescending;
        public bool AllowJumpDescending;
        public bool AllowJumpDescendingLock = true;
        public bool IsGrounded;
        public bool IsTouchingCeiling;
        public bool IsTouchingWalls;
        public float MaxDescendingSec;
        public int DescendingCounter = 0;
        public bool AllowHangingOnLedge = true;
        public int HangingCounter = 0;
        public int UnHangingCounter = 0;

        public float rollMultiplier;
        public float sprintMultiplier;

        public float bodyDamage;
        public float bodyKnockbackPower;

        public float PoiseBodyDamage = 0f;
        public float Poise;
        public float MaxPoise;
        public float PoiseRegenSec;

        public bool OnStaminaRegen = false;
        public bool OnUsingStamina = false;

        public float invincibleFramesSec = 1f;
        public int invincibleCounter = 0;
        public bool IsInvincible = true;

        public bool IsFalling = false;

        public bool IsFallen = false;
        public float FallenTimer = 0f;
        public float FallenDurationAllowedSec = 3f;

        public float DistanceToAggro = -1f;
        public float DistanceToUnaggro = -1f;

        public Dictionary<int, int> levelExpCost = new()
        {
            {1, 100 },
            {2, 250 },
            {3, 500 }
        };

        public void Refill()
        {
            HP = maxHP;
            mana = maxMana;
            stamina = maxStamina;
            speed = maxSpeed;
            Poise = MaxPoise;
        }


        public void UpdateInvincibleFrames()
        {
            if(IsInvincible)
            {
                invincibleCounter++;
                if(invincibleCounter > invincibleFramesSec* Graphics.Graphics.UpdatesPerSecond)
                {
                    IsInvincible = false;
                    invincibleCounter = 0;
                }
            }
        }

        public void RegenStamina()
        {
            OnStaminaRegen = false;

            if(stamina < maxStamina && !OnUsingStamina)
            {
                staminaUnlockCounter++;

                if(staminaUnlockCounter < staminaUnlockSec*Graphics.Graphics.UpdatesPerSecond)
                {
                    OnStaminaRegen = true;
                }

                stamina+=staminaRegenSec/ (float)Graphics.Graphics.UpdatesPerSecond;

                if(GameStateManager.IsGod)
                {
                    stamina += maxStamina;
                }
            }
            else
            {
                staminaUnlockCounter = 0;
            }
        }

        public void ReceiveDamage(float amount)
        {
            HP -= amount;
        }

        public void ReceivePoiseDamage(float amount)
        {
            Poise -= 50;
        }

        public void UpdateFallen(Resources.Model model)
        {

            if (model.Body.Angle > 0.5f || model.Body.Angle < -0.5f || LostPoise())
            {
                if (!model.Body.IsColliding)
                {
                    if (!IsFallen)
                    {
                        IsFalling = true;
                        model.ModelState = Utils.ModelStates.FALLING;
                    }
                }
                else
                {
                    IsFallen = true;
                    IsFalling = false;
                    model.ModelState = Utils.ModelStates.FALLEN;
                }
            }

            if (IsFallen)
            {
                FallenTimer++;
                if (FallenTimer >= FallenDurationAllowedSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    IsFallen = false;
                    FallenTimer = 0f;
                    model.Body.Move(new FlatVector(0, 10f));
                    model.Body.RotateTo(0f);

                    //regen Poise
                    Poise = MaxPoise;
                }
            }
        }

        public void UpdateLedgeHanging(StatsEntity ent)
        {
            if(!AllowHangingOnLedge && ent.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
            {
                UnHangingCounter++;

                if (UnHangingCounter > 0.5f * Graphics.Graphics.UpdatesPerSecond)
                {
                    AllowHangingOnLedge = true;
                    UnHangingCounter = 0;
                }
            }

            if (IsTouchingWalls)
            {
                LedgeEntity ledge = CollisionHelper.GetAnyLedges(ent.Model.Body);
                if (ledge != null && AllowHangingOnLedge)
                {
                    ent.Model.ModelState = ModelStates.HANGING_ON_LEDGE;
                    ent.Model.Body.MoveTo(FlatConverter.ToFlatVector(ledge.HangingPosition));

                    HangingCounter++;
                    if(HangingCounter > 0.25f * Graphics.Graphics.UpdatesPerSecond)
                    {
                        AllowHangingOnLedge = false;
                        HangingCounter = 0;
                    }

                    //TODO FIX THE LEDGES DIRECTION SWAP
                    //if(ledge.Model.Direction == Directions.LEFT)
                    //{
                    //    Model.SwapDirection();
                    //}
                }
            }
        }


        public void UpdateDescending(StatsEntity ent)
        {
            IsGrounded = CollisionHelper.GetAnyGround(ent) != null;
            IsTouchingCeiling = CollisionHelper.GetAnyCeiling(ent) != null;
            IsTouchingWalls = CollisionHelper.GetAnyWalls(ent) != null;

            if(IsTouchingWalls)
            {
                IsTouchingCeiling = false;
                IsGrounded = false;
            }

            if (ent.Model.ModelState == ModelStates.JUMPING ||
                 ent.Model.ModelState == ModelStates.JUMPING_AND_MOVING ||
                 ent.Model.ModelState == ModelStates.JUMPING_DESCENDING ||
                 ent.Model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING)
            {
                IsJumpDescending = CollisionHelper.IsDescending(ent);
            }
            

            if (AllowJumpDescendingLock && IsJumpDescending)
            {
                DescendingCounter++;
                AllowJumpDescending = true;
                if (DescendingCounter > MaxDescendingSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    IsJumpDescending = false;
                    AllowJumpDescendingLock = false;
                    AllowJumpDescending = false;
                    DescendingCounter = 0;
                }
            }
            else
            {
                DescendingCounter = 0;
            }


            if(IsGrounded || IsTouchingCeiling)
            {
                IsJumpDescending = false;
                AllowJumpDescendingLock = false;
                AllowJumpDescending = false;
                DescendingCounter = 0;
            }

            // Reset highestJumpY when grounded
            if (ent.Stats.IsGrounded)
            {
                ent.highestJumpY = float.MinValue;
            }
        }

        public bool LostPoise()
        {
            return Poise <= 0;
        }
    }
}
