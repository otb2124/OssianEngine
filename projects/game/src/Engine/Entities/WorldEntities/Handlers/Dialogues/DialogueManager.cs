using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DialogueManager
    {

        public Dialogue Current;
        public bool IsDialogueProceeding;

        public Dictionary<int, int> PlayerAnswers;

        public DialogueManager() 
        {
            IsDialogueProceeding = false;
            Current = null;
            PlayerAnswers = new Dictionary<int, int>();
        }

        public void StartDialogue(int newDialogueId)
        {
            if (newDialogueId == -1)
            {
                UI.UI.UIOuterNavigator.RemoveDialogueComponent();
                Current = null;
                IsDialogueProceeding = false;
                return;
            }

            Current = GetDialogueById(newDialogueId);
            Current.SetOptions();
            
            if(!IsDialogueProceeding)
            {
                UI.UI.UIOuterNavigator.ShowDialogueComponent(Current);
                IsDialogueProceeding = true;
            }
            else
            {
                UI.UI.UIOuterNavigator.SetDialogueComponentData(Current);
            }
        }

        public void SetAnswer(int oldDialogueChosenOptionId = -1)
        {
            if (Current != null && oldDialogueChosenOptionId != -1)
            {
                if (PlayerAnswers.ContainsKey(Current.Id))
                {
                    PlayerAnswers[Current.Id] = oldDialogueChosenOptionId;
                }
                else
                {
                    PlayerAnswers.Add(Current.Id, oldDialogueChosenOptionId);
                }
            }
        }

        public static Dialogue GetDialogueById(int id)
        {
            foreach (Dialogue frame in DialogueSetter.AllDialogues)
            {
                if (frame.Id == id)
                {
                    return frame;
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
    }
}
