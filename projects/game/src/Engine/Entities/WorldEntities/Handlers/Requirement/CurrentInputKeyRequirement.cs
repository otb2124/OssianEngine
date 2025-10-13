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

        public CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates keyState)
        {
            KeyState = keyState;
        }

        public override bool Check()
        {
            return Inputs.Inputs.KeyHandler.KeyStateMap[KeyState];
        }
    }
}
