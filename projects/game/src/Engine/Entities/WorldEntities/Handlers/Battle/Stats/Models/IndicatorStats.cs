using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class IndicatorStats
    {
        //indicators
        public float MaxHP;
        public float HP;

        public float MaxMana;
        public float Mana;

        public float MaxStamina;
        public float Stamina;
        

        public IndicatorStats(float maxHP, float maxMana, float maxStamina)
        {
            MaxHP = maxHP;
            MaxMana = maxMana;
            MaxStamina = maxStamina;
        }

        public void Refill()
        {
            HP = MaxHP;
            Mana = MaxMana;
            Stamina = MaxStamina;
        }
    }
}
