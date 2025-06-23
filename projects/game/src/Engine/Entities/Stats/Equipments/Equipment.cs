using System;
using Utils;
using static Entities.PhysicalEntity;


namespace Entities
{
    public class Equipment : Item
    {

        public float swingSpeed;
        public float physDmg;
        public float currentSwingTime = 0f;
        public bool isSwinging = false;

        public float physDef;

        public Equipment() : base(Items.SWORD, 1, "Equipment", "desc", ItemRarity.COMMON)
        {

        }

        public virtual void Draw(Directions direction){}
    }
}
