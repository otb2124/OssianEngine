using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Design.AxImporter;

namespace Entities
{
    public class DialogueSequence
    {

        public int Id;
        public Dialogue[] Dialogues;

        public DialogueSequence(int id, Dialogue[] dialogues)
        {
            Id = id;
            Dialogues = dialogues;
        }

        public Dialogue GetDialogueById(int id)
        {
            foreach (Dialogue dialogue in Dialogues)
            {
                if (dialogue.Id == id)
                {
                    return dialogue;
                }
            }

            return null;
        }

    }
}
