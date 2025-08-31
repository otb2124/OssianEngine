using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Linq;


namespace Physics
{
    public static class ProjectilePhysicsHandler
    {

        public static Type[] HardSurfaces = new Type[]
        {
            typeof(TileEntity), typeof(DestroyableEntity)
        };

        public static Type[] SoftSurfaces = new Type[]
        {
            typeof(Player), typeof(AnimalMob), typeof(HumanoidMob), typeof(PlatformEntity)
        };

        public static bool CheckProjectileCollision(FlatBody bodyA, FlatBody bodyB)
        {
            Type typeA = bodyA.Owner.GetType();
            Type typeB = bodyB.Owner.GetType();

            if(typeA == typeB)
            {
                return false;
            }

            if(typeA == typeof(ProjectileEntity) && typeB != typeof(ProjectileEntity))
            {
                //battle
                if (bodyB.Owner is BattleEntity toEnt)
                {
                    if (toEnt.Model.OwnerId != ((ProjectileEntity)bodyA.Owner).OwnerID)
                    {
                        if (!toEnt.Stats.IsInvincible)
                        {
                            Entities.BattleHitHandler.HandleHit((BattleEntity)bodyB.Owner, (ProjectileEntity)bodyA.Owner);
                        }
                    }
                }
                return ResolveProjectileBehaviourWithCollider((ProjectileEntity)bodyA.Owner, bodyB.Owner);
            }
            else if(typeB == typeof(ProjectileEntity) && typeA != typeof(ProjectileEntity))
            {
                //battle
                if (bodyA.Owner is BattleEntity toEnt)
                {
                    if(toEnt.Model.OwnerId != ((ProjectileEntity)bodyB.Owner).OwnerID)
                    {
                        Entities.BattleHitHandler.HandleHit((BattleEntity)bodyA.Owner, (ProjectileEntity)bodyB.Owner);
                    }
                }
                return ResolveProjectileBehaviourWithCollider((ProjectileEntity)bodyB.Owner, bodyA.Owner);
            }
            else if(typeA == typeof(ProjectileEntity) && typeB == typeof(ProjectileEntity))
            {
                return ResolveProjectileBehaviourWithCollider((ProjectileEntity)bodyB.Owner, (ProjectileEntity)bodyA.Owner);
            }

            return false;
        }


        public static bool ResolveProjectileBehaviourWithCollider(ProjectileEntity projectile, PhysicalEntity collider)
        {
            if(HardSurfaces.Contains(collider.GetType()))
            {
                return ResolveProjectileCollisionBehaviour(projectile.HardSurfaceBehaviour, projectile, collider);
            }
            else if(SoftSurfaces.Contains(collider.GetType()))
            {
                return ResolveProjectileCollisionBehaviour(projectile.SoftSurfaceBehaviour, projectile, collider);
            }
            else if (collider.GetType() == typeof(ProjectileEntity))
            {
                return ResolveProjectileCollisionBehaviour(projectile.OtherProjectileSurfaceBehaviour, projectile, collider);
            }

            return true;
        }


        public static bool ResolveProjectileCollisionBehaviour(ProjectileEntity.ProjectileCollisionBehaviour behaviour, ProjectileEntity projectile, PhysicalEntity collider)
        {
            if (behaviour == ProjectileEntity.ProjectileCollisionBehaviour.SKIP)
            {
                return true;
            }

            if (behaviour == ProjectileEntity.ProjectileCollisionBehaviour.FALL)
            {
                projectile.MoveDirection = Vector2.Zero;
                projectile.UpdateType = ProjectileEntity.ProjectileUpdateTypes.NONE;
                return false;
            }

            if (behaviour == ProjectileEntity.ProjectileCollisionBehaviour.STICK)
            {
                projectile.MoveDirection = Vector2.Zero;
                projectile.UpdateType = ProjectileEntity.ProjectileUpdateTypes.NONE;
                projectile.Model.Body.IsFrozen = true;
                return false;
            }

            if (behaviour == ProjectileEntity.ProjectileCollisionBehaviour.RICOCHET_VERTICALLY)
            {
                if(projectile.CanRichochet)
                {
                    projectile.MoveDirection = new Vector2(projectile.MoveDirection.X, -projectile.MoveDirection.Y);
                    projectile.CanRichochet = false;
                }
                
                return false;
            }

            if (behaviour == ProjectileEntity.ProjectileCollisionBehaviour.RICOCHET_BOTH)
            {
                if (projectile.CanRichochet)
                {
                    projectile.MoveDirection = new Vector2(-projectile.MoveDirection.X, -projectile.MoveDirection.Y);
                    projectile.CanRichochet = false;
                }

                return false;
            }

            return false;
        }
    }
}
