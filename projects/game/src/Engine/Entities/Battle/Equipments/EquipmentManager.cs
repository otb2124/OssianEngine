using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EquipmentManager
    {

        public Equipments Equipments;
        public WeaponInOutToggler WeaponInOutToggler;

        //TODO: ADD CURRENTHANDSWAPPER OR SMTH
        public WeaponHands CurrentHand = WeaponHands.LEFT;

        public EquipmentManager() 
        {
            Equipments = new Equipments();
            WeaponInOutToggler = new WeaponInOutToggler();
        }

        public WeaponEquipment GetCurrentWeapon()
        {
            var currentSlot = GetCurrentWeaponSlot();
            var equipment = EquipmentHelper.GetEquipmentSlot(currentSlot, Equipments.TogglingWeaponSlots).Equipment;
            if (equipment == null)
            {
                return WeaponInOutToggler.HandToWeaponIn(CurrentHand);
            }
            return (WeaponEquipment)equipment;
        }

        public BattleBody GetCurrentWeaponBody(BattleBodyManager manager) =>
            manager.HandToEquipmentWeaponBody(CurrentHand);

        public EquipmentSlot.EquipmentSlotTypes GetCurrentWeaponSlot() =>
           EquipmentHelper.HandToSlot(CurrentHand);


        public void SetWeapon(BattleBodyManager manager, WeaponEquipment weapon, WeaponHands hand)
        {
            WeaponInOutToggler.SetWeaponSwapPlaceHolder(hand, weapon);
            Equipments.SetEquipment(EquipmentHelper.HandToSlot(hand), weapon);
            Equipments.SetTogglingWeaponEquipment(EquipmentHelper.HandToSlot(hand), EquipmentHelper.CreateBareHands());
            manager.HandToEquipmentWeaponBody(hand).Init(EquipmentHelper.CreateBareHands().WeaponBodyData);
        }

        public void ToggleWeaponInOut(BattleBodyManager manager)
        {
            WeaponInOutToggler.ToggleWeaponInOut(Equipments, CurrentHand, manager);
        }
    }
}
