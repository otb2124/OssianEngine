using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DialogueOptionAction
    {
        public DialogueOptionAction() { }
        public virtual void Action(DialogueManager manager) { }
    }

    public class RequirementalDialogueOptionAction : DialogueOptionAction
    {
        public Requirement[] Requirements;
        public RequirementalDialogueOptionAction(Requirement[] requirements = null) 
        {
            Requirements = requirements;
        }

        public override void Action(DialogueManager manager) 
        {
            if(!Check()) { return; }
        }

        public virtual bool Check()
        {
            if(Requirements == null) { return true; }

            foreach (Requirement requirement in Requirements)
            {
                if(!requirement.Check(Entities.Player))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
