
using Resources;
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
                    WeaponEntity.WeaponSwingSpeedMultiplier = 1f;
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
                    WeaponEntity.WeaponSwingSpeedMultiplier = 1f;
                    WeaponEntity.Size = new Microsoft.Xna.Framework.Vector2(10, 40);
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    break;
            }

            WeaponEntity.Init();
        }

        public override void Draw(Model model)
        {
            WeaponEntity?.Draw(model);

            base.Draw(model);
        }

    }
}
