using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System.Diagnostics;
using Utils;

namespace Entities
{
    public class InteractiveEntity : PhysicalEntity
    {

        public InteractionManager InteractionManager;

        public InteractiveEntity(Utils.Models modelPreset, Vector2 pos, Vector2 interactionFieldSize) : base(modelPreset, pos)
        {
            InteractionManager = new InteractionManager(interactionFieldSize);
        }

        public InteractiveEntity(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize) : base(sprite, body, pos)
        {
            InteractionManager = new InteractionManager(interactionFieldSize);
        }

        public InteractiveEntity(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize) : base(spriteData, body, pos)
        {
            InteractionManager = new InteractionManager(interactionFieldSize);
        }

        public InteractiveEntity() : base()
        {

        }

        public virtual void Init(Models modelPreset, Vector2 pos, Vector2 interactionFieldSize, InteractionData interactionData)
        {
            InteractionManager = new InteractionManager(interactionFieldSize, interactionData);
            base.Init(modelPreset, pos);
        }

        public virtual void Init(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize, InteractionData interactionData)
        {
            InteractionManager = new InteractionManager(interactionFieldSize, interactionData);
            base.Init(sprite, body, pos);
        }

        public virtual void Init(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize, InteractionData interactionData)
        {
            InteractionManager = new InteractionManager(interactionFieldSize, interactionData);
            base.Init(spriteData, body, pos);
        }

        public override void Update()
        {
            InteractionManager.Update(Model);
            base.Update();
        }

        public override void Draw()
        {
            Model.DrawAngle = 0;
            Model.Draw();
        }


        public virtual void DrawInteractionField()
        {
            InteractionManager.Draw();
        }
    }
}
