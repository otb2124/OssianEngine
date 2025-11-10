using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public enum Spikes
    {
        FLOOR_SPIKE_0,
        FLOOR_SPIKE_1,
        FLOOR_SPIKE_2,
        FLOOR_SPIKE_3,
        FLOOR_SPIKE_4,
    };


    public class SpikeEntity : PhysicalEntity
    {

        public Spikes Type;

        public SpikeEntity(Spikes type, Vector2 pos, float rot = 0) : base()
        {
            Type = type;
            Init(StaticSpriteFactory.SpikeSetCut(type), GetPhysicalBodyPreset(Type), pos, rot);
        }


        public static PhysicalBodies GetPhysicalBodyPreset(Spikes type)
        {
            switch(type)
            {
                case Spikes.FLOOR_SPIKE_0:
                case Spikes.FLOOR_SPIKE_1:
                    return PhysicalBodies.SPIKE_S;
                case Spikes.FLOOR_SPIKE_2:
                    return PhysicalBodies.SPIKE_L;
                case Spikes.FLOOR_SPIKE_3:
                case Spikes.FLOOR_SPIKE_4:
                    return PhysicalBodies.SPIKE_XL;
            }

            return PhysicalBodies.SPIKE_S;
        }
    }
}
