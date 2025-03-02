using System;
using Utils;
using static Entities.PhysicalEntity;


namespace Entities
{
    public class EquipmentEntity
    {

        public float swingSpeed;
        public float physDmg;
        public float currentSwingTime = 0f;
        public bool isSwinging = false;

        public float physDef;


        public virtual void Draw(Directions direction)
        {

        }
    }
}
