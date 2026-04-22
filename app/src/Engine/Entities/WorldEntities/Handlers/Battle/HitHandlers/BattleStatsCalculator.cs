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
                return nhA.StatsManager.BodyHitStatsSet.DamageSet.PhysDamage * nhA.BattleBodyManager.GetCurrentBattleHitData().DamageSet.PhysDamage;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.DamageSet.PhysDamage * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).DamageSet.PhysDamage;
            }

            return 0;
        }

        //TODO: REPLACE THE BATTLEHITDATA ATTRIBUTES WITH DAMAGEDATA OBJECT
        public static float GetFinalMagicDamageForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.StatsManager.BodyHitStatsSet.DamageSet.MagicDamage * nhA.BattleBodyManager.GetCurrentBattleHitData().DamageSet.MagicDamage;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.DamageSet.MagicDamage * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).DamageSet.MagicDamage;
            }

            return 0;
        }

        public static float GetFinalKnockbackPowerForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.StatsManager.BodyHitStatsSet.KnockbackPower * nhA.BattleBodyManager.GetCurrentBattleHitData().KnockbackPower;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.KnockbackPower * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).KnockbackPower;
            }

            return 0;
        }

        public static float GetFinalPoiseDamageForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.StatsManager.BodyHitStatsSet.PoiseDamage * nhA.BattleBodyManager.GetCurrentBattleHitData().PoiseDamage;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.PoiseDamage * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).PoiseDamage;
            }

            return 0;
        }


        public static float GetFinalStaminaPerHitCostForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.StatsManager.BodyHitStatsSet.StatsCostSet.StaminaCost * nhA.BattleBodyManager.GetCurrentBattleHitData().StatsCostSet.StaminaCost;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.StatsCostSet.StaminaCost * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).StatsCostSet.StaminaCost;
            }

            return 0;
        }


        //TODO: REPLACE THE BATTLEHITDATA ATTRIBUTES WITH DAMAGEDATA OBJECT
        public static float GetFinalManaPerHitCostForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.StatsManager.BodyHitStatsSet.StatsCostSet.ManaCost * nhA.BattleBodyManager.GetCurrentBattleHitData().StatsCostSet.ManaCost;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.StatsCostSet.ManaCost * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).StatsCostSet.ManaCost;
            }

            return 0;
        }
    }
}
