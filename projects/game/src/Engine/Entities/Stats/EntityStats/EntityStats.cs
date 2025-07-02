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

        public bool OnStaminaRegen = false;
        public bool OnUsingStamina = false;

        public bool IsWeaponOut = false;
        

        public void Refill()
        {
            HP = maxHP;
            speed = maxSpeed;
            mana = maxMana;
            stamina = maxStamina;
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
    }
}
