using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class DamageSet
    {
        public float PhysDamage;
        public float MagicDamage;

        public DamageSet(float physDamage, float magicDamage)
        {
            PhysDamage = physDamage;
            MagicDamage = magicDamage;
        }

        public DamageSet() { }
    }

    public class DefenseSet
    {
        public float PhysDef;
        public float MagicDef;

        public DefenseSet(float physDef, float magicDef)
        {
            PhysDef = physDef;
            MagicDef = magicDef;
        }

        public DefenseSet() { }
    }

    public class StatsCostSet
    {
        public float HPCost;
        public float StaminaCost;
        public float ManaCost;

        public StatsCostSet(float hpcost, float staminacost, float manacost)
        {
            HPCost = hpcost;
            StaminaCost = staminacost;
            ManaCost = manacost;
        }

        public StatsCostSet() { }
    }

    public class BattleItemStatsData
    {
        //TODO: add static stats effects like poison damage (if poison damage > poison def = add debuf poisoned)
        public DamageSet DamageSet;
        public DefenseSet DefenseSet;
        public StatsCostSet StatsCostSet;
        public float PoiseDamage;
        public float KnockbackPower;

        public BattleItemStatsData(DamageSet damageSet, DefenseSet defenseSet, StatsCostSet staminaCostSet, float poiseDamage, float knockBackPower)
        {
            DamageSet = damageSet;
            DefenseSet = defenseSet;
            StatsCostSet = staminaCostSet;
            PoiseDamage = poiseDamage;
            KnockbackPower = knockBackPower;
        }

        public BattleItemStatsData()
        {
            DamageSet = new DamageSet();
            DefenseSet = new DefenseSet();
            StatsCostSet = new StatsCostSet();
            PoiseDamage = 0f;
            KnockbackPower = 0f;
        }
    }



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
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.DamageSet.PhysDamage * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).PhysDamageMultiplier;
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
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.KnockbackPower * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).KnockbackPowerMultiplier;
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
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.PoiseDamage * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).PoiseDamageMultiplier;
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
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.StatsCostSet.StaminaCost * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).StaminaCostMultiplier;
            }

            return 0;
        }
    }
}
