using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DisableDialogueSequenceDOP : RequirementalDialogueOptionAction
    {

        public int SequenceId;

        public DisableDialogueSequenceDOP(int sequenceId, Requirement[] requirements = null) : base(requirements)
        {
            SequenceId = sequenceId;
        }

        public override void Action(DialogueManager manager)
        {
            if (!Check()) { return; }

            manager.GetSequence(SequenceId).Disabled = true;
        }
    }
}
