using System;
using Utils;
using static Entities.PhysicalEntity;


namespace Entities
{
    public class Equipment : Item
    {

        public enum EquipmentSlotsTake 
        {
            WEAPON_SINGLE,
            WEAPON_DOUBLE,

            TORSO,
            HEAD,
            LEGS,
            HANDS,

            NECKLACE,
            CAPE,
            BELT,
            RING_L,
            RING_R,

            PET,
            PET_LIGHT,
            CONTAINMENT,
        }

        public EquipmentSlotsTake EquipmentSlot;

        public float PhysDmg;
        public float PhysDef;

        public Equipment(ItemKey itemKey) : base(itemKey)
        {

        }

        public virtual void Draw(Directions direction){}
    }
}
