using System;
using System.Collections.Generic;
using System.Linq;
using static Utils.TupleObjectsHelper;

namespace Entities
{
    public class DialogueManager
    {
        public DialogueSequence[] Sequences;

        public DialogueSequence CurrentSequence;
        public int CurrentDialogueId;

        public List<DialogueAnswer> AllAnswers;

        public DialogueManager()
        {
            Sequences = DialogueSetter.Sequences;

            AllAnswers = new List<DialogueAnswer>();
            CurrentDialogueId = -1;
        }

        public void SetDialogue(DialogueOptionAction[] dops)
        {
            foreach (DialogueOptionAction dop in dops)
            {
                ProcessDOP(dop);
            }
        }

        public void ProcessDOP(DialogueOptionAction dop)
        {
            //case to end
            if (dop is ExitDialogueDOP)
            {
                CurrentDialogueId = -1;
                RemoveUIDialogueComponent();
            }

            //case to start
            if (dop is StartSequenceDOP startSequenceDOP)
            {
                if(GetSequence(startSequenceDOP.SequenceId) != null)
                {
                    CurrentSequence = GetSequence(startSequenceDOP.SequenceId);
                    CurrentDialogueId = CurrentSequence.InitialDialogueId;
                    UpdateSequence(new NextDialogueDOP(CurrentDialogueId));
                    InitializeUIDialogueComponent();
                }
                else
                {
                    return;
                }
            }
            
            //case to next dialogue
            if(dop is NextDialogueDOP nextDop)
            {
                UpdateSequence(nextDop);
                UpdateUIDialogueComponent();
            }

            //case to set intial dialogue for sequence
            if (dop is SetInitialDialogueForSequenceDOP setInitDialogueForSequenceDOP)
            {
                //UpdateSequence(nextDop);
                //UpdateUIDialogueComponent();
                GetSequence(setInitDialogueForSequenceDOP.SequenceId).InitialDialogueId = setInitDialogueForSequenceDOP.DialogueId;
            }

            if(dop is DisableDialogueSequenceDOP disableSequenceDOP)
            {
                GetSequence(disableSequenceDOP.SequenceId).Disabled = true;
            }
        }

        public void UpdateSequence(NextDialogueDOP nextDialogueDOP)
        {
            CurrentDialogueId = nextDialogueDOP.DialogueId;
            CurrentSequence.GetDialogueById(CurrentDialogueId).SetCurrentOptions();
            CurrentSequence.GetDialogueById(CurrentDialogueId).TimesRead++;
        }

        public void InitializeUIDialogueComponent()
        {
            UI.UI.UIOuterNavigator.ShowDialogueComponent(CurrentSequence.GetDialogueById(CurrentDialogueId));
        }

        public void UpdateUIDialogueComponent()
        {
            UI.UI.UIOuterNavigator.SetDialogueComponentData(CurrentSequence.GetDialogueById(CurrentDialogueId));
        }

        public void RemoveUIDialogueComponent()
        {
            UI.UI.UIOuterNavigator.RemoveDialogueComponent();
        }

        public void SetAnswer(int oldDialogueChosenOptionId)
        {
            if (CurrentDialogueId != -1 && oldDialogueChosenOptionId != -1)
            {
                DialogueAnswer answer = new DialogueAnswer(CurrentDialogueId, oldDialogueChosenOptionId);
                AllAnswers.Add(answer);

            }

            //TimesUsed logic
            DialogueOption oldDialogueOption = GetDialogueOption(oldDialogueChosenOptionId, CurrentDialogueId, CurrentSequence.Id);// GetDialogue(CurrentDialogueId, 0).CurrentOptions[optionUIId];

            if (oldDialogueOption.ExternalDependencyMap != IntPair.MinusOne)
            {
                DialogueOption dependencyOption = GetDialogueOption(oldDialogueOption.ExternalDependencyMap.Item2, oldDialogueOption.ExternalDependencyMap.Item1, CurrentSequence.Id);
                dependencyOption.TimesUsed++;
            }

            oldDialogueOption.TimesUsed++;
        }

        public DialogueSequence GetSequence(int id)
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

        public Dialogue GetDialogue(int dialogueId, int sequenceId)
        {
            return GetSequence(sequenceId).GetDialogueById(dialogueId);
        }

        public DialogueOption GetDialogueOption(int optionId, int dialogueId, int sequenceId)
        {
            return GetSequence(sequenceId).GetDialogueById(dialogueId).GetOptionById(optionId);
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

        public DialogueAnswer[] GetAllDialogueAnswers(int dialogueId)
        {
            List<DialogueAnswer> answers = new List<DialogueAnswer>();

            foreach (DialogueAnswer answer in AllAnswers)
            {
                if(answer.Data.Item1 == dialogueId)
                {
                    answers.Add(answer);
                }
            }

            return answers.ToArray();
        }

        public DialogueAnswer GetDialogueAnswer(int dialogueId, int optionIdAnswer)
        {
            foreach (DialogueAnswer answer in GetAllDialogueAnswers(dialogueId))
            {
                if(answer.Data.Item2 == optionIdAnswer)
                {
                    return answer;
                }
            }

            return null;
        }

        public DialogueOption[] GetAllDependentOptions(int dialogueId, int optionId)
        {
            List<DialogueOption> dependentOptions = new List<DialogueOption>();

            foreach (DialogueSequence sequence in Sequences)
            {
                foreach (Dialogue dialogue in sequence.Dialogues)
                {
                    foreach (DialogueOption option in dialogue.Options)
                    {
                        if (option.ExternalDependencyMap != IntPair.MinusOne &&
                        option.ExternalDependencyMap.Item1 == dialogueId &&
                        option.ExternalDependencyMap.Item2 == optionId)
                        {
                            dependentOptions.Add(option);
                        }
                    }
                }
            }

            return dependentOptions.ToArray();
        }

        public int GetDialogueIdByOptionId(int optionId)
        {
            for (global::System.Int32 i = 0; i < CurrentSequence.Dialogues.Length; i++)
            {
                foreach (DialogueOption option in CurrentSequence.Dialogues[i].Options)
                {
                    if (option.Id == optionId)
                    {
                        return CurrentSequence.Dialogues[i].Id;
                    }
                }
            }
            return -1;
        }
    }
}
