using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BodyBattleBodyManager : BattleBodyManager
    {

        public BodyBattleBodyManager() : base()
        {
            CreateBodies(1);
        }

        public override void CreateBodies(int count)
        {
            BattleBodies.Clear();

            for (int i = 0; i < count; i++)
            {
                BattleBodies.Add(new BodyBattleBody());
            }
        }

        public override void CreateBody()
        {
            BattleBodies.Add(new BodyBattleBody());
        }
    }
}
