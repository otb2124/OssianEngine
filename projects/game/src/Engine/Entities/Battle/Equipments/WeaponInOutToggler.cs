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

        public void SetWeaponSwapPlaceHolder(WeaponHands hand, WeaponEquipment toChange)
        {
            if (hand == WeaponHands.LEFT)
                LeftWeaponIn = toChange;
            else
                RightWeaponIn = toChange;
        }

        public void SetCurrentWeaponSwapPlaceHolder(WeaponHands currentHand, WeaponEquipment toChange) =>
            SetWeaponSwapPlaceHolder(currentHand, toChange);

        public WeaponEquipment HandToWeaponIn(WeaponHands hand) =>
            hand == WeaponHands.LEFT ? LeftWeaponIn : RightWeaponIn;

        public void ToggleWeaponInOut(Equipments equipments, WeaponHands currentHand, BattleBodyManager manager)
        {
            var placeholder = HandToWeaponIn(currentHand);
            var slot = EquipmentHelper.HandToSlot(currentHand);
            var currentWeapon = (WeaponEquipment)equipments.GetEquipmentSlot(slot).Equipment;
            SetCurrentWeaponSwapPlaceHolder(currentHand, currentWeapon);
            equipments.SetEquipment(slot, placeholder);
            manager.HandToEquipmentWeaponBody(currentHand).Init(placeholder.WeaponBodyData);
            IsWeaponOut = !IsWeaponOut;
        }

        public void WeaponInSwap(Equipments equipments, WeaponHands currentHand, BattleBodyManager manager)
        {
            var placeholder = HandToWeaponIn(currentHand);
            var slot = EquipmentHelper.HandToSlot(currentHand);
            equipments.SetEquipment(slot, placeholder);
            manager.HandToEquipmentWeaponBody(currentHand).Init(placeholder.WeaponBodyData);
        }
    }
}
