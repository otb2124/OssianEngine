using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DisableDialogueSequenceDOP : DialogueOptionAction
    {

        public int SequenceId;

        public DisableDialogueSequenceDOP(int sequenceId)
        {
            SequenceId = sequenceId;
        }
    }
}
