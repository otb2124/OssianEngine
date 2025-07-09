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
            { typeof(Player), new() { typeof(PlatformEntity), typeof(HumanoidMob), typeof(AnimalMob) } },
            { typeof(HumanoidMob), new() { typeof(AnimalMob), typeof(HumanoidMob) } },
            { typeof(AnimalMob), new() { typeof(AnimalMob) } },

            /*
            { typeof(GroupMember), new() { typeof(GroupMember) } },
            { typeof(TileEntity), new() { typeof(GroupMember) } },
            { typeof(InteractiveItemEntity), new() { typeof(DynamicEntity) } },
            { typeof(LadderEntity), new() { typeof(DynamicEntity) } },
            { typeof(FlatEntity), new() { typeof(FlatEntity) } },
            { typeof(NPC), new() { typeof(NPC), typeof(DynamicEntity) } }
            */
        };


        public bool IgnoreCollision(FlatBody bodyA, FlatBody bodyB)
        {
            Type typeA = bodyA.owner.GetType();
            Type typeB = bodyB.owner.GetType();


            //platforms
            if (!Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVEDOWNPRESSED])
            {
                if (typeB == typeof(PlatformEntity))
                {
                    return bodyA.GetAABB().Min.Y <= bodyB.GetAABB().Min.Y;
                }
                if (typeA == typeof(PlatformEntity))
                {
                    return bodyB.GetAABB().Min.Y <= bodyA.GetAABB().Min.Y;
                }
            }
            


            if ((ignoreCollisionTransformation.TryGetValue(typeA, out var setA) && setA.Contains(typeB)) ||
                (ignoreCollisionTransformation.TryGetValue(typeB, out var setB) && setB.Contains(typeA)))
            {
                return true;
            }

            return false;
        }

    }
}
