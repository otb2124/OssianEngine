
using Microsoft.Xna.Framework;
using Resources;
using Utils;
using static Entities.WeaponComboMovesetFactory;

namespace Entities
{
    public class WeaponEquipment : Equipment
    {

        public WeaponBody WeaponEntity;

        public WeaponEquipment(ItemKey itemKey) : base(itemKey)
        {
            
        }

        public override void SetItem()
        {
            WeaponEntity = new WeaponBody();

            switch (ItemKey.EnumValue)
            {
                case ItemLib.Weapons.BARE_HAND:
                    Name = "Bare Hands";
                    Description = "Bare hands";
                    Value = 0;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 1;
                    KnockbackPower = 0.1f;
                    WeaponEntity.WeaponSwingSpeedMultiplier = 2f;
                    WeaponEntity.Sprite = StaticSprites.NONE;
                    WeaponEntity.MoveSet = WeaponMovesets.BARE_HANDS;
                    WeaponEntity.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    PoiseDmg = 10;
                    break;
                case ItemLib.Weapons.TERRABLADE:
                    Name = "Terrablade";
                    Description = "A terrablade";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 5;
                    KnockbackPower = 2f;
                    WeaponEntity.WeaponSwingSpeedMultiplier = 1f;
                    WeaponEntity.Sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
                    WeaponEntity.MoveSet = WeaponMovesets.SWORD;
                    WeaponEntity.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    PoiseDmg = 25;
                    break;
                case ItemLib.Weapons.KNIFE:
                    Name = "Knife";
                    Description = "Iron knife";
                    Value = 10;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 2.5f;
                    KnockbackPower = 1f;
                    WeaponEntity.WeaponSwingSpeedMultiplier = 1.5f;
                    WeaponEntity.Sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
                    WeaponEntity.MoveSet = WeaponMovesets.KNIFE;
                    WeaponEntity.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    PoiseDmg = 15;
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
