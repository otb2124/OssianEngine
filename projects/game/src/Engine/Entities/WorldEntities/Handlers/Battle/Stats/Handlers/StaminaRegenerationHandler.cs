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

        public void RegenStamina(IndicatorStats iStats)
        {
            OnStaminaRegen = false;

            if (iStats.Stamina < iStats.MaxStamina && !OnUsingStamina)
            {
                StaminaUnlockCounter++;

                if (StaminaUnlockCounter < StaminaUnlockSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    OnStaminaRegen = true;
                }

                iStats.Stamina += StaminaRegenSec / (float)Graphics.Graphics.UpdatesPerSecond;

                if (GameStateManager.IsGod)
                {
                    iStats.Stamina += iStats.MaxStamina;
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
