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
            DialogueAnswer answer = Entities.DialogueManager.GetDialogueAnswer(DialogueId, OptionId);

            Console.WriteLine($"Check if answer exists: {answer != null}");

            if (answer != null)
            {
                Console.WriteLine($"Original answer for: {answer}");
                return true;
            }

            DialogueOption[] dependentOptions = Entities.DialogueManager.GetAllDependentOptions(DialogueId, OptionId);

            foreach (DialogueOption dependentOption in dependentOptions)
            {
                DialogueAnswer dependentAnswer = Entities.DialogueManager.GetDialogueAnswer(Entities.DialogueManager.GetDialogueIdByOptionId(dependentOption.Id), dependentOption.Id);

                if (dependentAnswer != null)
                {
                    Console.WriteLine($"Dependent answer for {answer}, is answered via {dependentAnswer}");
                    return true;
                }
            }

            return false;
        }
    }
}
