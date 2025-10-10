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
        LEDGE_HANG
    };


    public class EntityStatFeature
    {

        public EntityStatFeatures Type;

        public EntityStatFeature() { }

        public virtual void Update(StatsManager statsManager, Resources.Model model){ }
    }
}
