using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NextDialogueSequenceDOP : RequirementalDialogueOptionAction
    {

        public int SequenceId;

        public NextDialogueSequenceDOP(int sequeneceId, Requirement[] requirements = null) : base(requirements)
        {
            SequenceId = sequeneceId;
        }

        public override void Action(DialogueManager manager)
        {
            if (!Check()) { return; }
        }
    }
}
