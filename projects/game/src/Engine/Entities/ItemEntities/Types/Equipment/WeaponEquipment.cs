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

        public WeaponEquipment(EquatableKey itemKey) : base(itemKey)
        {

        }

        public override void SetItem()
        {
            base.SetItem();

            WeaponBodyData = new BattleBodyData();

            switch (ItemKey.EnumValue)
            {
                case ItemLib.Weapons.BARE_HAND:
                    Name = "Bare Hands";
                    Description = "Bare hands";
                    Value = 0;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.WEAPON;

                    Stackable = false;

                    BattleItemStatsData.DamageSet.PhysDamage = 1f;
                    BattleItemStatsData.PoiseDamage = 10f;
                    BattleItemStatsData.KnockbackPower = 0.1f;
                    BattleItemStatsData.StatsCostSet.StaminaCost = 5f;

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 2f;
                    WeaponBodyData.Sprite = StaticSprites.NONE;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_BARE_HANDS;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationFramesData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    break;
                case ItemLib.Weapons.TERRABLADE:
                    Name = "Terrablade";
                    Description = "A terrablade";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.WEAPON;

                    Stackable = false;

                    BattleItemStatsData.DamageSet.PhysDamage = 20f;
                    BattleItemStatsData.PoiseDamage = 50f;
                    BattleItemStatsData.KnockbackPower = 2f;
                    BattleItemStatsData.StatsCostSet.StaminaCost = 25f;

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 1f;
                    WeaponBodyData.Sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_SWORD;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationFramesData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    break;
                case ItemLib.Weapons.KNIFE:
                    Name = "Knife";
                    Description = "Iron knife";
                    Value = 10;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.WEAPON;

                    Stackable = false;

                    BattleItemStatsData.DamageSet.PhysDamage = 20f;
                    BattleItemStatsData.PoiseDamage = 50f;
                    BattleItemStatsData.KnockbackPower = 2f;
                    BattleItemStatsData.StatsCostSet.StaminaCost = 25f;

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 1.5f;
                    WeaponBodyData.Sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_KNIFE;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationFramesData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    break;
                case ItemLib.Weapons.TORCH:
                    Name = "Torch";
                    Description = "A torch";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.WEAPON;

                    Stackable = false;

                    BattleItemStatsData.DamageSet.PhysDamage = 20f;
                    BattleItemStatsData.PoiseDamage = 50f;
                    BattleItemStatsData.KnockbackPower = 2f;
                    BattleItemStatsData.StatsCostSet.StaminaCost = 25f;

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 1f;
                    WeaponBodyData.Sprite = StaticSprites.ENTITIES_WEAPONS_TORCH;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_SWORD;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationFramesData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    WeaponBodyData.LightSourceData = new Graphics.LightSource.LightSourceData(LightSource.LightSourceData.LightSourceForms.CIRCULAR, new Vector2(150f, 0f), Vector2.Zero, new Color(1f, 1f, 0.8f, 0.5f), 10f, 0f);
                    break;
                case ItemLib.Weapons.FIREBALL_SPELL:
                    Name = "Fireball Spell";
                    Description = "A fireball spell";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.WEAPON;

                    Stackable = false;

                    BattleItemStatsData.DamageSet.MagicDamage = 20f;
                    BattleItemStatsData.PoiseDamage = 50f;
                    BattleItemStatsData.KnockbackPower = 2f;
                    BattleItemStatsData.StatsCostSet.ManaCost = 25f;

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 1f;
                    WeaponBodyData.Sprite = StaticSprites.NONE;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_SPELL;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationFramesData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    WeaponBodyData.ProjectileToCast = Projectiles.FIREBALL;
                    WeaponBodyData.DisableHitBoxDamage = true;
                    break;
                case ItemLib.Weapons.BOW:
                    Name = "Bow";
                    Description = "A bow";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.WEAPON;

                    Stackable = false;

                    BattleItemStatsData.DamageSet.PhysDamage = 5f;
                    BattleItemStatsData.PoiseDamage = 10f;
                    BattleItemStatsData.KnockbackPower = 1f;
                    BattleItemStatsData.StatsCostSet.ManaCost = 0f;
                    BattleItemStatsData.StatsCostSet.StaminaCost = 15f;

                    WeaponBodyData.WeaponSwingSpeedMultiplier = 1f;
                    WeaponBodyData.Sprite = StaticSprites.NONE;
                    WeaponBodyData.MoveSet = BattleMovesets.WEAPON_BOW;
                    WeaponBodyData.WeaponOutAnimationData = new Graphics.AnimationFramesData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
                    WeaponBodyData.ProjectileToCast = Projectiles.ARROW;
                    WeaponBodyData.DisableHitBoxDamage = true;
                    break;
            }

            WeaponBodyData.ModelStateBetweenHits = ModelStates.WEAPON_OUT_IDLE;
        }

    }
}