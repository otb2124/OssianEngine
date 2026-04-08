using Entities;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class ArmorEquipment : Equipment
    {

        public SpriteSheets SpriteSheet;

        public ArmorEquipment(EquatableKey itemKey) : base(itemKey)
        {
            
        }

    }
}
