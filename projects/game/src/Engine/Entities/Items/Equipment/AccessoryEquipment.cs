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
            switch (ItemKey.EnumValue)
            {
                case ItemLib.Accessories.IRON_NECKLACE:
                    Name = "Iron Necklace";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDef = 10;
                    EquipmentSlot = EquipmentSlotsTake.NECKLACE;
                    break;
                case ItemLib.Accessories.IRON_BELT:
                    Name = "Iron Belt";
                    Description = "An iron helmet";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDef = 10;
                    EquipmentSlot = EquipmentSlotsTake.BELT;
                    break;
                case ItemLib.Accessories.CALL_DOG:
                    Name = "Dog bone";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDef = 10;
                    EquipmentSlot = EquipmentSlotsTake.PET;
                    break;
                case ItemLib.Accessories.CALL_FIREFLY:
                    Name = "Firefly in a Jar";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDef = 10;
                    EquipmentSlot = EquipmentSlotsTake.PET_LIGHT;
                    break;
                case ItemLib.Accessories.BACKPACK:
                    Name = "Backpack";
                    Description = "An iron chestplate";
                    Value = 500;
                    Rarity = ItemRarity.COMMON;
                    PhysDef = 10;
                    EquipmentSlot = EquipmentSlotsTake.CONTAINMENT;
                    break;
            }
        }
    }
}
