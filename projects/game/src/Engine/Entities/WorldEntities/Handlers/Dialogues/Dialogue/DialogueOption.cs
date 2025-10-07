using System.Linq;
using static Utils.TupleObjectsHelper;

namespace Entities
{
    public enum DialogueOptionActionTypes
    {
        NONE,
        START_DIALOGUE,
        START_TRADE,
        EXIT,
    }

    public class DialogueOption
    {
        public int Id;
        public string Text;

        public DialogueOptionAction[] Actions;

        public Requirement[] Requirements;

        public bool UseOnlyOnce;

        public int TimesUsed;

        public IntPair ExternalDependencyMap;
        public bool DependencyCopyPassedFlag;
        public bool IsCopy;

        public DialogueOption(int id, string text, DialogueOptionAction[] actions = null, Requirement[] requirements = null)
        {
            Id = id;
            Text = text;

            Actions = actions;

            Requirements = requirements;

            TimesUsed = 0;
            ExternalDependencyMap = IntPair.MinusOne;

            IsCopy = false;
        }

        public DialogueOption(int id, IntPair externalDependencyMap)
        {
            Id = id;
            ExternalDependencyMap = externalDependencyMap;

            Text = $"Dependent of {externalDependencyMap}";
            DependencyCopyPassedFlag = false;
            IsCopy = true;
        }

        public void CopyDependencyAttributes()
        {
            DialogueOption[] dependencyCandidates = Entities.DialogueManager.GetDialogue(ExternalDependencyMap.Item1, Entities.DialogueManager.CurrentSequence.Id).GetAllowedOptions();

            DialogueOption matchingOption = dependencyCandidates?.FirstOrDefault(option => option.Id == ExternalDependencyMap.Item2);

            if (matchingOption != null)
            {
                Text = matchingOption.Text;
                Actions = matchingOption.Actions;
                Requirements = matchingOption.Requirements;
                UseOnlyOnce = matchingOption.UseOnlyOnce;
                TimesUsed = matchingOption.TimesUsed;

                DependencyCopyPassedFlag = true;
            }
        }



        public override string ToString()
        {
            return $"Id: {Id}, Text: {Text}, IsCopy: {IsCopy}";
        }
    }

}
