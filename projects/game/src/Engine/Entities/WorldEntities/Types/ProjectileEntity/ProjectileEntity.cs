using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using Utils;

namespace Entities
{

    public enum Projectiles
    {
        NONE,
        FIREBALL,
        ARROW,
    }

    public class ProjectileEntity : StatsEntity
    {


        //add EXPLOSION TIMER
        //low priority GRAVITY (for arrow)
        public enum ProjectileUpdateTypes
        {
            NONE,
            MOVE,
            MOVE_TIMER,
            TIMER,
        };

        //add EXPLOSION
        public enum ProjectileCollisionBehaviour
        {
            NONE,
            SKIP,
            STICK,
            FALL,
            RICOCHET_VERTICALLY,
            RICOCHET_BOTH,
        };

        public Projectiles Type;

        public ProjectileUpdateTypes UpdateType;

        public ProjectileCollisionBehaviour HardSurfaceBehaviour;
        public ProjectileCollisionBehaviour SoftSurfaceBehaviour;
        public ProjectileCollisionBehaviour OtherProjectileSurfaceBehaviour;

        public Vector2 MoveDirection;

        public bool CanRichochet = true;
        public float RicochetCooldownTimer = 0f;

        public BattleHitStatsSet BattleDamageStatsData;

        public int OwnerID;

        public ProjectileEntity(Vector2 pos, Projectiles projectileType, Vector2 direction) : base()
        {
            Type = projectileType;
            MoveDirection = direction;

            SetProjectile(pos);
        }


        public void SetProjectile(Vector2 pos)
        {
            Vector2 bodySize;

            switch (Type)
            {
                case Projectiles.FIREBALL:

                    bodySize = new Vector2(20 * 2, 5 * 2);

                    Model = ModelFactory.CreateModel(StaticSprites.ENTITIES_FIREBALL, FlatBodyFactory.CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, bodySize, 1f, 0f));
                    Model.BodyOffset = new Vector2(0, bodySize.Y / 10f * 32f);
                    Model.Body.MoveTo(FlatConverter.ToFlatVector(pos));
                    Model.Body.RotateTo(0f);
                    Model.UpdatesSurroundingRectangles = false;

                    Physics.Physics.flatWorld.AddBody(Model.Body);
                    Model.Body.Owner = this;
                    Model.OwnerId = Id;

                    SetAnimations();
                    SetSounds();
                    SetStats();

                    UpdateType = ProjectileUpdateTypes.MOVE_TIMER;

                    HardSurfaceBehaviour = ProjectileCollisionBehaviour.RICOCHET_VERTICALLY;
                    SoftSurfaceBehaviour = ProjectileCollisionBehaviour.SKIP;
                    OtherProjectileSurfaceBehaviour = ProjectileCollisionBehaviour.SKIP;

                    break;
                case Projectiles.ARROW:

                    bodySize = new Vector2(20 * 0.75f, 5 * 0.75f);

                    Model = ModelFactory.CreateModel(StaticSprites.ENTITIES_ARROW, FlatBodyFactory.CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, bodySize, 1f, 0f));
                    Model.BodyOffset = new Vector2(0, bodySize.Y / 10f * 32f);
                    Model.Body.MoveTo(FlatConverter.ToFlatVector(pos));
                    Model.Body.RotateTo(0f);
                    Model.UpdatesSurroundingRectangles = false;

                    Physics.Physics.flatWorld.AddBody(Model.Body);
                    Model.Body.Owner = this;
                    Model.OwnerId = Id;

                    SetAnimations();
                    SetSounds();
                    SetStats();

                    UpdateType = ProjectileUpdateTypes.MOVE_TIMER;

                    HardSurfaceBehaviour = ProjectileCollisionBehaviour.RICOCHET_VERTICALLY;
                    SoftSurfaceBehaviour = ProjectileCollisionBehaviour.SKIP;
                    OtherProjectileSurfaceBehaviour = ProjectileCollisionBehaviour.SKIP;

                    break;
                case Projectiles.NONE:
                    break;
            }

            
        }

        public virtual void UpdateProjectileStats(BattleHitStatsSet battleDamageStatsData, int ownerId)
        {
            OwnerID = ownerId;
            BattleDamageStatsData = battleDamageStatsData;
        }


        public override void SetAnimations()
        {
            ModelAppearancePart bodyPart = new ModelAppearancePart(ModelAppearanceParts.BODY);

            bodyPart.AddAnimationSet(AnimationSetSetter.CreateAnimationSetBySpriteSheet(Model.SpriteData.SpriteSheet));

            Model.ModelAppearance.AppearanceParts.Add(bodyPart);
        }

        public override void SetStats()
        {
            base.SetStats();

            UpdatesModelStates = false;

            StatsManager.Stats = new EntityStat[]
            {
                new EntityStat(EntityStats.HP, 5, 5),
                new EntityStat(EntityStats.MOVEMENT_SPEED, 2, 2),
                new EntityStat(EntityStats.JUMP_SPEED, 2.8f, 2.8f, 60),
                new EntityStat(EntityStats.POISE, 100, 100, 3)
            };

            StatsManager.BodyHitStatsSet = new BattleHitStatsSet(new DamageSet(5, 0), new DefenseSet(0, 0), new StatsCostSet(0, 25, 0), 20, 1);

            StatsManager.Abilities = new EntityAbility[]
            {
                new InvincibleFramesAbility(0.5f)
            };

            StatsManager.RefillAll();
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

                Vector2 velocity = normalizedDirection * StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue;
                Model.Body.Move(FlatConverter.ToFlatVector(velocity));

                if (MoveDirection != Vector2.Zero)
                {
                    Model.Body.RotateTo((float)Math.Atan2(MoveDirection.Y, MoveDirection.X));
                    Model.Direction = Directions.RIGHT;
                }
            }

            if (UpdateType == ProjectileUpdateTypes.TIMER || UpdateType == ProjectileUpdateTypes.MOVE_TIMER)
            {
                StatsManager.GetStat(EntityStats.HP).CurrentValue -= 1f * (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
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
