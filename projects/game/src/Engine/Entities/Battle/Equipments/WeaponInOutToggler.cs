using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class WeaponInOutToggler
    {

        public WeaponEquipment LeftWeaponIn;
        public WeaponEquipment RightWeaponIn;
        public bool IsWeaponOut = false;

        public WeaponInOutToggler()
        {
        }

        public void SetWeaponSwapPlaceHolder(WeaponHand hand, WeaponEquipment toChange)
        {
            if (hand == WeaponHand.LEFT)
                LeftWeaponIn = toChange;
            else
                RightWeaponIn = toChange;
        }

        public void SetCurrentWeaponSwapPlaceHolder(WeaponHand currentHand, WeaponEquipment toChange) =>
            SetWeaponSwapPlaceHolder(currentHand, toChange);

        public WeaponEquipment HandToWeaponIn(WeaponHand hand) =>
            hand == WeaponHand.LEFT ? LeftWeaponIn : RightWeaponIn;

        public void ToggleWeaponInOut(Equipments equipments, WeaponHand currentHand, EquipmenBattleBodyManager manager)
        {
            var placeholder = HandToWeaponIn(currentHand);
            var slot = EquipmentHelper.HandToSlot(currentHand);
            var currentWeapon = (WeaponEquipment)equipments.GetEquipmentSlot(slot).Equipment;
            SetCurrentWeaponSwapPlaceHolder(currentHand, currentWeapon);
            equipments.SetEquipment(slot, placeholder);
            manager.HandToEquipmentWeaponBody(currentHand).Init(placeholder.WeaponBodyData);
        }

        public void WeaponInSwap(Equipments equipments, WeaponHand currentHand, EquipmenBattleBodyManager manager)
        {
            var placeholder = HandToWeaponIn(currentHand);
            var slot = EquipmentHelper.HandToSlot(currentHand);
            equipments.SetEquipment(slot, placeholder);
            manager.HandToEquipmentWeaponBody(currentHand).Init(placeholder.WeaponBodyData);
        }
    }
}
