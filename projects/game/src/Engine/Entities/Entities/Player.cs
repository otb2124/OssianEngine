using Microsoft.Xna.Framework;
using Physics;
using Graphics;
using Microsoft.Xna.Framework.Graphics;
using Utils;
using MathHelper = Utils.MathHelper;
using System.Diagnostics;
using System;

namespace Entities
{
    public class Player : LivingEntity
    {

        public Player(Vector2 pos) : base(Models.PLAYER, pos, 0f)
        {
            
        }

        public override void setStats()
        {
            sManager.stats.maxHP = 100;
            sManager.stats.HP = sManager.stats.maxHP;
            sManager.stats.maxSpeed = 2;
            sManager.stats.speed = sManager.stats.maxSpeed;

            sManager.equipmentManager.weaponL.physDmg = 1;
            sManager.equipmentManager.weaponL.swingSpeed = 0.4f;

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
            //Debug.WriteLine(this.sManager.stats.HP);

            if (KeyHandlerUtil.isPlayerMoving())
            {
                this.UpdateMovement();
            }
            else
            {
                if(!(this.model.modelState == ModelStates.ATTACKING))
                {
                    model.modelState = ModelStates.IDLE;
                }


                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKPRESSED])
                {
                    model.modelState = ModelStates.ATTACKING;
                }
            }

            UpdateHitboxes();

            UpdateAnimationState();
            base.Update();
        }


        public void UpdateMovement()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                model.body.Move(new FlatVector(sManager.stats.speed, 0));
                model.modelState = ModelStates.MOVING;
                model.direction = Directions.RIGHT;
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
            {
                model.body.Move(new FlatVector(-sManager.stats.speed, 0));
                model.modelState = ModelStates.MOVING;
                model.direction = Directions.LEFT;
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
            {
                model.body.Move(new FlatVector(0, 10));
                model.modelState = ModelStates.MOVING;
            }
        }



        public void UpdateHitboxes()
        {

            //weapon
            float horizontalOffset = this.model.direction == Directions.RIGHT ? 10f : -10f;
            float weaponRot = this.model.direction == Directions.RIGHT ? MathHelper.DegreesToRadians(90) : MathHelper.DegreesToRadians(-90);
            Vector2 weaponPosition = FlatConverter.ToVector2(this.model.body.Position) + new Vector2(horizontalOffset, 0);

            
            if (this.model.modelState == ModelStates.ATTACKING)
            {
                this.sManager.equipmentManager.GetCurrentWeapon().hitbox.Update(
                    weaponPosition,
                    new Vector2(this.model.body.Width, this.model.body.Height)
                );


                if (!this.sManager.equipmentManager.GetCurrentWeapon().isSwinging)
                {
                    this.sManager.equipmentManager.GetCurrentWeapon().Swing();
                }

                this.sManager.equipmentManager.GetCurrentWeapon().UpdateSwing(this.model.direction);

                if (!this.sManager.equipmentManager.GetCurrentWeapon().isSwinging)
                {
                    this.model.modelState = ModelStates.IDLE;
                }
            }
            else
            {
                this.sManager.equipmentManager.GetCurrentWeapon().hitbox.Update(
                new Vector2(0,0),
                new Vector2(0, 0)
                );
                this.sManager.equipmentManager.GetCurrentWeapon().isSwinging = false;
            }


            //armor
            this.sManager.equipmentManager.armorHB.Update(
                FlatConverter.ToVector2(this.model.body.Position),
                new Vector2(this.model.body.Width, this.model.body.Height - 20),
                0f
            );
        }


        
        public void UpdateAnimationState()
        {
            switch(model.modelState)
            {
                case ModelStates.MOVING:
                    model.animationState = AnimationStates.MOVING;
                    break;
                case ModelStates.IDLE:
                    model.animationState = AnimationStates.IDLE;
                    break;
            }

            model.aManager.Update(new Tuple<Directions, AnimationStates>(model.direction, model.animationState));
        }

        public override void Draw()
        {
            Debug.WriteLine(spriteZ);

            base.Draw();
        }

        public override void DrawWeapon()
        {
            if (this.model.modelState == ModelStates.ATTACKING)
            {
                sManager.equipmentManager.Draw(this.model.direction);
            }
        }

    }
}
