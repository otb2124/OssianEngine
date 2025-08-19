using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class EquipmentHelper
    {


        public static EquipmentSlot.EquipmentSlots HandToSlot(WeaponHand hand) =>
            hand == WeaponHand.LEFT ? EquipmentSlot.EquipmentSlots.WEAPON_L : EquipmentSlot.EquipmentSlots.WEAPON_R;

        public static WeaponEquipment CreateBareHands() =>
            (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.BARE_HAND));
    }
}
