using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class CurrentInputKeyRequirement : Requirement
    {

        public Inputs.KeyHandler.KeyStates KeyState;

        public CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates keyState, bool negate = false)
        {
            KeyState = keyState;
            IsNegation = negate;
        }

        public override bool Check(StatsEntity Entity)
        {
            bool result = false;

            if(Entity != null && Entity is AIEntity ent)
            {
                result = ent.EntityControlHandler.ControlStateMap[KeyState];
            }

            return IsNegation ? !result : result;
        }
    }
}
