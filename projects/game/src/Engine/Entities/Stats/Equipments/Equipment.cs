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

        public Equipment(ItemLib.ItemTypes type, int value, string name, string description, ItemRarity rarity) : base(type, value, name, description, rarity)
        {

        }

        public virtual void Draw(Directions direction){}
    }
}
