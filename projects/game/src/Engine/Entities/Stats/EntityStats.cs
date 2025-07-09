using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool OnStaminaRegen = false;
        public bool OnUsingStamina = false;

        public float invincibleFramesSec = 1f;
        public int invincibleCounter = 0;
        public bool IsInvincible = true;
        

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
    }
}
