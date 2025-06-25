using Entities;
using Microsoft.Xna.Framework;
using Utils;
using static Entities.PhysicalEntity;
using static Resources.Model;

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
        public WeaponEquipment weaponL;
        public WeaponEquipment weaponR;

        public ArmorEquipment chestplate;
        //public ArmorEquipment helmet;


        public EquipmentManager()
        {
            weaponL = (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE));

            chestplate = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_CHESTPLATE));
        }


        public WeaponEquipment GetCurrentWeapon()
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

        public void Update(Vector2 hitboxpos, Vector2 hitboxsize, float angle)
        {
            GetCurrentWeapon().hitbox.Update(hitboxpos, hitboxsize, angle);
        }


        public void DrawHitbox()
        {
            this.GetCurrentWeapon().DrawHitbox();
            this.chestplate.armorHB.Draw(Color.Blue);
        }


        public void Draw(Directions direction)
        {
            this.GetCurrentWeapon().Draw(direction);
        }
    }
}
