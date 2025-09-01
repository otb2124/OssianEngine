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

        public static DamageSet One;
        public static DamageSet Zero;

        static DamageSet()
        {
            One = new DamageSet(1, 1);
            Zero = new DamageSet(0, 0);
        }
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

        public static DefenseSet One;
        public static DefenseSet Zero;

        static DefenseSet()
        {
            One = new DefenseSet(1, 1);
            Zero = new DefenseSet(0, 0);
        }
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

        public static StatsCostSet One;
        public static StatsCostSet Zero;

        static StatsCostSet()
        {
            One = new StatsCostSet(1, 1, 1);
            Zero = new StatsCostSet(0, 0, 0);
        }
    }

    public class BattleHitStatsData
    {
        //TODO: add static stats effects like poison damage (if poison damage > poison def = add debuf poisoned)
        public DamageSet DamageSet;
        public DefenseSet DefenseSet;
        public StatsCostSet StatsCostSet;
        public float PoiseDamage;
        public float KnockbackPower;

        public BattleHitStatsData(DamageSet damageSet, DefenseSet defenseSet, StatsCostSet staminaCostSet, float poiseDamage, float knockBackPower)
        {
            DamageSet = damageSet;
            DefenseSet = defenseSet;
            StatsCostSet = staminaCostSet;
            PoiseDamage = poiseDamage;
            KnockbackPower = knockBackPower;
        }

        public static BattleHitStatsData One;
        public static BattleHitStatsData Zero;

        static BattleHitStatsData()
        {
            One = new BattleHitStatsData(DamageSet.One, DefenseSet.One, StatsCostSet.One, 1, 1);
            Zero = new BattleHitStatsData(DamageSet.Zero, DefenseSet.Zero, StatsCostSet.Zero, 0, 0);
        }

    }



    public static class BattleStatsCalculator
    {


        public static float GetFinalPhysDamageForBattleEntity(BattleEntity ent)
        {
            if (ent is NonEquipmentEntity nhA)
            {
                return nhA.Stats.BodyPhysDamage * nhA.BattleBodyManager.GetCurrentBattleHitData().DamageSet.PhysDamage;
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
                return nhA.Stats.BodyMagicDamage * nhA.BattleBodyManager.GetCurrentBattleHitData().DamageSet.MagicDamage;
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
                return nhA.Stats.BodyKnockbackPower * nhA.BattleBodyManager.GetCurrentBattleHitData().KnockbackPower;
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
                return nhA.Stats.BodyPoiseDamage * nhA.BattleBodyManager.GetCurrentBattleHitData().PoiseDamage;
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
                return nhA.Stats.BodyStaminaHitCost * nhA.BattleBodyManager.GetCurrentBattleHitData().StatsCostSet.StaminaCost;
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
                return nhA.Stats.BodyManaHitCost * nhA.BattleBodyManager.GetCurrentBattleHitData().StatsCostSet.ManaCost;
            }
            else if (ent is EquipmentEntity eqA)
            {
                return eqA.EquipmentManager.GetCurrentWeapon().BattleItemStatsData.StatsCostSet.ManaCost * eqA.BattleBodyManager.GetCurrentBattleHitData(eqA.EquipmentManager).StatsCostSet.ManaCost;
            }

            return 0;
        }
    }
}
