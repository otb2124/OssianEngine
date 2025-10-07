using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NextDialogueDOP : DialogueOptionAction
    {

        public int DialogueId;

        public NextDialogueDOP(int dialogueId)
        {
            DialogueId = dialogueId;
        }

        public override void Action()
        {
            base.Action();
        }
    }
}
