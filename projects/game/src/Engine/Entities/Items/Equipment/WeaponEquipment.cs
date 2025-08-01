
using Utils;

namespace Entities
{
    public class WeaponEquipment : Equipment
    {

        public WeaponEntity WeaponEntity;

        public WeaponEquipment(ItemKey itemKey) : base(itemKey)
        {
            
        }

        public override void SetItem()
        {
            WeaponEntity = new WeaponEntity();

            switch (ItemKey.EnumValue)
            {
                case ItemLib.Weapons.BARE_HAND:
                    Name = "Bare hands";
                    Description = "A terrablade";
                    Value = 0;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 1;
                    WeaponEntity.swingSpeed = 0.4f;
                    WeaponEntity.Size = new Microsoft.Xna.Framework.Vector2(10, 40);
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    break;
                case ItemLib.Weapons.TERRABLADE:
                    Name = "Terrablade";
                    Description = "A terrablade";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 5;
                    KnockbackPower = 1f;
                    WeaponEntity.swingSpeed = 0.5f;
                    WeaponEntity.Size = new Microsoft.Xna.Framework.Vector2(10, 40);
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    break;
            }
        }

        public override void Draw(Directions direction)
        {
            WeaponEntity?.Draw(direction);

            base.Draw(direction);
        }

    }
}
