using Resources;
using System;
using UI;

namespace Entities
{

    public class Equipments
    {

        public WeaponHand CurrentHand = WeaponHand.LEFT;

        public WeaponEquipment LeftWeaponIn;
        public WeaponEquipment RightWeaponIn;
        public bool IsWeaponOut = false;

        public EquipmentSlot[] Slots;

        public Equipments()
        {
            Slots = new EquipmentSlot[]
            {
                new(EquipmentSlot.EquipmentSlots.WEAPON_L),
                new(EquipmentSlot.EquipmentSlots.WEAPON_R),
                new(EquipmentSlot.EquipmentSlots.CHESTPLATE),
                new(EquipmentSlot.EquipmentSlots.HELMET),
                new(EquipmentSlot.EquipmentSlots.BOOTS),
                new(EquipmentSlot.EquipmentSlots.GLOVES),
                new(EquipmentSlot.EquipmentSlots.NECKLACE),
                new(EquipmentSlot.EquipmentSlots.CAPE),
                new(EquipmentSlot.EquipmentSlots.BELT),
                new(EquipmentSlot.EquipmentSlots.RING_L),
                new(EquipmentSlot.EquipmentSlots.RING_R),
                new(EquipmentSlot.EquipmentSlots.PET),
                new(EquipmentSlot.EquipmentSlots.PET_LIGHT),
                new(EquipmentSlot.EquipmentSlots.CONTAINMENT)
            };
        }


        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlots type) =>
            Array.Find(Slots, slot => slot.Type == type);

        public void SetEquipmentSlot(EquipmentSlot.EquipmentSlots slotType, Equipment item) =>
            GetEquipmentSlot(slotType).Equipment = item;


        public ArmorEquipment GetCurrentArmor() =>
            (ArmorEquipment)GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment;

        public WeaponEquipment GetCurrentWeapon() =>
            (WeaponEquipment)GetEquipmentSlot(GetCurrentWeaponSlot()).Equipment;


        public EquipmentSlot.EquipmentSlots GetCurrentWeaponSlot() =>
           EquipmentHelper.HandToSlot(CurrentHand);


        public void SetWeapon(EquipmentWeaponBodyManager manager, WeaponHand hand, WeaponEquipment weapon)
        {
            SetWeaponSwapPlaceHolder(hand, weapon);
            SetEquipmentSlot(EquipmentHelper.HandToSlot(hand), EquipmentHelper.CreateBareHands());
            manager.HandToEquipmentWeaponBody(CurrentHand).Init(EquipmentHelper.CreateBareHands().WeaponBodyData);
        }


        public WeaponEquipment GetCurrentWeaponSwapPlaceHolder() =>
            HandToWeaponIn(CurrentHand);


        public EquipmetWeaponBody GetCurrentWeaponBody(EquipmentWeaponBodyManager manager) =>
            manager.HandToEquipmentWeaponBody(CurrentHand);


        public void SetCurrentWeaponSwapPlaceHolder(WeaponEquipment toChange) =>
            SetWeaponSwapPlaceHolder(CurrentHand, toChange);
        public void SetWeaponSwapPlaceHolder(WeaponHand hand, WeaponEquipment toChange)
        {
            if (hand == WeaponHand.LEFT)
                LeftWeaponIn = toChange;
            else
                RightWeaponIn = toChange;
        }

        public void WeaponOutSwap(EquipmentWeaponBodyManager manager)
        {
            var slot = GetCurrentWeaponSlot();
            var currentWeapon = (WeaponEquipment)GetEquipmentSlot(slot).Equipment;
            var placeholder = GetCurrentWeaponSwapPlaceHolder();
            SetCurrentWeaponSwapPlaceHolder(currentWeapon);
            SetEquipmentSlot(slot, placeholder);
            manager.HandToEquipmentWeaponBody(CurrentHand).Init(placeholder.WeaponBodyData);
        }


        public WeaponEquipment HandToWeaponIn(WeaponHand hand) =>
           hand == WeaponHand.LEFT ? LeftWeaponIn : RightWeaponIn;



        public void Draw(Model model, EquipmentWeaponBodyManager manager) =>
            GetCurrentWeaponBody(manager)?.Draw(model);

        public void DrawHitbox(EquipmentWeaponBodyManager manager)
        {
            GetCurrentWeaponBody(manager).DrawHitbox();
            GetCurrentArmor()?.DrawHitbox();
        }
    }
}
