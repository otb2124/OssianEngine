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

    public class InteractionData
    {
        public InteractionTriggers Trigger;
        public InteractionActions Action;

        public InteractionDialogueData DialogueSequenceData;

        public InteractionData()
        {
            Trigger = InteractionTriggers.NONE;
            Action = InteractionActions.NONE;
        }

        public InteractionData(InteractionTriggers trigger, InteractionActions action)
        {
            Trigger = trigger;
            Action = action;
        }

        public InteractionData(InteractionTriggers trigger, int[] sequenceIds)
        {
            Trigger = trigger;
            Action = InteractionActions.START_DIALOGUE;

            DialogueSequenceData = new InteractionDialogueData(sequenceIds);
        }
    }
}
