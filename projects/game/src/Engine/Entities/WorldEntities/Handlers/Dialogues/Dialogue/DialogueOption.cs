using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DialogueOptionActionTypes Type;

        public Requirement[] Requirements;

        public bool IsUsedOneTime;
        public int TimesUsed;

        public DialogueOption(int id, string text, int nextDialogueId = -1, bool isUsedOneTime = false, Requirement[] requirements = null) 
        {
            Id = id;
            Text = text;
            NextDialogueId = nextDialogueId;
            Requirements = requirements;

            IsUsedOneTime = isUsedOneTime;
            TimesUsed = 0;

            SetType();
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
            return "Id: " + Id + ", Text: " + Text + ", NextId: " + NextDialogueId + ", Type: " + Type;
        }
    }

}
