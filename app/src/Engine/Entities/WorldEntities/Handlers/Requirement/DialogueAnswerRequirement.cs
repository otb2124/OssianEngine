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

        public override bool Check(StatsEntity Entity)
        {
            DialogueAnswer answer = Entities.DialogueManager.GetDialogueAnswer(DialogueId, OptionId);

            if (answer != null)
            {
                return true;
            }

            DialogueOption[] dependentOptions = Entities.DialogueManager.GetAllDependentOptions(DialogueId, OptionId);

            foreach (DialogueOption dependentOption in dependentOptions)
            {
                int dependentDialogueId = Entities.DialogueManager.GetDialogueIdByOptionId(dependentOption.Id);

                DialogueAnswer dependentAnswer = Entities.DialogueManager.GetDialogueAnswer(dependentDialogueId, dependentOption.Id);

                if (dependentAnswer != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
