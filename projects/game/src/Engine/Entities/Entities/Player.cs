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

namespace Entities
{
    public class Player : LivingEntity
    {

        public Player(Vector2 pos) : base(ModelFactory.Models.HERO, pos, 0f)
        {
            
        }

        public override void setStats()
        {
            stats.maxHP = 100;
            stats.HP = 100;
            stats.dmg = 1;
            stats.maxSpeed = 1;
            stats.speed = 1;

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

            if (KeyHandlerUtil.isPlayerMoving())
            {
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
                {
                    model.body.Move(new FlatVector(stats.speed, 0));
                    model.modelState = Model.ModelStates.MOVING;
                    model.direction = Directions.RIGHT;
                }

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
                {
                    model.body.Move(new FlatVector(-stats.speed, 0));
                    model.modelState = Model.ModelStates.MOVING;
                    model.direction = Directions.LEFT;
                }

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
                {
                    model.body.Move(new FlatVector(0, 10));
                    model.modelState = Model.ModelStates.MOVING;
                }
            }
            else
            {
                model.modelState = Model.ModelStates.IDLE;
            }

            UpdateAnimationState();
            base.Update();
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
