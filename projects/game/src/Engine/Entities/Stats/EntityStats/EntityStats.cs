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

        public float maxEndurance;
        public float endurance;
        public float enduranceRegenSec;
        public float enduranceSprintCostSec;
        public float enduranceJumpCostSec;
        public float enduranceRollCostSec;
        public float enduranceAttackCost;
        public int enduranceUnlockCounter = 0;
        public float enduranceUnlockSec;

        public float maxSpeed;
        public float speed;
        public float jumpSpeed;
        public float rollMultiplier;
        public float sprintMultiplier;

        public bool OnEnduranceRegen = false;
        public bool OnUsingEndurance = false;

        public bool IsWeaponOut = false;
        

        public void Refill()
        {
            HP = maxHP;
            speed = maxSpeed;
            mana = maxMana;
            endurance = maxEndurance;
        }


        public void RegenEndurance()
        {
            OnEnduranceRegen = false;

            if(endurance < maxEndurance && !OnUsingEndurance)
            {
                enduranceUnlockCounter++;

                if(enduranceUnlockCounter < enduranceUnlockSec*60)
                {
                    OnEnduranceRegen = true;
                }

                endurance+=enduranceRegenSec/60;
            }
            else
            {
                enduranceUnlockCounter = 0;
            }
        }
    }
}
