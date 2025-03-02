using Entities;
using Microsoft.Xna.Framework;
using static Entities.PhysicalEntity;

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

        public Hitbox armorHB;


        public EquipmentManager()
        {
            weaponL = new Weapon();

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


        public void DrawHitbox()
        {
            this.GetCurrentWeapon().DrawHitbox();
            this.armorHB.Draw(Color.Blue);
        }


        public void Draw(Directions direction)
        {
            this.GetCurrentWeapon().Draw(direction);
        }
    }
}
