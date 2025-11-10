using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public enum EntityStatFeatures
    {
        INVINCIBLE_FRAMES,
        GCS,
        STAMINA_REGENERATION,
        ITEM_PICKUP,
        DESCENCION,
        FALL,
        FLY,
        LEDGE_HANG,
        DOUBLE_JUMP,
        INWATER_WALKING,
        LADDER_CLIMBING,
        PRICK_INTO_SPIKE
    };


    public class EntityAbility
    {

        public EntityStatFeatures Type;

        public EntityAbility() { }

        public virtual void Update(StatsManager statsManager, Resources.Model model){ }
    }
}
