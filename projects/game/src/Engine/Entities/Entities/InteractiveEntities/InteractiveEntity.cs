using Microsoft.Xna.Framework;
using Physics;
using Resources;
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

        public InteractiveEntity(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize) : base(sprite, body, pos)
        {
            InteractionFieldSize = interactionFieldSize;
            InteractionField = new Hitbox();
        }

        public InteractiveEntity(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize) : base(spriteData, body, pos)
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

        public virtual void Init(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize)
        {
            InteractionFieldSize = interactionFieldSize;
            InteractionField = new Hitbox();
            base.Init(sprite, body, pos);
        }

        public virtual void Init(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize)
        {
            InteractionFieldSize = interactionFieldSize;
            InteractionField = new Hitbox();
            base.Init(spriteData, body, pos);
        }

        public override void Update()
        {
            InteractionField.Update(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Width + InteractionFieldSize.X, this.Model.body.Height + InteractionFieldSize.Y), 0);
            base.Update();
        }

        public override void Draw()
        {
            Model.DrawAngle = 0;
            this.Model.Draw();
        }


        public virtual void DrawInterractionField()
        {
            InteractionField.Draw(Color.Red);
        }
    }
}
