using Microsoft.Xna.Framework;
using Resources;


namespace Entities
{


    public enum InteractionTriggers
    {
        NONE,
        AUTO,
        INTERACTION_BUTTON_PRESSED,
    }

    public class NPCInteractionManager
    {
        public static Vector2 DEFAULT_INTERACTIONFIELD_SIZE = new Vector2(30, 30);

        public enum NPCInteractionTypes
        {
            NONE,
            DIALOGUE,
            ADD_QUEST,
            TRADE
        }


        public InteractionField InteractionField;

        public InteractionTriggers InteractionTrigger;
        public NPCInteractionTypes InteractionType;

        public NPCInteractionManager(NPCInteractionTypes interactionType, InteractionTriggers interactionTrigger)
        {
            InteractionField = new InteractionField(DEFAULT_INTERACTIONFIELD_SIZE);
            InteractionType = interactionType;
            InteractionTrigger = interactionTrigger;
        }

        public NPCInteractionManager()
        {
        }

        public void Update(Model model)
        {
            InteractionField.Update(model.Body.Position.ToVector2(), new Vector2(model.Body.Width, model.Body.Height), 0);
        }
    }
}
