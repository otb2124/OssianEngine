using Physics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Entities
{
    public class EntityStats
    {

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
        public float staminaAttackCost;
        public int staminaUnlockCounter = 0;
        public float staminaUnlockSec;

        public float maxSpeed;
        public float speed;
        public float jumpSpeed;
        public float rollMultiplier;
        public float sprintMultiplier;

        public float bodyDamage;
        public float bodyKnockbackPower;

        public bool OnStaminaRegen = false;
        public bool OnUsingStamina = false;

        public float invincibleFramesSec = 1f;
        public int invincibleCounter = 0;
        public bool IsInvincible = true;

        public bool IsFallen = false;
        public float FallenTimer = 0f;
        public float FallenDurationAllowedSec = 3f;

        public void Refill()
        {
            HP = maxHP;
            speed = maxSpeed;
            mana = maxMana;
            stamina = maxStamina;
        }


        public void UpdateInvincibleFrames()
        {
            if(IsInvincible)
            {
                invincibleCounter++;
                if(invincibleCounter > invincibleFramesSec*60)
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

                if(staminaUnlockCounter < staminaUnlockSec*60)
                {
                    OnStaminaRegen = true;
                }

                stamina+=staminaRegenSec/60;
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


        public void UpdateFallen(FlatBody body)
        {
            Console.WriteLine(body.Angle);

            if (!IsFallen && (body.Angle > 3f || body.Angle < -3f) && body.IsColliding)
            {
                IsFallen = true;
            }

            if (IsFallen)
            {
                FallenTimer++;
                if (FallenTimer >= FallenDurationAllowedSec*60)
                {
                    IsFallen = false;
                    FallenTimer = 0f;
                    body.Move(new FlatVector(0, 10f));
                    body.RotateTo(0f);
                }
            }
        }
    }
}
