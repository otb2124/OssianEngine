using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Armor : Equipment
    {

        public Armor() : base()
        {
            Type = ItemLib.ItemTypes.ARMOR;
            Value = 10;
            Name = "Armor";
            Description = "desc";
            Rarity = ItemRarity.COMMON;
        }
    }
}
