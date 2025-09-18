using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System.Diagnostics;
using Utils;

namespace Entities
{
    public class InteractiveEntity : PhysicalEntity
    {

        public InteractionField InteractionField;

        public InteractiveEntity(Utils.Models modelPreset, Vector2 pos, Vector2 interactionFieldSize) : base(modelPreset, pos)
        {
            InteractionField = new InteractionField(interactionFieldSize);
        }

        public InteractiveEntity(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize) : base(sprite, body, pos)
        {
            InteractionField = new InteractionField(interactionFieldSize);
        }

        public InteractiveEntity(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize) : base(spriteData, body, pos)
        {
            InteractionField = new InteractionField(interactionFieldSize);
        }

        public InteractiveEntity() : base()
        {

        }

        public virtual void Init(Models modelPreset, Vector2 pos, Vector2 interactionFieldSize)
        {
            InteractionField = new InteractionField(interactionFieldSize);
            base.Init(modelPreset, pos);
        }

        public virtual void Init(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize)
        {
            InteractionField = new InteractionField(interactionFieldSize);
            base.Init(sprite, body, pos);
        }

        public virtual void Init(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize)
        {
            InteractionField = new InteractionField(interactionFieldSize);
            base.Init(spriteData, body, pos);
        }

        public override void Update()
        {
            InteractionField.Update(FlatConverter.ToVector2(Model.Body.Position), new Vector2(Model.Body.Width, Model.Body.Height), 0);
            base.Update();
        }

        public override void Draw()
        {
            Model.DrawAngle = 0;
            Model.Draw();
        }


        public virtual void DrawInteractionField()
        {
            InteractionField.Draw();
        }
    }
}
