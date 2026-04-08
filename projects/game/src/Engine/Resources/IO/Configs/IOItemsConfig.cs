using Entities;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Entities.BattleMovesetFactory;

namespace Resources
{
    public class IOItemsConfig : IOConfig
    {
        public List<ItemConfig> ItemList { get; set; }

        public IOItemsConfig()
        {
            FilePath = "items";
        }

        public override void Apply()
        {
            ResourceLoader.ItemResources = new Dictionary<EquatableKey, ItemConfig>();

            foreach (var config in ItemList)
            {
                if (string.IsNullOrEmpty(config.key))
                    continue;

                try
                {
                    EquatableKey key = ItemFactory.GetItemKeyFromString(config.key);
                    ResourceLoader.ItemResources[key] = config;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to add item {config.key}: {ex.Message}");
                }
            }
        }
    }

    public class ItemConfig
    {
        public string key; //primary
        public string name;
        public string description;
        public int value;
        public string rarity;
        public string equipmentSlot;
        public bool stackable;
        public BattleStatsDataConfig battleStatsData = new BattleStatsDataConfig();
        public WeaponBodyDataConfig weaponBodyData = new WeaponBodyDataConfig();
        public string spriteSheet;
        public override string ToString()
        {
            return $"{key}, {name}, {description}";
        }

        public Item ToItem()
        {
            EquatableKey itemKey = ItemFactory.GetItemKeyFromString(key);

            Item item = ItemFactory.CreateItem(itemKey);  // This creates the correct derived type

            // Now fill the data from JSON
            item.Name = name;
            item.Description = description;
            item.Value = value;
            item.Rarity = rarity.ToEnum<ItemRarity>();
            item.Stackable = stackable;

            // Fill BattleItemStatsData from battleStatsData
            if (item is Equipment equipment)
            {
                equipment.BattleItemStatsData.DamageSet.PhysDamage = battleStatsData.physDamage;
                equipment.BattleItemStatsData.DamageSet.MagicDamage = battleStatsData.magicDamage;
                equipment.BattleItemStatsData.PoiseDamage = battleStatsData.poiseDamage;
                equipment.BattleItemStatsData.KnockbackPower = battleStatsData.knockbackPower;
                equipment.BattleItemStatsData.StatsCostSet.StaminaCost = battleStatsData.staminaCost;
                equipment.BattleItemStatsData.StatsCostSet.ManaCost = battleStatsData.manaCost;

                equipment.BattleItemStatsData.DefenseSet.PhysDef = battleStatsData.physDef;
                equipment.BattleItemStatsData.DefenseSet.MagicDef = battleStatsData.magicDef;
            }

            // Special handling for WeaponEquipment
            if (item is WeaponEquipment weaponItem && weaponBodyData != null)
            {
                weaponItem.WeaponBodyData.WeaponSwingSpeedMultiplier = weaponBodyData.weaponSwingSpeedMultiplier;
                weaponItem.WeaponBodyData.Sprite = weaponBodyData.sprite.ToEnum<StaticSprites>();
                weaponItem.WeaponBodyData.MoveSet = weaponBodyData.moveSet.ToEnum<BattleMovesets>();
                weaponItem.WeaponBodyData.ProjectileToCast = weaponBodyData.projectileToCast.ToEnum<Projectiles>();
                weaponItem.WeaponBodyData.DisableHitBoxDamage = weaponBodyData.disableHitBoxDamage;

                if (weaponBodyData.lightSource != null)
                {
                    // Add light source logic if needed
                }
            }

            return item;
        }
    }

    public class BattleStatsDataConfig
    {
        public float physDamage;
        public float magicDamage;
        public float poiseDamage;
        public float knockbackPower;
        public float staminaCost;
        public float manaCost;
        public float physDef;
        public float magicDef;
    }

    public class WeaponBodyDataConfig
    {
        public float weaponSwingSpeedMultiplier;
        public string sprite;
        public string moveSet;
        public string projectileToCast;
        public bool disableHitBoxDamage;
        public LightSourceConfig lightSource;
    }

    public class LightSourceConfig
    {
        public string form;
        public float size;
        public string color;
    }
}
