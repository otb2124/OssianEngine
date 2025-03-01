using Microsoft.Xna.Framework;
using Physics;
using Resources;
using Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using static Graphics.Animation;
using System;
using Model = Resources.Model;

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

            float frameSpeed = 0.2f;
            model.AddAnimation(Directions.LEFT, AnimationStates.IDLE, 4, new Vector2(0, 0), new Vector2(48, 96), frameSpeed, SpriteEffects.None);
            model.AddAnimation(Directions.RIGHT, AnimationStates.IDLE, 4, new Vector2(0, 96), new Vector2(48, 96), frameSpeed, SpriteEffects.FlipHorizontally);
        }


        public override void Update()
        {
            model.aManager.Update(new Tuple<Directions, AnimationStates>(model.direction, model.animationState));
            model.modelState = Model.ModelStates.IDLE;

            //Debug.WriteLine(stats.HP);

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                model.body.Move(new FlatVector(stats.speed, 0));
                model.modelState = Model.ModelStates.MOVING;
                model.direction = Directions.RIGHT;
            }
            
            if(Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
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

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
