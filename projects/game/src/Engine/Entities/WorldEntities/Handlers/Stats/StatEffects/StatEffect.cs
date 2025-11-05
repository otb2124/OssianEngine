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
        FAST_LEGS,
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
        private bool WasAppliedOnce = false;

        public bool RestoresToDefault;

        private uint Counter;

        public StatEffect(StatEffects type, Dictionary<EntityStats, float> entityStatAffection, float maxDuration, bool isAffectMultiplying, bool applyOnce = true, float intensivitySec = 0, bool restoresToDefault = false)
        {
            Type = type;
            EntityStatAffection = entityStatAffection;
            MaxDuration = (uint)(maxDuration * Graphics.Graphics.UpdatesPerSecond);
            CurrentDuration = MaxDuration;
            IntensivitySec = (uint)(intensivitySec * Graphics.Graphics.UpdatesPerSecond);
            IsAffectMultiplying = isAffectMultiplying;
            ApplyOnce = applyOnce;
            RestoresToDefault = restoresToDefault;
        }

        public void Update(EntityStat[] stats)
        {
            if (CurrentDuration == 0) return;

            bool isLastTick = (CurrentDuration == 1);

            CurrentDuration--;

            if (RestoresToDefault && isLastTick)
            {
                foreach (var kvp in EntityStatAffection)
                {
                    EntityStat s = stats.FirstOrDefault(x => x.Type == kvp.Key);
                    if (s != null) s.CurrentValue = s.MaximumValue;
                }
                return;
            }

            if (IntensivitySec == 0)
            {
                if ((ApplyOnce && !WasAppliedOnce) || !ApplyOnce)
                {
                    Apply(stats);
                    WasAppliedOnce = true;
                }
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
            }
        }

        public static Dictionary<StatEffects, StatEffect> StatEffectMap = new()
        {
            { StatEffects.POISONED, new StatEffect(StatEffects.POISONED, new Dictionary<EntityStats, float> { { EntityStats.HP, -10f } }, 5f, false, false, 1f) },
            { StatEffects.FAST_LEGS, new StatEffect(StatEffects.FAST_LEGS, new Dictionary<EntityStats, float> { { EntityStats.MOVEMENT_SPEED, 2f } }, 10f, true, true, 0f, true) }
        };
    }
}
