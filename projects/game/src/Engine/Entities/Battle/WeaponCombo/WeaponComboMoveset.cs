using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.WeaponComboMovesetFactory;
using Utils;
using Graphics;

namespace CSPlatformerSandbox.Engine.Entities.Battle.WeaponCombo
{
    public class WeaponComboMoveset
    {

        public WeaponComboHit[] Combos;

        public WeaponComboMoveset(WeaponComboHit[] comboHits)
        {
            Combos = comboHits;
        }
    }
}
