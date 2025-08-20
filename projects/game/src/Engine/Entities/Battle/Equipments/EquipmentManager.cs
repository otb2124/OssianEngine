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


        public EquipmentSlot.EquipmentSlots GetCurrentWeaponSlot() =>
           EquipmentHelper.HandToSlot(CurrentHand);


        public void SetWeapon(EquipmentWeaponBodyManager manager, WeaponEquipment weapon)
        {
            WeaponInOutToggler.SetWeaponSwapPlaceHolder(CurrentHand, weapon);
            Equipments.SetEquipment(EquipmentHelper.HandToSlot(CurrentHand), EquipmentHelper.CreateBareHands());
            manager.HandToEquipmentWeaponBody(CurrentHand).Init(EquipmentHelper.CreateBareHands().WeaponBodyData);
        }


        public EquipmetWeaponBody GetCurrentWeaponBody(EquipmentWeaponBodyManager manager) =>
            manager.HandToEquipmentWeaponBody(CurrentHand);


        public void Draw(Model model, EquipmentWeaponBodyManager manager) =>
            GetCurrentWeaponBody(manager)?.Draw(model);

        public void DrawHitbox(EquipmentWeaponBodyManager manager)
        {
            GetCurrentWeaponBody(manager).DrawHitbox();
            Equipments.GetCurrentArmor()?.DrawHitbox();
        }



        public void ToggleWeaponInOut(EquipmentWeaponBodyManager manager)
        {
            WeaponInOutToggler.ToggleWeaponInOut(Equipments, CurrentHand, manager);
        }
    }
}
