using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NextDialogueDOP : RequirementalDialogueOptionAction
    {

        public int DialogueId;

        public NextDialogueDOP(int dialogueId, Requirement[] requirements = null) : base(requirements)
        {
            DialogueId = dialogueId;
        }

        public override void Action(DialogueManager manager)
        {
            if (!Check()) { return; }

            manager.UpdateSequence(DialogueId);
            manager.UpdateUIDialogueComponent();
        }
    }
}
