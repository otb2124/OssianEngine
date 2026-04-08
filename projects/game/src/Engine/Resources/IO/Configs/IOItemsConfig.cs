using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class IOItemsConfig : IOConfig
    {
        public List<ItemConfig> ItemList { get; private set; } = new List<ItemConfig>();

        public IOItemsConfig()
        {
            FilePath = "items";
        }

        public override void Apply()
        {
            Console.WriteLine(RawJsonData);
        }
    }

    public struct ItemConfig
    {
        public string key;
        public string name;
        public string description;
        public int value;
        public string rarity;
        public string equipmentSlot;
        public bool stackable;
        public BattleStatsDataConfig battleStatsData;
        public WeaponBodyDataConfig weaponBodyData;
        public string spriteSheet;
    }

    public struct BattleStatsDataConfig
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

    public struct WeaponBodyDataConfig
    {
        public float weaponSwingSpeedMultiplier;
        public string sprite;
        public string moveSet;
        public string projectileToCast;
        public bool disableHitBoxDamage;
        public LightSourceConfig lightSource;
    }

    public struct LightSourceConfig
    {
        public string form;
        public float size;
        public string color;
    }
}
