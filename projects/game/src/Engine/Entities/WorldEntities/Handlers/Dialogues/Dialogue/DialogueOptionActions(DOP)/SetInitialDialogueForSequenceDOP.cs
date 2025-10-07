using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SetInitialDialogueForSequenceDOP : DialogueOptionAction
    {

        public int DialogueId;
        public int SequenceId;

        public SetInitialDialogueForSequenceDOP(int dialogueId, int sequeneceId)
        {
            DialogueId = dialogueId;
            SequenceId = sequeneceId;
        }

        public override void Action()
        {
            base.Action();
        }
    }
}
