using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.BattleMovesetFactory;
using Utils;
using Graphics;

namespace CSPlatformerSandbox.Engine.Entities.Battle.WeaponCombo
{
    public class BattleMoveset
    {

        public BattleComboHit[] Combos;

        public BattleMoveset(BattleComboHit[] comboHits)
        {
            Combos = comboHits;
        }
    }
}
