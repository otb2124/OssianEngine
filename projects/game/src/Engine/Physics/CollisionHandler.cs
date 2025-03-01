using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Physics
{
    public class CollisionHandler
    {


        public CollisionHandler() { }

        private static readonly Dictionary<Type, HashSet<Type>> ignoreCollisionTransformation = new()
        {
            { typeof(Player), new() { typeof(Mob) } },

            //{ typeof(Mob), new() { typeof(PhysicalEntity), typeof(LivingEntity) } },

            /*
            { typeof(GroupMember), new() { typeof(GroupMember) } },
            { typeof(PlatformEntity), new() { typeof(GroupMember) } },
            { typeof(InterractiveItem), new() { typeof(DynamicEntity) } },
            { typeof(LadderEntity), new() { typeof(DynamicEntity) } },
            { typeof(FlatEntity), new() { typeof(FlatEntity) } },
            { typeof(NPC), new() { typeof(NPC), typeof(DynamicEntity) } }
            */
        };


        public bool IgnoreCollision(FlatBody bodyA, FlatBody bodyB)
        {
            Type typeA = bodyA.owner.GetType();
            Type typeB = bodyB.owner.GetType();

            if ((ignoreCollisionTransformation.TryGetValue(typeA, out var setA) && setA.Contains(typeB)) ||
                (ignoreCollisionTransformation.TryGetValue(typeB, out var setB) && setB.Contains(typeA)))
            {
                return true;
            }

            return false;
        }


        public void Collide(FlatBody bodyA, FlatBody bodyB)
        {
            Type typeA = bodyA.owner.GetType();
            Type typeB = bodyB.owner.GetType();


            //dealDamage on collision
            if (typeA == typeof(Player) && typeB == typeof(Mob) || typeA == typeof(Mob) && typeB == typeof(Player))
            {
                ((LivingEntity)bodyB.owner).sManager.DealDamageTo((LivingEntity)bodyA.owner);
            }
        }

    }
}
