using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static System.Formats.Asn1.AsnWriter;
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
        public int NextDialogueId;
        public int NextSequenceId;

        public DialogueOptionActionTypes Type;

        public Requirement[] Requirements;

        public bool UseOnlyOnce;

        public int TimesUsed;

        public IntPair ExternalDependencyMap;
        public bool DependencyCopyPassedFlag;
        public bool IsCopy;

        public DialogueOption(int id, string text, int nextDialogueId = -1, int nextDialogueSequenceId = -1, Requirement[] requirements = null)
        {
            Id = id;
            Text = text;

            NextDialogueId = nextDialogueId;
            NextSequenceId = nextDialogueSequenceId;

            Requirements = requirements;

            TimesUsed = 0;
            ExternalDependencyMap = IntPair.MinusOne;

            IsCopy = false;

            SetType();
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
            DialogueOption[] dependencyCandidates = Entities.DialogueManager.GetDialogue(ExternalDependencyMap.Item1, 0).GetCurrentOptions();

            DialogueOption matchingOption = dependencyCandidates?.FirstOrDefault(option => option.Id == ExternalDependencyMap.Item2);

            if (matchingOption != null)
            {
                Text = matchingOption.Text;
                NextDialogueId = matchingOption.NextDialogueId;
                NextSequenceId = matchingOption.NextSequenceId;
                Requirements = matchingOption.Requirements;
                Type = matchingOption.Type;
                UseOnlyOnce = matchingOption.UseOnlyOnce;
                TimesUsed = matchingOption.TimesUsed;

                DependencyCopyPassedFlag = true;
            }
        }


        public void SetType()
        {
            if(NextDialogueId == -1)
            {
                Type = DialogueOptionActionTypes.EXIT;
            }
            else
            {
                Type = DialogueOptionActionTypes.START_DIALOGUE;
            }
        }


        public override string ToString()
        {
            return $"Id: {Id}, Text: {Text}, NextId: {NextDialogueId}, Type: {Type}, IsCopy: {IsCopy}";
        }
    }

}
