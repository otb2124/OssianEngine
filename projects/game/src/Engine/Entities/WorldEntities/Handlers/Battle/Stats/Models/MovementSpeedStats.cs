using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MovementSpeedStats
    {

        public float MaxMovementSpeed;
        public float MovementSpeed;

        public MovementSpeedStats(float maxMovementSpeed)
        {
            MaxMovementSpeed = maxMovementSpeed;
            MovementSpeed = maxMovementSpeed;
        }


        public void Refill()
        {
            MovementSpeed = MaxMovementSpeed;
        }
    }
}
