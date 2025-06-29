using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using Utils;

namespace Entities
{
    public class PhysicalEntity : Entity
    {
        
        public Resources.Model model;

        public float baseSpriteZ;
        public float spriteZ;

        public PhysicalEntity(Models modelPreset, Vector2 pos, float rotation = 0f) : base() 
        {
            Init(modelPreset, pos, rotation);
        }

        public PhysicalEntity(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, float rotation = 0f) : base()
        {
            Init(sprite, body, pos, rotation);
        }

        public PhysicalEntity(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, float rotation = 0f) : base()
        {
            Init(spriteData, body, pos, rotation);
        }

        public PhysicalEntity() : base()
        {

        }

        public virtual void Init(Models modelPreset, Vector2 pos, float rotation = 0f)
        {
            model = ModelFactory.CreateModel(modelPreset);
            model.body.MoveTo(FlatConverter.ToFlatVector(pos));
            model.body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(model.body);
            model.body.owner = this;

            this.baseSpriteZ = this.model.spriteData.z;
            this.spriteZ = baseSpriteZ;

            SetAnimations();
        }

        public virtual void Init(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, float rotation = 0f)
        {
            model = ModelFactory.CreateModel(sprite, body);
            model.body.MoveTo(FlatConverter.ToFlatVector(pos));
            model.body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(model.body);
            model.body.owner = this;

            this.baseSpriteZ = this.model.spriteData.z;
            this.spriteZ = baseSpriteZ;

            SetAnimations();
        }

        public virtual void Init(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, float rotation = 0f)
        {
            model = ModelFactory.CreateModel(spriteData, body);
            model.body.MoveTo(FlatConverter.ToFlatVector(pos));
            model.body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(model.body);
            model.body.owner = this;

            this.baseSpriteZ = this.model.spriteData.z;
            this.spriteZ = baseSpriteZ;

            SetAnimations();
        }


        public virtual void SetAnimations()
        {
            this.model.aManager.AddStaticAnimation(this.model.spriteData);
        }

        public override void Draw()
        {
            model.DrawAngle = model.body.Angle;
            this.model.Draw();
        }

        public override void DrawCollider()
        {
            this.model.DrawCollider();
        }
    }
}
