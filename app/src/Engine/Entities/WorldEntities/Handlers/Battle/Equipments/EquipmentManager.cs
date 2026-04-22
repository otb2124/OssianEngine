using Resources;

namespace Entities
{
    public class EquipmentManager
    {
        public Equipments Equipments;
        public WeaponInOutToggler WeaponInOutToggler;

        public EquipmentManager()
        {
            Equipments = new Equipments();
            WeaponInOutToggler = new WeaponInOutToggler();
        }

        public WeaponEquipment GetCurrentWeapon()
        {
            var toggling = Equipments.TogglingWeaponSlot.Equipment;
            return toggling != null
                ? (WeaponEquipment)toggling
                : WeaponInOutToggler.WeaponIn ?? EquipmentHelper.CreateBareHands();
        }

        public BattleBody GetCurrentWeaponBody(BattleBodyManager manager) =>
            manager.WeaponBody;

        public EquipmentSlot.EquipmentSlots GetCurrentWeaponSlot() =>
            EquipmentSlot.EquipmentSlots.WEAPON;

        public void SetWeapon(BattleBodyManager manager, WeaponEquipment weapon)
        {
            WeaponInOutToggler.SetWeaponPlaceholder(weapon);
            Equipments.SetEquipment(EquipmentSlot.EquipmentSlots.WEAPON, weapon);
            Equipments.TogglingWeaponSlot.Equipment = EquipmentHelper.CreateBareHands();
            manager.WeaponBody.Init(EquipmentHelper.CreateBareHands().WeaponBodyData);
        }

        public void ToggleWeaponInOut(BattleBodyManager manager)
        {
            WeaponInOutToggler.ToggleWeaponInOut(Equipments, manager);
        }
    }
}