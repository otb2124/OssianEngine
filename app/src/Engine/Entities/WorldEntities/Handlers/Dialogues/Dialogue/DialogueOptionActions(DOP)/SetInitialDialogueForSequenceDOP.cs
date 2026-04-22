using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SetInitialDialogueForSequenceDOP : RequirementalDialogueOptionAction
    {

        public int DialogueId;
        public int SequenceId;

        public SetInitialDialogueForSequenceDOP(int dialogueId, int sequeneceId, Requirement[] requirements = null) : base(requirements)
        {
            DialogueId = dialogueId;
            SequenceId = sequeneceId;
        }

        public override void Action(DialogueManager manager)
        {
            if (!Check()) { return; }

            manager.GetSequence(SequenceId).InitialDialogueId = DialogueId;
        }
    }
}
