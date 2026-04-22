using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public enum EntityStats
    {
        HP,
        MANA,
        STAMINA,
        MOVEMENT_SPEED,
        JUMP_SPEED,
        SPRINT_SPEED_MULTIPLIER,
        ROLL_SPEED_MULTIPLIER,
        POISE,
        AGGRO_RANGE,
        UNAGGRO_RANGE,
        FLY_SPEED
    };


    public class EntityStat
    {

        public int Id;
        public EntityStats Type;

        public float CurrentValue;
        public float MaximumValue;

        public float StaminaDependencySec;

        public EntityStat(EntityStats type, float currentValue, float maximumValue, float staminaCostSec = 0)
        {
            Type = type;
            CurrentValue = currentValue;
            MaximumValue = maximumValue;
            StaminaDependencySec = staminaCostSec;
        }

        public void Refill()
        {
            CurrentValue = MaximumValue;
        }

        public void ModifyCurrent(float value)
        {
            CurrentValue -= value;
        }

        public bool LessEquealZero()
        {
            return CurrentValue <= 0;
        }
    }
}
