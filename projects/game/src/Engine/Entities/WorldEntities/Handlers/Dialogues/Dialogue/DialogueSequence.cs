using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.Design.AxImporter;

namespace Entities
{
    public class DialogueSequence
    {

        public int Id;

        public int InitialDialogueId;
        public Dialogue[] Dialogues;

        public Requirement[] Requirements;

        public int ChoicePriority;

        public DialogueSequence(int id, int initialId, int choicePriority, Dialogue[] dialogues, Requirement[] requirements = null)
        {
            Id = id;

            InitialDialogueId = initialId;
            Dialogues = dialogues;

            Requirements = requirements;
            ChoicePriority = choicePriority;
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

        public override string ToString()
        {
            return $"Id: {Id}, Dialogues: {Dialogues.Length}, InitialDialogueId: {InitialDialogueId}";
        }
    }
}
