using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class DialogueOptionTimesUsedRequirement : Requirement
    {
        public int OptionId;
        public int DialogueId;
        public int SequenceId;
        public int TimesUsed;

        public DialogueOptionTimesUsedRequirement(int optionId, int dialogueId, int sequenceId, int timesUsed = 1)
        {
            OptionId = optionId;
            DialogueId = dialogueId;
            SequenceId = sequenceId;
            TimesUsed = timesUsed;
        }

        public override bool Check()
        {
            DialogueOption option = Entities.DialogueManager.GetDialogueOption(OptionId, DialogueId, SequenceId);

            if (option.TimesUsed >= TimesUsed)
            {
                return true;
            }

            DialogueOption[] dependentOptions = Entities.DialogueManager.GetAllDependentOptions(DialogueId, OptionId);

            foreach (DialogueOption dependentOption in dependentOptions)
            {
                if (dependentOption.TimesUsed >= TimesUsed)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
