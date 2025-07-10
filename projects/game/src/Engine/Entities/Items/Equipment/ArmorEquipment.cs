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

        public Hitbox hitbox;

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
                hitbox = new Hitbox();
            }
        }



        public void DrawHitbox()
        {
            this.hitbox.Draw(Color.Blue);
        }


        public void Update(Model model)
        {
            hitbox.Update(
                FlatConverter.ToVector2(model.body.Position),
                new Vector2(model.body.Width, model.body.Height),
                model.body.Angle
            );
        }
    }
}
