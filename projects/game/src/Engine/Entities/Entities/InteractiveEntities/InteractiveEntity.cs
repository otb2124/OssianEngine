using Microsoft.Xna.Framework;
using Physics;
using System.Diagnostics;
using Utils;

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

        public InteractiveEntity() : base()
        {
            
        }

        public virtual void Init(Models modelPreset, Vector2 pos, Vector2 interactionFieldSize)
        {
            InteractionFieldSize = interactionFieldSize;
            InteractionField = new Hitbox();
            base.Init(modelPreset, pos);
        }

        public override void Update()
        {
            InteractionField.Update(FlatConverter.ToVector2(this.model.body.Position), new Vector2(this.model.body.Width + InteractionFieldSize.X, this.model.body.Height + InteractionFieldSize.Y), 0);
            base.Update();
        }

        public override void Draw()
        {
            model.DrawAngle = 0;
            this.model.Draw();
        }


        public override void DrawHitbox()
        {
            InteractionField.Draw(Color.Red);

            base.DrawHitbox();
        }
    }
}
