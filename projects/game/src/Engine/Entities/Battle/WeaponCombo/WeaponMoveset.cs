using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.WeaponComboHitSetFactory;
using Utils;
using Graphics;

namespace CSPlatformerSandbox.Engine.Entities.Battle.WeaponCombo
{
    public class WeaponMoveset
    {

        public WeaponComboHit[] Combos;

        public WeaponMoveset(WeaponComboHit[] comboHits)
        {
            Combos = comboHits;
        }
    }
}
