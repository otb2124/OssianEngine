using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class DialogueAnswerRequirement : Requirement
    {

        public int DialogueId;
        public int OptionId;

        public DialogueAnswerRequirement(int dialogueId, int optionId)
        {
            DialogueId = dialogueId;
            OptionId = optionId;
        }

        public override bool Check()
        {
            if(Entities.DialogueManager.PlayerAnswers.ContainsKey(DialogueId))
            {
                return Entities.DialogueManager.PlayerAnswers[DialogueId] == OptionId;
            }
            else
            {
                return false;
            }
            
        }
    }
}
