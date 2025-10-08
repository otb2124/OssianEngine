using Microsoft.Xna.Framework;
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
        public int EntityDialogueId;

        public InteractionDialogueData(int[] dialogueSequence, int entityDialogueId = -1)
        {
            DialogueSequenceIds = dialogueSequence;
            EntityDialogueId = entityDialogueId;
        }

        public void StartCurrentDialogue()
        {
            Entities.DialogueManager.SetDialogue(new StartSequenceDOP(GetPrioritiezedSequence()));
        }

        public int GetPrioritiezedSequence()
        {
            //TODO: there are more then two currently for priority start next sequence after previous
            int[] awailableSequences = GetAwailableSequenceIds();

            int prioritizedId = -1;

            if (awailableSequences.Length > 0)
            {
                prioritizedId = awailableSequences[0];

                foreach (int id in awailableSequences)
                {
                    if (Entities.DialogueManager.GetSequence(id).ChoicePriority > Entities.DialogueManager.GetSequence(prioritizedId).ChoicePriority)
                    {
                        prioritizedId = id;
                    }
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
                    bool allRequirementsMet = true;

                    if (sequence.Requirements != null && sequence.Requirements.Length > 0)
                    {
                        foreach (Requirement requirement in sequence.Requirements)
                        {
                            if (!requirement.Check())
                            {
                                allRequirementsMet = false;
                                break;
                            }
                        }
                    }

                    if (allRequirementsMet)
                    {
                        idList.Add(id);
                    }
                }
            }

            return idList.ToArray();
        }
    }
}
