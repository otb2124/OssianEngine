using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class StaminaRegenerationHandler
    {

        public float StaminaRegenSec;
        public float StaminaUnlockSec;

        public int StaminaUnlockCounter = 0;
        public bool OnStaminaRegen = false;
        public bool OnUsingStamina = false;

        public StaminaRegenerationHandler(float staminaRegenSec, float staminaUnlockSec)
        {
            StaminaRegenSec = staminaRegenSec;
            StaminaUnlockSec = staminaUnlockSec;
        }

        public void Update(EntityStat stamina, StatsBattleHitSpendHandler statsBattleHitSpendHandler)
        {
            if (statsBattleHitSpendHandler != null && statsBattleHitSpendHandler.StatsPerAttackHitSpent)
                return;

            OnStaminaRegen = false;

            if (stamina.CurrentValue < stamina.MaximumValue && !OnUsingStamina)
            {
                StaminaUnlockCounter++;

                if (StaminaUnlockCounter < StaminaUnlockSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    OnStaminaRegen = true;
                }

                stamina.CurrentValue += StaminaRegenSec / (float)Graphics.Graphics.UpdatesPerSecond;

                if (GameStateManager.IsGod)
                {
                    stamina.CurrentValue += stamina.MaximumValue;
                }
            }
            else
            {
                StaminaUnlockCounter = 0;
            }

            OnUsingStamina = false;
        }
    }
}
