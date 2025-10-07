using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StartSequenceDOP : RequirementalDialogueOptionAction
    {

        public int SequenceId;

        public StartSequenceDOP(int sequenceId, Requirement[] requirements = null) : base(requirements)
        {
            SequenceId = sequenceId;
        }

        public override void Action(DialogueManager manager)
        {
            if (!Check()) { return; }

            if (manager.CurrentSequence != null) { return; }

            manager.CurrentSequence = manager.GetSequence(SequenceId);
            manager.CurrentDialogueId = manager.CurrentSequence.InitialDialogueId;
            manager.UpdateSequence(manager.CurrentDialogueId);
            manager.InitializeUIDialogueComponent();
        }
    }
}
