namespace Entities
{
    public class WeaponInOutToggler
    {
        public WeaponEquipment WeaponIn;
        public bool IsWeaponOut = false;

        public WeaponInOutToggler() { }

        public void SetWeaponPlaceholder(WeaponEquipment weapon)
        {
            WeaponIn = weapon;
        }

        public void ToggleWeaponInOut(Equipments equipments, BattleBodyManager manager)
        {
            WeaponEquipment placeholder = WeaponIn ?? EquipmentHelper.CreateBareHands();

            EquipmentSlot slot = equipments.TogglingWeaponSlot;
            WeaponEquipment current = (WeaponEquipment)slot.Equipment;

            SetWeaponPlaceholder(current);
            slot.Equipment = placeholder;

            manager.WeaponBody.Init(placeholder.WeaponBodyData);
            IsWeaponOut = !IsWeaponOut;
        }
    }
}