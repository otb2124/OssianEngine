using Microsoft.Xna.Framework;
using Physics;
using Resources;
using Graphics;
using Microsoft.Xna.Framework.Graphics;
using static Graphics.Animation;
using System;
using Model = Resources.Model;
using Utils;
using System.Diagnostics;
using MathHelper = Utils.MathHelper;
using static Entities.PhysicalEntity;

namespace Entities
{
    public class Player : LivingEntity
    {

        public Player(Vector2 pos) : base(ModelFactory.Models.HERO, pos, 0f)
        {
            
        }

        public override void setStats()
        {
            sManager.stats.maxHP = 100;
            sManager.stats.HP = 100;
            sManager.stats.maxSpeed = 1;
            sManager.stats.speed = 1;

            base.setStats();
        }



        public override void SetAnimations()
        {
            model.aManager = new AnimationManager();
            float frameSpeed = 0;
            //idle
            frameSpeed = 0.2f;
            model.aManager.AddAnimation(model.sprite, Directions.LEFT, AnimationStates.IDLE, 4, new Vector2(0, 0), new Vector2(48, 96), frameSpeed, SpriteEffects.FlipHorizontally);
            model.aManager.AddAnimation(model.sprite, Directions.RIGHT, AnimationStates.IDLE, 4, new Vector2(0, 0), new Vector2(48, 96), frameSpeed, SpriteEffects.None);

            //move
            frameSpeed = 0.2f;
            model.aManager.AddAnimation(model.sprite, Directions.LEFT, AnimationStates.MOVING, 4, new Vector2(0, 96), new Vector2(48, 96), frameSpeed, SpriteEffects.FlipHorizontally);
            model.aManager.AddAnimation(model.sprite, Directions.RIGHT, AnimationStates.MOVING, 4, new Vector2(0, 96), new Vector2(48, 96), frameSpeed, SpriteEffects.None);
        }


        public override void Update()
        {
            Debug.WriteLine(this.sManager.stats.HP);

            if (KeyHandlerUtil.isPlayerMoving())
            {
               this.UpdateMovement();
            }
            else
            {
                model.modelState = Model.ModelStates.IDLE;
            }


            UpdateHitboxes();

            UpdateAnimationState();
            base.Update();
        }


        public void UpdateHitboxes()
        {
            float horizontalOffset = this.model.direction == Directions.RIGHT ? 10f : -10f;
            float weaponRot = this.model.direction == Directions.RIGHT ? MathHelper.DegreesToRadians(90) : MathHelper.DegreesToRadians(-90);
            Vector2 weaponPosition = FlatConverter.ToVector2(this.model.body.Position) + new Vector2(horizontalOffset, 0);

            //weapon
            this.sManager.equipmentManager.GetCurrentWeapon().hitbox.Update(
                weaponPosition,
                new Vector2(this.model.body.Width, this.model.body.Height),
                weaponRot
            );

            //armor
            this.sManager.equipmentManager.armorHB.Update(
                FlatConverter.ToVector2(this.model.body.Position),
                new Vector2(this.model.body.Width, this.model.body.Height - 20),
                0f
            );
        }


        public void UpdateMovement()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                model.body.Move(new FlatVector(sManager.stats.speed, 0));
                model.modelState = Model.ModelStates.MOVING;
                model.direction = Directions.RIGHT;
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
            {
                model.body.Move(new FlatVector(-sManager.stats.speed, 0));
                model.modelState = Model.ModelStates.MOVING;
                model.direction = Directions.LEFT;
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
            {
                model.body.Move(new FlatVector(0, 10));
                model.modelState = Model.ModelStates.MOVING;
            }
        }

        public void UpdateAnimationState()
        {
            switch(model.modelState)
            {
                case Model.ModelStates.MOVING:
                    model.animationState = AnimationStates.MOVING;
                    break;
                case Model.ModelStates.IDLE:
                    model.animationState = AnimationStates.IDLE;
                    break;
            }

            model.aManager.Update(new Tuple<Directions, AnimationStates>(model.direction, model.animationState));
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
