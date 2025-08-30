using Entities;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ArmorEquipment : Equipment
    {

        public ArmorEquipment(ItemKey itemKey) : base(itemKey)
        {
            
        }

        public override void SetItem()
        {
            base.SetItem();

            switch (ItemKey.EnumValue)
            {
                case ItemLib.Chestplates.IRON_CHESTPLATE:
                    Name = "Iron Chestplate";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlot = EquipmentSlotsTake.TORSO;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
                case ItemLib.Helmets.IRON_HELMET:
                    Name = "Iron Helmet";
                    Description = "An iron helmet";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlot = EquipmentSlotsTake.HEAD;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
                case ItemLib.Boots.IRON_BOOTS:
                    Name = "Iron Boots";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlot = EquipmentSlotsTake.LEGS;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
                case ItemLib.Gloves.IRON_GLOVES:
                    Name = "Iron Gloves";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    EquipmentSlot = EquipmentSlotsTake.HANDS;

                    BattleItemStatsData.DefenseSet.PhysDef = 5f;
                    break;
            }
        }
    }
}
