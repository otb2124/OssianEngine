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

    public class BattleHitStatsSet
    {
        //TODO: add static stats effects like poison damage (if poison damage > poison def = add debuf poisoned)
        public DamageSet DamageSet;
        public DefenseSet DefenseSet;
        public StatsCostSet StatsCostSet;
        public float PoiseDamage;
        public float KnockbackPower;

        public BattleHitStatsSet(DamageSet damageSet, DefenseSet defenseSet, StatsCostSet staminaCostSet, float poiseDamage, float knockBackPower)
        {
            DamageSet = damageSet;
            DefenseSet = defenseSet;
            StatsCostSet = staminaCostSet;
            PoiseDamage = poiseDamage;
            KnockbackPower = knockBackPower;
        }

        public BattleHitStatsSet()
        {
            DamageSet = new DamageSet(0, 0);
            DefenseSet = new DefenseSet(0, 0);
            StatsCostSet = new StatsCostSet(0, 0, 0);
            PoiseDamage = 0f;
            KnockbackPower = 0f;
        }

        public static BattleHitStatsSet One;
        public static BattleHitStatsSet Zero;

        static BattleHitStatsSet()
        {
            One = new BattleHitStatsSet(DamageSet.One, DefenseSet.One, StatsCostSet.One, 1, 1);
            Zero = new BattleHitStatsSet(DamageSet.Zero, DefenseSet.Zero, StatsCostSet.Zero, 0, 0);
        }

    }
}
