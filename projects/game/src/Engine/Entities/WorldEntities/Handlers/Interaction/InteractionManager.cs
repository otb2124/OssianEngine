using Microsoft.Xna.Framework;
using Resources;


namespace Entities
{


    public class InteractionManager
    {
        public InteractionField InteractionField;
        public InteractionEntity InteractionData;

        public InteractionManager()
        {
            InteractionField = new InteractionField();
            InteractionData = new InteractionEntity();
        }

        public InteractionManager(Vector2 interactionFieldSize, InteractionEntity data)
        {
            InteractionField = new InteractionField(interactionFieldSize);
            InteractionData = data;
        }

        public InteractionManager(Vector2 interactionFieldSize)
        {
            InteractionField = new InteractionField(interactionFieldSize);
            InteractionData = new InteractionEntity();
        }

        public InteractionManager(InteractionEntity data)
        {
            InteractionField = new InteractionField();
            InteractionData = data;
        }

        public void Update(Model model)
        {
            InteractionField.Update(model.Body.Position.ToVector2(), new Vector2(model.Body.Width, model.Body.Height), 0);
        }

        public void Draw()
        {
            InteractionField.Draw();
        }
    }
}
