using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ExitDialogueDOP : RequirementalDialogueOptionAction
    {

        public ExitDialogueDOP(Requirement[] requirements = null) : base(requirements)
        {

        }

        public override void Action(DialogueManager manager)
        {
            if (!Check()) { return; }

            manager.CurrentDialogueId = -1;
            manager.CurrentSequence = null;
            manager.RemoveUIDialogueComponent();
        }
    }
}
