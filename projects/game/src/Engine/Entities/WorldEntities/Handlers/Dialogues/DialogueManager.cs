using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DialogueManager
    {
        public DialogueSequence CurrentSequence;

        public Dialogue CurrentDialogue;
        public bool IsSequenceProceeding;

        public Dictionary<int, int> PlayerAnswers;

        public DialogueManager() 
        {
            IsSequenceProceeding = false;
            CurrentDialogue = null;
            PlayerAnswers = new Dictionary<int, int>();
        }

        public void StartDialogue(int newDialogueId)
        {
            if (newDialogueId == -1)
            {
                UI.UI.UIOuterNavigator.RemoveDialogueComponent();
                CurrentDialogue = null;
                IsSequenceProceeding = false;
                return;
            }

            CurrentDialogue = GetDialogueById(newDialogueId, 0);
            CurrentDialogue.SetOptions();
            
            if(!IsSequenceProceeding)
            {
                UI.UI.UIOuterNavigator.ShowDialogueComponent(CurrentDialogue);
                IsSequenceProceeding = true;
            }
            else
            {
                UI.UI.UIOuterNavigator.SetDialogueComponentData(CurrentDialogue);
            }
        }

        public void SetAnswer(int oldDialogueChosenOptionId = -1)
        {
            if (CurrentDialogue != null && oldDialogueChosenOptionId != -1)
            {
                if (PlayerAnswers.ContainsKey(CurrentDialogue.Id))
                {
                    PlayerAnswers[CurrentDialogue.Id] = oldDialogueChosenOptionId;
                }
                else
                {
                    PlayerAnswers.Add(CurrentDialogue.Id, oldDialogueChosenOptionId);
                }
            }
        }

        public static Dialogue GetDialogueById(int id, int sequenceId)
        {
            foreach (Dialogue frame in DialogueSetter.AllDialogues[sequenceId])
            {
                if (frame.Id == id)
                {
                    return frame;
                }
            }

            return null;
        }

        public static DialogueSequence GetDialogueSequenceById(int id)
        {
            foreach (DialogueSequence sequence in DialogueSetter.AllSequences)
            {
                if (sequence.Id == id)
                {
                    return sequence;
                }
            }

            return null;
        }

        public static DialogueOption[] GetAllowedOptions(int dialogueId)
        {
            if (!DialogueSetter.AllDialogueOptions.ContainsKey(dialogueId))
            {
                return Array.Empty<DialogueOption>();
            }

            DialogueOption[] options = DialogueSetter.AllDialogueOptions[dialogueId];
            List<DialogueOption> allowedOptions = new List<DialogueOption>();

            foreach (DialogueOption option in options)
            {
                bool meetsRequirements = true;

                if (option.Requirements != null && option.Requirements.Length > 0)
                {
                    foreach (Requirement requirement in option.Requirements)
                    {
                        if (!requirement.Check())
                        {
                            meetsRequirements = false;
                            break;
                        }
                    }
                }

                if (meetsRequirements)
                {
                    allowedOptions.Add(option);
                }
            }

            return allowedOptions.ToArray();
        }

        public static Dialogue[] GetDialogues(int sequenceId)
        {
            if (!DialogueSetter.AllDialogues.ContainsKey(sequenceId))
            {
                return Array.Empty<Dialogue>();
            }

            Dialogue[] dialogues = DialogueSetter.AllDialogues[sequenceId];

            return dialogues;
        }
    }
}
