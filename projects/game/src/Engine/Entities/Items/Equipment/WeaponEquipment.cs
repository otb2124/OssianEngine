
using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using Utils;
using static Entities.WeaponComboMovesetFactory;

namespace Entities
{
    public class WeaponEquipment : Equipment
    {

        public WeaponBody WeaponBody;

        public WeaponEquipment(ItemKey itemKey) : base(itemKey)
        {
            
        }

        public override void SetItem()
        {
            WeaponBody = new WeaponBody();

            switch (ItemKey.EnumValue)
            {
                case ItemLib.Weapons.BARE_HAND:
                    Name = "Bare Hands";
                    Description = "Bare hands";
                    Value = 0;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 1;
                    KnockbackPower = 0.1f;
                    WeaponBody.WeaponSwingSpeedMultiplier = 2f;
                    WeaponBody.Sprite = StaticSprites.NONE;
                    WeaponBody.MoveSet = WeaponMovesets.BARE_HANDS;
                    WeaponBody.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
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
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    PoiseDmg = 25;

                    WeaponBody.WeaponSwingSpeedMultiplier = 1f;
                    WeaponBody.Sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
                    WeaponBody.MoveSet = WeaponMovesets.SWORD;
                    WeaponBody.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    break;
                case ItemLib.Weapons.KNIFE:
                    Name = "Knife";
                    Description = "Iron knife";
                    Value = 10;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 2.5f;
                    KnockbackPower = 1f;
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    PoiseDmg = 15;

                    WeaponBody.WeaponSwingSpeedMultiplier = 1.5f;
                    WeaponBody.Sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
                    WeaponBody.MoveSet = WeaponMovesets.KNIFE;
                    WeaponBody.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    break;
                case ItemLib.Weapons.TORCH:
                    Name = "Torch";
                    Description = "A torch";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 5;
                    KnockbackPower = 2f;
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    PoiseDmg = 25;

                    WeaponBody.WeaponSwingSpeedMultiplier = 1f;
                    WeaponBody.Sprite = StaticSprites.ENTITIES_WEAPONS_TORCH;
                    WeaponBody.MoveSet = WeaponMovesets.SWORD;
                    WeaponBody.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    WeaponBody.LightSourceData = new Graphics.LightSource.LightSourceData(LightSource.LightSourceData.LightSourceForms.CIRCULAR, new Vector2(100f, 0f), Vector2.Zero, new Color(1f, 1f, 0.8f, 0.5f), 10f, 0f);
                    break;
            }

            WeaponBody.Init();
        }

        public override void Draw(Model model)
        {
            WeaponBody?.Draw(model);

            base.Draw(model);
        }

    }
}
