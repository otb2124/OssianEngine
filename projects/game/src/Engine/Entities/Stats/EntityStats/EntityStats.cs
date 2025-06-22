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

        public float maxSpeed;
        public float speed;

        public float maxMana;
        public float mana;

        public float maxEndurance;
        public float endurance;


        public void Refill()
        {
            HP = maxHP;
            speed = maxSpeed;
            mana = maxMana;
            endurance = maxEndurance;
        }
    }
}
