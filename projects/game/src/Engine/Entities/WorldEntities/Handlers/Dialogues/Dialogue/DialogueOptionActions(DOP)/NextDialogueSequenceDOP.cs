using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NextDialogueSequenceDOP : DialogueOptionAction
    {

        public int SequenceId;

        public NextDialogueSequenceDOP(int sequeneceId)
        {
            SequenceId = sequeneceId;
        }

        public override void Action()
        {
            base.Action();
        }
    }
}
