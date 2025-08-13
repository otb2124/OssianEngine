using Resources;
using System;

namespace Entities
{

    public class EquipmentManager
    {
        public enum WeaponHand
        {
            LEFT,
            RIGHT,
            BOTH
        }

        public WeaponHand CurrentHand = WeaponHand.LEFT;
        public EquipmentSlot[] Slots;
        public WeaponEquipment LeftWeaponIn;
        public WeaponEquipment RightWeaponIn;
        public bool IsWeaponOut;

        public EquipmentManager()
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

        public void SetWeapon(WeaponHand hand, WeaponEquipment weapon)
        {
            SetWeaponSwapPlaceHolder(hand, weapon);
            SetEquipmentSlot(HandToSlot(hand), CreateBareHands());
        }

        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlots type) =>
            Array.Find(Slots, slot => slot.Type == type);

        public void SetEquipmentSlot(EquipmentSlot.EquipmentSlots slotType, Equipment item) =>
            GetEquipmentSlot(slotType).Equipment = item;

        public EquipmentSlot.EquipmentSlots GetCurrentWeaponSlot() =>
            HandToSlot(CurrentHand);

        public ArmorEquipment GetCurrentArmor() =>
            (ArmorEquipment)GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment;

        public WeaponEquipment GetCurrentWeaponSwapPlaceHolder() =>
            HandToWeaponIn(CurrentHand);

        public WeaponEquipment GetCurrentWeapon()
        {
            var slot = GetCurrentWeaponSlot();
            var weapon = (WeaponEquipment)GetEquipmentSlot(slot).Equipment;
            if (weapon == null)
            {
                weapon = CreateBareHands();
                SetEquipmentSlot(slot, weapon);
            }
            return weapon;
        }


        public void SetCurrentWeaponSwapPlaceHolder(WeaponEquipment toChange) =>
            SetWeaponSwapPlaceHolder(CurrentHand, toChange);
        public void SetWeaponSwapPlaceHolder(WeaponHand hand, WeaponEquipment toChange)
        {
            if (hand == WeaponHand.LEFT)
                LeftWeaponIn = toChange;
            else
                RightWeaponIn = toChange;
        }
        public void WeaponOutSwap()
        {
            var slot = GetCurrentWeaponSlot();
            var currentWeapon = (WeaponEquipment)GetEquipmentSlot(slot).Equipment;
            var placeholder = GetCurrentWeaponSwapPlaceHolder();
            SetCurrentWeaponSwapPlaceHolder(currentWeapon);
            SetEquipmentSlot(slot, placeholder);
        }


        public WeaponEquipment HandToWeaponIn(WeaponHand hand) =>
           hand == WeaponHand.LEFT ? LeftWeaponIn : RightWeaponIn;
        public EquipmentSlot.EquipmentSlots HandToSlot(WeaponHand hand) =>
            hand == WeaponHand.LEFT ? EquipmentSlot.EquipmentSlots.WEAPON_L : EquipmentSlot.EquipmentSlots.WEAPON_R;
        public static WeaponEquipment CreateBareHands() =>
            (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.BARE_HAND));


        public void Draw(Model model) =>
            GetCurrentWeapon()?.Draw(model);

        public void DrawHitbox()
        {
            GetCurrentWeapon()?.WeaponEntity.DrawHitbox();
            GetCurrentArmor()?.DrawHitbox();
        }
    }
}
