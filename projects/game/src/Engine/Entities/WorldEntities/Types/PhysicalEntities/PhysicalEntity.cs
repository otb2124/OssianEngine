using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using Utils;
using Model = Resources.Model;

namespace Entities
{
    public class PhysicalEntity : WorldEntity
    {
        
        public Resources.Model Model;

        public float baseSpriteZ;
        public float spriteZ;

        public bool UpdatesSurroundingRectangles = true;

        

        public LightSource.LightSourceData Emission;
        public bool IsWall = false;

        public Dictionary<EntitySounds, Resources.Sounds[]> soundSet;


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

        public virtual void Init(Models modelPreset, Vector2 pos, float rotation = 0f, Directions initDirection = Directions.LEFT)
        {
            Model = ModelFactory.CreateModel(modelPreset);
            Model.Body.MoveTo(FlatConverter.ToFlatVector(pos));
            Model.Body.RotateTo(rotation);
            Model.UpdatesSurroundingRectangles = UpdatesSurroundingRectangles;
            Physics.Physics.flatWorld.AddBody(Model.Body);
            Model.Body.Owner = this;
            Model.OwnerId = Id;
            Model.Direction = initDirection;

            baseSpriteZ = Model.SpriteData.Z;
            spriteZ = baseSpriteZ;

            SetAnimations();
            SetEmission();
            SetSounds();
        }

        public virtual void Init(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, float rotation = 0f, Directions initDirection = Directions.LEFT)
        {
            Model = ModelFactory.CreateModel(sprite, body);
            Model.Body.MoveTo(FlatConverter.ToFlatVector(pos));
            Model.Body.RotateTo(rotation);
            Model.UpdatesSurroundingRectangles = UpdatesSurroundingRectangles;
            Physics.Physics.flatWorld.AddBody(Model.Body);
            Model.Body.Owner = this;
            Model.OwnerId = Id;
            Model.Direction = initDirection;

            this.baseSpriteZ = this.Model.SpriteData.Z;
            this.spriteZ = baseSpriteZ;

            SetAnimations();
            SetEmission();
            SetSounds();
        }

        public virtual void Init(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, float rotation = 0f)
        {
            Model = ModelFactory.CreateModel(spriteData, body);
            Model.Body.MoveTo(FlatConverter.ToFlatVector(pos));
            Model.Body.RotateTo(rotation);
            Model.UpdatesSurroundingRectangles = UpdatesSurroundingRectangles;
            Physics.Physics.flatWorld.AddBody(Model.Body);
            Model.Body.Owner = this;
            Model.OwnerId = Id;

            this.baseSpriteZ = this.Model.SpriteData.Z;
            this.spriteZ = baseSpriteZ;

            SetAnimations();
            SetEmission();
        }


        public virtual void SetEmission(){ }

        public virtual void SetSounds() 
        {
            soundSet = new()
            {
                { EntitySounds.RECEIVEDAMAGE, new Resources.Sounds[] { Resources.Sounds.NONE } },
                { EntitySounds.STEP, new Resources.Sounds[] { Resources.Sounds.NONE } },
                { EntitySounds.JUMP, new Resources.Sounds[] { Resources.Sounds.NONE } }
            };
        }

        public void PlayEntitySound(EntitySounds sound, float timeSec)
        {
            Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(Id, soundSet[sound][RandomHelper.RandomInteger(0, soundSet[sound].Length)], Model.Body.Position.ToVector2(), timeSec));
        }

        public virtual void UpdateSoundState()
        {
            switch (Model.ModelState)
            {
                case ModelStates.MOVING:
                    PlayEntitySound(EntitySounds.STEP, 0.25f);
                    break;
                case ModelStates.IDLE:
                    //
                    break;
                case ModelStates.JUMPING:
                    PlayEntitySound(EntitySounds.JUMP, 1f);
                    break;
                case ModelStates.SPRINTING:
                    PlayEntitySound(EntitySounds.STEP, 0.2f);
                    break;
                case ModelStates.WEAPON_OUT_IDLE:
                    //
                    break;
                case ModelStates.WEAPON_OUT_MOVING:
                    PlayEntitySound(EntitySounds.STEP, 0.25f);
                    break;
                case ModelStates.ROLLING:
                    //
                    break;
                case ModelStates.FALLEN:
                    //
                    break;
                case ModelStates.FALLING:
                    //
                    break;


                //play smth like aaaah power scream
                case ModelStates.ATTACKING_LIGHT:
                    //PlayEntitySound(EntitySounds.WEAPON_SWING, 0.5f);
                    break;
                case ModelStates.ATTACKING_HEAVY:
                    //PlayEntitySound(EntitySounds.WEAPON_SWING, 0.5f);
                    break;
            }

        }

        public virtual void UpdateAnimationState()
        {
            Model.AnimationState = Model.ModelStateToAnimationState(Model.ModelState, Model.AnimationState);
            Model.UpdateAppearance();
        }

        public override void Update()
        {
            UpdateAnimationState();
            UpdateSoundState();
            base.Update();
        }

        public virtual void SetAnimations()
        {
            //Model.AManagers = new List<AnimationSet>();

            Model.ModelAppearance = new ModelAppearance();
            ModelAppearancePart bodyPart = new ModelAppearancePart(ModelAppearanceParts.BODY);
            bodyPart.AddAnimationSet(new AnimationSet(Model.SpriteData));
            Model.ModelAppearance.AppearanceParts.Add(bodyPart);
        }

        public override void Draw()
        {
            Model.DrawAngle = Model.Body.Angle;
            Model.Draw();
        }

        public virtual void DrawCollider()
        {
            Model.DrawCollider();
        }
    }
}
