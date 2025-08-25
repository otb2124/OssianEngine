using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class BattleStatsCalculator
    {


        public static float GetFinalPhysDamageForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.Stats.BodyDamage * nhA.BattleBodyManager.GetCurrentBattleHitData().PhysDamageMultiplier;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().PhysDmg * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).PhysDamageMultiplier;
            }

            return 0;
        }

        public static float GetFinalKnockbackPowerForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.Stats.BodyKnockbackPower * nhA.BattleBodyManager.GetCurrentBattleHitData().KnockbackPowerMultiplier;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().KnockbackPower * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).KnockbackPowerMultiplier;
            }

            return 0;
        }

        public static float GetFinalPoiseDamageForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.Stats.BodyPoiseDamage * nhA.BattleBodyManager.GetCurrentBattleHitData().PoiseDamageMultiplier;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().PoiseDmg * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).PoiseDamageMultiplier;
            }

            return 0;
        }


        public static float GetFinalStaminaPerHitCostForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.Stats.BodyStaminaHitCost * nhA.BattleBodyManager.GetCurrentBattleHitData().StaminaCostMultiplier;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().StaminaCostPerHit * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).StaminaCostMultiplier;
            }

            return 0;
        }
    }
}
