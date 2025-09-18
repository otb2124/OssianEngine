using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EquipmentSlot
    {

        public enum EquipmentSlotTypes
        {
            NONE,

            WEAPON_L,
            WEAPON_R,
            
            CHESTPLATE,
            HELMET,
            BOOTS,
            GLOVES,

            NECKLACE,
            CAPE,
            BELT,
            RING_L,
            RING_R,

            PET,
            PET_LIGHT,
            CONTAINMENT
        }

        public Equipment Equipment;
        public EquipmentSlotTypes EquipmentSlotType;

        public EquipmentSlot(EquipmentSlotTypes type)
        {
            EquipmentSlotType = type;
        }
    }
}
