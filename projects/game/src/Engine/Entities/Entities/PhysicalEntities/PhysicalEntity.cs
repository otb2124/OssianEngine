using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System;
using Utils;

namespace Entities
{
    public class PhysicalEntity : Entity
    {
        
        public Resources.Model Model;

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
            Model = ModelFactory.CreateModel(modelPreset);
            Model.body.MoveTo(FlatConverter.ToFlatVector(pos));
            Model.body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(Model.body);
            Model.body.owner = this;

            this.baseSpriteZ = this.Model.spriteData.z;
            this.spriteZ = baseSpriteZ;

            SetAnimations();
        }

        public virtual void Init(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, float rotation = 0f)
        {
            Model = ModelFactory.CreateModel(sprite, body);
            Model.body.MoveTo(FlatConverter.ToFlatVector(pos));
            Model.body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(Model.body);
            Model.body.owner = this;

            this.baseSpriteZ = this.Model.spriteData.z;
            this.spriteZ = baseSpriteZ;

            SetAnimations();
        }

        public virtual void Init(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, float rotation = 0f)
        {
            Model = ModelFactory.CreateModel(spriteData, body);
            Model.body.MoveTo(FlatConverter.ToFlatVector(pos));
            Model.body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(Model.body);
            Model.body.owner = this;

            this.baseSpriteZ = this.Model.spriteData.z;
            this.spriteZ = baseSpriteZ;

            SetAnimations();
        }

        public virtual void UpdateAnimationState()
        {
            switch (Model.modelState)
            {
                case ModelStates.MOVING:
                    Model.animationState = AnimationStates.MOVING;
                    break;
                case ModelStates.IDLE:
                    Model.animationState = AnimationStates.IDLE;
                    break;
                case ModelStates.JUMPING:
                    Model.animationState = AnimationStates.JUMPING;
                    break;
                case ModelStates.SPRINTING:
                    Model.animationState = AnimationStates.SPRINTING;
                    break;
                case ModelStates.BATTLE_IDLE:
                    Model.animationState = AnimationStates.BATTLE_IDLE;
                    break;
                case ModelStates.BATTLE_MOVING:
                    Model.animationState = AnimationStates.BATTLE_MOVING;
                    break;
                case ModelStates.BATTLE_ROLL:
                    Model.animationState = AnimationStates.BATTLE_ROLL;
                    break;
                case ModelStates.FALLEN:
                    Model.animationState = AnimationStates.FALLEN;
                    break;
            }

            Model.aManager.Update(new Tuple<Directions, AnimationStates>(Model.direction, Model.animationState));
        }


        public override void Update()
        {
            UpdateAnimationState();
            base.Update();
        }


        public virtual void SetAnimations()
        {
            this.Model.aManager.AddStaticAnimation(this.Model.spriteData);
        }

        public override void Draw()
        {
            Model.DrawAngle = Model.body.Angle;
            this.Model.Draw();
        }

        public virtual void DrawCollider()
        {
            this.Model.DrawCollider();
        }
    }
}
