using Entities;
using Microsoft.Xna.Framework;
using UI;
using Utils;
using static Entities.PhysicalEntity;
using static Resources.Model;

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

        public WeaponHand currentHand = WeaponHand.LEFT;

        public EquipmentSlot[] slots;


        public EquipmentManager()
        {
            slots = new EquipmentSlot[13];
            slots[0] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L);
            slots[1] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_R);
            slots[2] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE);
            slots[3] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.HELMET);
            slots[4] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.BOOTS);
            slots[5] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.GLOVES);
            slots[6] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.NECKLACE);
            slots[7] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.BELT);
            slots[8] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.RING_L);
            slots[9] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.RING_R);
            slots[10] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.PET);
            slots[11] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.PET_LIGHT);
            slots[12] = new EquipmentSlot(EquipmentSlot.EquipmentSlots.CONTAINMENT);
        }


        public EquipmentSlot GetEquipmentSlot(EquipmentSlot.EquipmentSlots type)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Type == type)
                {
                    return slots[i];
                }
            }

            return null;
        }

        public WeaponEquipment GetCurrentWeapon()
        {
            if(currentHand == WeaponHand.LEFT)
            {
                return (WeaponEquipment)GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment;
            }
            else
            {
                return (WeaponEquipment)GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_R).Equipment;
            }
        }

        public ArmorEquipment GetCurrentArmor()
        {
            return ((ArmorEquipment)GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment);
        }

        public void Update(Vector2 hitboxpos, Vector2 hitboxsize, float angle)
        {
            GetCurrentWeapon().hitbox.Update(hitboxpos, hitboxsize, angle);
        }


        public void DrawHitbox()
        {
            GetCurrentWeapon().DrawHitbox();
            GetCurrentArmor().DrawHitbox();
        }


        public void Draw(Directions direction)
        {
            this.GetCurrentWeapon().Draw(direction);
        }
    }
}
