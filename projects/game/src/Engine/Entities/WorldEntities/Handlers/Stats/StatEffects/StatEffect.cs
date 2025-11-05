using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public enum StatEffects
    {
        POISONED,
        IN_WATER,
    };

    public class StatEffect
    {
        public StatEffects Type;

        public Dictionary<EntityStats, float> EntityStatAffection;

        public float CurrentDuration;
        public float MaxDuration;

        public bool IsAffectMultiplying;

        public float IntensivitySec;

        public bool ApplyOnce;

        private uint Counter;

        public StatEffect(StatEffects type, Dictionary<EntityStats, float> entityStatAffection, float maxDuration, bool isAffectMultiplying, bool applyOnce = true, float intensivitySec = 0)
        {
            Type = type;
            EntityStatAffection = entityStatAffection;
            MaxDuration = (uint)(maxDuration * Graphics.Graphics.UpdatesPerSecond);
            CurrentDuration = MaxDuration;
            IntensivitySec = (uint)(intensivitySec * Graphics.Graphics.UpdatesPerSecond);
            IsAffectMultiplying = isAffectMultiplying;
            ApplyOnce = applyOnce;
        }

        public void Update(EntityStat[] stats)
        {
            if (CurrentDuration == 0) return;
            CurrentDuration--;

            if (ApplyOnce)
            {
                if (CurrentDuration == MaxDuration - 1)
                    Apply(stats);
                return;
            }

            if (IntensivitySec == 0)
            {
                Apply(stats);
                return;
            }

            Counter++;
            if (Counter >= IntensivitySec)
            {
                Counter = 0;
                Apply(stats);
            }
        }

        private void Apply(EntityStat[] stats)
        {
            foreach (var kvp in EntityStatAffection)
            {
                EntityStats target = kvp.Key;
                float value = kvp.Value;

                EntityStat stat = stats.FirstOrDefault(s => s.Type == target);
                if (stat == null) continue;

                if (IsAffectMultiplying)
                    stat.CurrentValue *= value;
                else
                    stat.CurrentValue += value;

                stat.CurrentValue = MathHelper.Clamp(stat.CurrentValue, 0f, stat.MaximumValue);
            }
        }

        public static Dictionary<StatEffects, StatEffect> StatEffectMap = new()
        {
            { StatEffects.POISONED, new StatEffect(StatEffects.POISONED, new Dictionary<EntityStats, float> { { EntityStats.HP, -10f } }, 5f, false, false, 1f) }
        };
    }
}
