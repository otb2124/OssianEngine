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
        public WeaponHand CurrentHand = WeaponHand.LEFT;

        public EquipmentManager() 
        {
            Equipments = new Equipments();
            WeaponInOutToggler = new WeaponInOutToggler();
        }

        public WeaponEquipment GetCurrentWeapon() =>
           (WeaponEquipment)EquipmentHelper.GetEquipmentSlot(GetCurrentWeaponSlot(), Equipments.EquipmentSlots).Equipment;

        public EquipmetWeaponBody GetCurrentWeaponBody(EquipmenBattleBodyManager manager) =>
            manager.HandToEquipmentWeaponBody(CurrentHand);

        public EquipmentSlot.EquipmentSlots GetCurrentWeaponSlot() =>
           EquipmentHelper.HandToSlot(CurrentHand);


        public void SetWeapon(EquipmenBattleBodyManager manager, WeaponEquipment weapon)
        {
            WeaponInOutToggler.SetWeaponSwapPlaceHolder(CurrentHand, weapon);
            Equipments.SetEquipment(EquipmentHelper.HandToSlot(CurrentHand), EquipmentHelper.CreateBareHands());
            manager.HandToEquipmentWeaponBody(CurrentHand).Init(EquipmentHelper.CreateBareHands().WeaponBodyData);
        }

        public void ToggleWeaponInOut(EquipmenBattleBodyManager manager)
        {
            WeaponInOutToggler.ToggleWeaponInOut(Equipments, CurrentHand, manager);
        }
    }
}
