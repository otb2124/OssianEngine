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

        public DialogueSequence(int id) 
        {
            Id = id;
        }

        public void SetDialogues()
        {
            Dialogues = Entities.DialogueManager.GetDialogues(Id);
        }

    }
}
