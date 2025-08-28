using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Entities.BattleMovesetFactory;

namespace Entities
{
    public class ProjectileEntity : BattleEntity
    {

        public enum ProjectileUpdateTypes
        {
            NONE,
            MOVE,
            MOVE_TIMER,
            TIMER,
        };


        public ProjectileUpdateTypes UpdateType;
        public Vector2 MoveDirection;

        public ProjectileEntity(Vector2 pos, Vector2 direction) : base()
        {
            Init(StaticSprites.ENTITIES_FIREBALL, FlatBodyPreset.PROJECTILE, pos);
            SetStats();

            UpdateType = ProjectileUpdateTypes.MOVE_TIMER;
            MoveDirection = direction;
        }


        public override void SetAnimations()
        {
            float frameSpeed = 0.05f;

            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.IDLE, 4, new Vector2(0, 0), new Vector2(32, 32), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.MOVING, 4, new Vector2(0, 0), new Vector2(32, 32), frameSpeed);
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.ROLL, 4, new Vector2(0, 0), new Vector2(32, 32), frameSpeed);
        }

        public override void SetStats()
        {
            base.SetStats();

            CanRegensStamina = false;
            CanUpdateIFrames = false;
            CanFall = false;
            UpdatesModelStates = false;

            Stats.maxHP = 5;
            Stats.maxSpeed = 0.5f;
            Stats.jumpSpeed = 2.5f;
            Stats.MaxPoise = 100f;
            Stats.PoiseRegenSec = 3;

            Stats.BodyKnockbackPower = 1;
            Stats.BodyDamage = 5;
            Stats.BodyStaminaHitCost = 25;
            Stats.BodyPoiseDamage = 20;

            Stats.Refill();
        }

        public override void SetBattleBodies()
        {
            BattleBodyData battleBodyData = new BattleBodyData();
            battleBodyData.Sprite = StaticSprites.NONE;
            battleBodyData.WeaponSwingSpeedMultiplier = 1f;
            battleBodyData.MoveSet = BattleMovesets.BODY_SLIME;
            battleBodyData.WeaponOutAnimationData = new Graphics.AnimationData(1, new Vector2(0, 0), new Vector2(128, 128), 0f);
            battleBodyData.ModelStateBetweenHits = ModelStates.IDLE;

            BattleBodyManager = new BattleBodyManager(BattleBodyTypes.BODY);
            BattleBodyManager.InitBody(0, battleBodyData);
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
