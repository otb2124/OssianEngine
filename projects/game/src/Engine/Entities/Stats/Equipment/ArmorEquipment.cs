using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ArmorEquipment : Equipment
    {

        public Hitbox armorHB;

        public ArmorEquipment(ItemKey itemKey) : base(itemKey)
        {
            
        }

        public override void SetItem()
        {
            switch (ItemKey.EnumValue)
            {
                case ItemLib.Armors.IRON_CHESTPLATE:
                    Name = "Iron Chestplate";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDef = 10;
                    EquipmentSlot = EquipmentSlotsTake.TORSO;
                    break;
                case ItemLib.Armors.IRON_HELMET:
                    Name = "Iron Helmet";
                    Description = "An iron helmet";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDef = 10;
                    EquipmentSlot = EquipmentSlotsTake.HEAD;
                    break;
                case ItemLib.Armors.IRON_BOOTS:
                    Name = "Iron Boots";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDef = 10;
                    EquipmentSlot = EquipmentSlotsTake.LEGS;
                    break;
                case ItemLib.Armors.IRON_GLOVES:
                    Name = "Iron Gloves";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDef = 10;
                    EquipmentSlot = EquipmentSlotsTake.HANDS;
                    break;
            }

            if(EquipmentSlot == EquipmentSlotsTake.TORSO)
            {
                armorHB = new Hitbox();
            }
        }
    }
}
