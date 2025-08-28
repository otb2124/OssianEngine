using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using Utils;

namespace Entities
{
    public class ProjectileEntity : StatsEntity
    {

        public enum ProjectileUpdateTypes
        {
            NONE,
            MOVE,
            MOVE_TIMER,
            TIMER,
        };

        public enum ProjectileCollisionBehaviour
        {
            NONE,
            SKIP,
            STICK,
            FALL,
            RICOCHET_VERTICALLY,
            RICOCHET_BOTH,
        };


        public ProjectileUpdateTypes UpdateType;

        public ProjectileCollisionBehaviour HardSurfaceBehaviour;
        public ProjectileCollisionBehaviour SoftSurfaceBehaviour;
        public ProjectileCollisionBehaviour OtherProjectileSurfaceBehaviour;

        public Vector2 MoveDirection;

        public bool CanRichochet = true;
        public float RicochetCooldownTimer = 0f;

        public ProjectileEntity(Vector2 pos, Vector2 bodySize, Vector2 direction) : base()
        {
            Model = ModelFactory.CreateModel(StaticSprites.ENTITIES_FIREBALL, FlatBodyFactory.CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, bodySize, 1f, 0f));
            Model.BodyOffset = new Vector2(0, bodySize.Y/10f*32f);
            Model.Body.MoveTo(FlatConverter.ToFlatVector(pos));
            Model.Body.RotateTo(0f);
            Model.UpdatesSurroundingRectangles = false;

            Physics.Physics.flatWorld.AddBody(Model.Body);
            Model.Body.Owner = this;

            SetAnimations();
            SetSounds();
            SetStats();

            UpdateType = ProjectileUpdateTypes.MOVE_TIMER;

            HardSurfaceBehaviour = ProjectileCollisionBehaviour.RICOCHET_VERTICALLY;
            SoftSurfaceBehaviour = ProjectileCollisionBehaviour.SKIP;
            OtherProjectileSurfaceBehaviour = ProjectileCollisionBehaviour.SKIP;

            MoveDirection = direction;
        }


        public override void SetAnimations()
        {
            float frameSpeed = 0.05f;

            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.IDLE, 4, new Vector2(0, 0), new Vector2(32, 32), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.MOVING, 4, new Vector2(0, 0), new Vector2(32, 32), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.SpriteData, AnimationStates.ROLL, 4, new Vector2(0, 0), new Vector2(32, 32), frameSpeed);
        }

        public override void SetStats()
        {
            base.SetStats();

            CanRegensStamina = false;
            CanUpdateIFrames = false;
            CanFall = false;
            UpdatesModelStates = false;

            Stats.maxHP = 5;
            Stats.maxSpeed = 2f;
            Stats.jumpSpeed = 2.5f;
            Stats.MaxPoise = 100f;
            Stats.PoiseRegenSec = 3;

            Stats.BodyKnockbackPower = 1;
            Stats.BodyDamage = 5;
            Stats.BodyStaminaHitCost = 25;
            Stats.BodyPoiseDamage = 20;

            Stats.Refill();
        }

        public virtual void UpdateProjectile()
        {
            if (UpdateType == ProjectileUpdateTypes.MOVE || UpdateType == ProjectileUpdateTypes.MOVE_TIMER)
            {
                Model.ModelState = ModelStates.MOVING;

                Vector2 normalizedDirection = MoveDirection;
                if (normalizedDirection != Vector2.Zero)
                {
                    normalizedDirection.Normalize();
                }

                Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
                Model.Body.linearVelocity = FlatVector.Zero;

                Vector2 velocity = normalizedDirection * Stats.speed;
                Model.Body.Move(FlatConverter.ToFlatVector(velocity));

                if (MoveDirection != Vector2.Zero)
                {
                    Model.Body.RotateTo((float)Math.Atan2(MoveDirection.Y, MoveDirection.X));
                    Model.Direction = Directions.RIGHT;
                }
            }

            if (UpdateType == ProjectileUpdateTypes.TIMER || UpdateType == ProjectileUpdateTypes.MOVE_TIMER)
            {
                Stats.HP -= 1f * (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
            }

            if (!CanRichochet)
            {
                RicochetCooldownTimer += (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
                if (RicochetCooldownTimer >= 0.1f)
                {
                    CanRichochet = true;
                    RicochetCooldownTimer = 0f;
                }
            }
        }

        public override void Update()
        {
            UpdateProjectile();

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
