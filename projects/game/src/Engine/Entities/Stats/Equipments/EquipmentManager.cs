using Entities;

namespace Equipment
{
    public class EquipmentManager
    {
        public enum WeaponHand
        {
            LEFT,
            RIGHT
        }

        public WeaponHand currentHand = WeaponHand.LEFT;
        public Weapon weaponL;
        public Weapon weaponR;

        public Armor chestplate;
        public Armor helmet;

        public Hitbox weaponHB;
        public Hitbox armorHB;


        public EquipmentManager()
        {
            weaponL = new Weapon();

            weaponHB = new Hitbox();
            armorHB = new Hitbox();
        }


        public Weapon GetCurrentWeapon()
        {
            if(currentHand == WeaponHand.LEFT)
            {
                return weaponL;
            }
            else
            {
                return weaponR;
            }
        }
    }
}
