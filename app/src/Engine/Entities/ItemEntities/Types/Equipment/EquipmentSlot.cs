namespace Entities
{
    public class EquipmentSlot
    {
        public enum EquipmentSlots
        {
            NONE,

            WEAPON,

            TORSO,
            HEAD,
            HANDS,
            LEGS,

            NECKLACE,
            CAPE,
            BELT,
            RING_0,
            RING_1,

            PET_0,
            PET_1,
            CONTAINMENT
        }

        public Equipment Equipment;
        public EquipmentSlots EquipmentSlotType;

        public EquipmentSlot(EquipmentSlots type)
        {
            EquipmentSlotType = type;
        }
    }
}