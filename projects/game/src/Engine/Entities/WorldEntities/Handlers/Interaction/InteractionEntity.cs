using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public enum InteractionTriggers
    {
        NONE,
        AUTO,
        INTERACTION_BUTTON_PRESSED,
    }
    public enum InteractionActions
    {
        NONE,
        ADD_ITEM_TO_INVENTORY,
        START_DIALOGUE,
        ADD_QUEST,
        START_TRADE
    }

    public class InteractionEntity
    {
        public InteractionTriggers Trigger;
        public InteractionActions Action;

        public int DialogueId;

        public InteractionEntity()
        {
            Trigger = InteractionTriggers.NONE;
            Action = InteractionActions.NONE;
        }

        public InteractionEntity(InteractionTriggers trigger, InteractionActions action)
        {
            Trigger = trigger;
            Action = action;
        }

        public InteractionEntity(InteractionTriggers trigger, int dialogueId)
        {
            Trigger = trigger;
            Action = InteractionActions.START_DIALOGUE;

            DialogueId = dialogueId;
        }
    }
}
