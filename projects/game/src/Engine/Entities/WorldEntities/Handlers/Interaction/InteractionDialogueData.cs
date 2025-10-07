using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InteractionDialogueData
    {

        public int[] DialogueSequenceIds;

        public InteractionDialogueData(int[] dialogueSequence)
        {
            DialogueSequenceIds = dialogueSequence;
        }

        public int GetPrioritiezedSequence()
        {
            //TODO: there are more then two currently for priority start next sequence after previous
            int[] awailableSequences = GetAwailableSequenceIds();
            int prioritizedId = awailableSequences[0];

            foreach (int id in awailableSequences)
            {
                Console.WriteLine(id);

                if(Entities.DialogueManager.GetSequence(id).ChoicePriority > Entities.DialogueManager.GetSequence(prioritizedId).ChoicePriority)
                {
                    prioritizedId = id;
                }
            }

            return prioritizedId;
        }

        public int[] GetAwailableSequenceIds()
        {
            List<int> idList = new List<int>();

            foreach (int id in DialogueSequenceIds)
            {
                DialogueSequence sequence = Entities.DialogueManager.GetSequence(id);

                if (!sequence.Disabled)
                {
                    if (sequence.Requirements != null)
                    {
                        foreach (Requirement requirement in Entities.DialogueManager.GetSequence(id).Requirements)
                        {
                            if (requirement.Check())
                            {
                                idList.Add(id);
                            }
                        }
                    }
                    else
                    {
                        idList.Add(id);
                    }
                }
                
            }

            return idList.ToArray();
        }
    }
}
