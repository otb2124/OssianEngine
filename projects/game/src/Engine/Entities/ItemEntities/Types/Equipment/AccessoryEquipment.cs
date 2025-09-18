using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AccessoryEquipment : Equipment
    {


        public AccessoryEquipment(ItemKey itemKey) : base(itemKey)
        {

        }


        public override void SetItem()
        {
            base.SetItem();

            switch (ItemKey.EnumValue)
            {
                case ItemLib.Necklaces.IRON_NECKLACE:
                    Name = "Iron Necklace";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.NECKLACE;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
                case ItemLib.Belts.IRON_BELT:
                    Name = "Iron Belt";
                    Description = "An iron helmet";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.BELT;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
                case ItemLib.Rings:
                    Name = "Iron Ring";
                    Description = "An iron ring";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.RING;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
                case ItemLib.Pets.CALL_DOG:
                    Name = "Dog bone";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.PET;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
                case ItemLib.LightPets.CALL_FIREFLY:
                    Name = "Firefly in a Jar";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.PET_LIGHT;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
                case ItemLib.Containments.BACKPACK:
                    Name = "Backpack";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.CONTAINMENT;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
                case ItemLib.Capes.LEATHER_CAPE:
                    Name = "Leather Cape";
                    Description = "An iron chestplate";
                    Value = 100;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlotTake = EquipmentSlotsTakes.CAPE;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
            }
        }
    }
}
