using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class StaminaRegenerationAbility : EntityAbility
    {

        public float StaminaRegenSec;
        public float StaminaUnlockSec;

        public int StaminaUnlockCounter = 0;

        public bool OnUsingStamina;
        public bool OnStaminaRegen;

        public StaminaRegenerationAbility(float staminaRegenSec, float staminaUnlockSec)
        {
            StaminaRegenSec = staminaRegenSec;
            StaminaUnlockSec = staminaUnlockSec;
            Type = EntityStatFeatures.STAMINA_REGENERATION;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            if (statsManager.StatsBattleHitSpendHandler != null && statsManager.StatsBattleHitSpendHandler.StatsPerAttackHitSpent)
                return;

            OnStaminaRegen = false;

            if (statsManager.GetStat(EntityStats.STAMINA).CurrentValue < statsManager.GetStat(EntityStats.STAMINA).MaximumValue && !OnUsingStamina)
            {
                StaminaUnlockCounter++;

                if (StaminaUnlockCounter < StaminaUnlockSec * Graphics.Graphics.UpdatesPerSecond)
                {
                    OnStaminaRegen = true;
                }

                statsManager.GetStat(EntityStats.STAMINA).CurrentValue += StaminaRegenSec / (float)Graphics.Graphics.UpdatesPerSecond;

                if (GameStateManager.IsGod)
                {
                    statsManager.GetStat(EntityStats.STAMINA).CurrentValue += statsManager.GetStat(EntityStats.STAMINA).MaximumValue;
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
