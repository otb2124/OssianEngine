using Microsoft.Xna.Framework;
using Physics;
using System.Diagnostics;

namespace Entities
{
    public class InteractiveEntity : PhysicalEntity
    {

        public Hitbox InteractionField;
        public Vector2 InteractionFieldSize;

        public InteractiveEntity(Utils.Models modelPreset, Vector2 pos, Vector2 interactionFieldSize) : base(modelPreset, pos)
        {
            InteractionFieldSize = interactionFieldSize;
            InteractionField = new Hitbox();
        }

        public bool PlayerIntersectsInteractionField()
        {
            return false;
        }

        public override void Update()
        {
            InteractionField.Update(FlatConverter.ToVector2(this.model.body.Position), new Vector2(this.model.body.Width + InteractionFieldSize.X, this.model.body.Height + InteractionFieldSize.Y), this.model.body.Angle);

            if (PlayerIntersectsInteractionField())
            {
                Debug.WriteLine("INTERSECTS");
            }

            base.Update();
        }


        public override void DrawHitbox()
        {
            InteractionField.Draw(Color.Red);

            base.DrawHitbox();
        }
    }
}
