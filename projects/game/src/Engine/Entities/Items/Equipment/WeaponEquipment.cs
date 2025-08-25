
using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using Utils;
using static Entities.BattleMovesetFactory;

namespace Entities
{
    public class WeaponEquipment : Equipment
    {

        public BattleBodyData WeaponBodyData;

        public WeaponEquipment(ItemKey itemKey) : base(itemKey)
        {
            
        }

        public override void SetItem()
        {
            WeaponBodyData = new BattleBodyData();

            switch (ItemKey.EnumValue)
            {
                case ItemLib.Weapons.BARE_HAND:
                    Name = "Bare Hands";
                    Description = "Bare hands";
                    Value = 0;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 1;
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    PoiseDmg = 10;
                    KnockbackPower = 0.1f;

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 2f;
                    WeaponBodyData.Sprite = StaticSprites.NONE;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_BARE_HANDS;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    break;
                case ItemLib.Weapons.TERRABLADE:
                    Name = "Terrablade";
                    Description = "A terrablade";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDmg = 20;
                    KnockbackPower = 2f;
                    EquipmentSlot = EquipmentSlotsTake.WEAPON_SINGLE;
                    PoiseDmg = 50;

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 1f;
                    WeaponBodyData.Sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_SWORD;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
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

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 1.5f;
                    WeaponBodyData.Sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_KNIFE;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
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

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 1f;
                    WeaponBodyData.Sprite = StaticSprites.ENTITIES_WEAPONS_TORCH;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_SWORD;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    WeaponBodyData.LightSourceData = new Graphics.LightSource.LightSourceData(LightSource.LightSourceData.LightSourceForms.CIRCULAR, new Vector2(150f, 0f), Vector2.Zero, new Color(1f, 1f, 0.8f, 0.5f), 10f, 0f);
                    break;
            }

            WeaponBodyData.ModelStateBetweenHits = ModelStates.WEAPON_OUT_IDLE;
        }

    }
}
