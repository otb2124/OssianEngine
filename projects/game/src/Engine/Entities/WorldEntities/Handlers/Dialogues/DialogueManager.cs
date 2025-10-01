using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils.TupleObjectsHelper;

namespace Entities
{
    public class DialogueManager
    {
        public Dictionary<int, DialogueOption[]> DialogueOptions;
        public Dictionary<int, Dialogue[]> Dialogues;
        public DialogueSequence[] Sequences;

        public DialogueSequence CurrentSequence;

        public int CurrentDialogueId;
        public bool IsSequenceProceeding;

        public Dictionary<int, int> PlayerAnswers;

        public DialogueManager()
        {
            Sequences = DialogueSetter.AllSequences;
            Dialogues = DialogueSetter.AllDialogues;
            DialogueOptions = DialogueSetter.AllDialogueOptions;


            PlayerAnswers = new Dictionary<int, int>();
            CurrentDialogueId = -1;
            IsSequenceProceeding = false;
        }

        public void SetSequence(int sequenceId)
        {
            CurrentSequence = GetDialogueSequenceById(sequenceId);
            CurrentSequence.SetDialogues();
        }

        public void SetDialogue(int newDialogueId = 0)
        {
            if (newDialogueId == -1)
            {
                CurrentDialogueId = -1;
                IsSequenceProceeding = false;
                UI.UI.UIOuterNavigator.RemoveDialogueComponent();
                return;
            }

            UpdateSequence(newDialogueId);
            
            if(!IsSequenceProceeding)
            {
                UI.UI.UIOuterNavigator.ShowDialogueComponent(CurrentSequence.Dialogues[CurrentDialogueId]);
                IsSequenceProceeding = true;
            }
            else
            {
                UI.UI.UIOuterNavigator.SetDialogueComponentData(CurrentSequence.Dialogues[CurrentDialogueId]);
            }
        }

        public void UpdateSequence(int newDialogueId)
        {
            CurrentDialogueId = newDialogueId;
            CurrentSequence.Dialogues[CurrentDialogueId].SetOptions();
            CurrentSequence.Dialogues[CurrentDialogueId].TimesRead++;
        }

        public void SetAnswer(int oldDialogueChosenOptionId, int optionUIId)
        {
            if (CurrentDialogueId != -1 && oldDialogueChosenOptionId != -1)
            {
                if (PlayerAnswers.ContainsKey(CurrentDialogueId))
                {
                    PlayerAnswers[CurrentDialogueId] = oldDialogueChosenOptionId;
                }
                else
                {
                    PlayerAnswers.Add(CurrentDialogueId, oldDialogueChosenOptionId);
                }
            }


            //TimesUsed logic
            DialogueOption oldDialogueOption = CurrentSequence.Dialogues[CurrentDialogueId].Options[optionUIId];

            if (oldDialogueOption.ExternalDependencyMap == IntPair.MinusOne)
            {
                CurrentSequence.Dialogues[CurrentDialogueId].Options[optionUIId].TimesUsed++;
            }
            else
            {
                GetDialogueOptionById(oldDialogueOption.ExternalDependencyMap.Item2, oldDialogueOption.ExternalDependencyMap.Item1).TimesUsed++;
            }
        }

        public DialogueOption GetDialogueOptionById(int id, int dialogueId)
        {
            foreach (DialogueOption frame in DialogueOptions[dialogueId])
            {
                if (frame.Id == id)
                {
                    return frame;
                }
            }

            return null;
        }


        public Dialogue GetDialogueById(int id, int sequenceId)
        {
            foreach (Dialogue frame in Dialogues[sequenceId])
            {
                if (frame.Id == id)
                {
                    return frame;
                }
            }

            return null;
        }

        public DialogueSequence GetDialogueSequenceById(int id)
        {
            foreach (DialogueSequence sequence in Sequences)
            {
                if (sequence.Id == id)
                {
                    return sequence;
                }
            }

            return null;
        }

        public Dialogue[] GetDialogues(int sequenceId)
        {
            if (!Dialogues.ContainsKey(sequenceId))
            {
                return Array.Empty<Dialogue>();
            }

            Dialogue[] dialogues = Dialogues[sequenceId];

            return dialogues;
        }

        public DialogueOption[] GetAllowedOptions(int dialogueId)
        {
            if (!DialogueOptions.ContainsKey(dialogueId))
            {
                return Array.Empty<DialogueOption>();
            }

            DialogueOption[] options = DialogueOptions[dialogueId];
            List<DialogueOption> allowedOptions = new List<DialogueOption>();

            foreach (DialogueOption option in options)
            {
                bool passedChecks = IsOptionMeetsRequirements(option) && IsOptionOneTimeUsed(option) && IsOptionPassedCopyDependency(option);
                 
                if (passedChecks)
                {
                    allowedOptions.Add(option);
                }
            }

            return allowedOptions.ToArray();
        }

        public static bool IsOptionMeetsRequirements(DialogueOption option)
        {
            if (option.Requirements != null && option.Requirements.Length > 0)
            {
                foreach (Requirement requirement in option.Requirements)
                {
                    if (!requirement.Check())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsOptionOneTimeUsed(DialogueOption option)
        {
            if (option.UseOnlyOnce && option.TimesUsed >= 1)
            {
                return false;
            }

            return true;
        }


        public static bool IsOptionPassedCopyDependency(DialogueOption option)
        {
            if(option.ExternalDependencyMap != IntPair.MinusOne)
            {
                option.CopyDependencyAttributes();

                if(!option.DependencyCopyPassedFlag)
                {
                    return false;
                }
            }

            return true;
        }

        
    }
}
