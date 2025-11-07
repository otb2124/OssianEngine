using Entities;
using System;
using System.Collections.Generic;

namespace Physics
{
    public static class CollisionHandler
    {

        private static readonly Dictionary<Type, HashSet<Type>> IgnoreCollisionTransformationGeneral = new()
        {
            { typeof(Player), new() { typeof(PlatformEntity), typeof(HumanoidEntity), typeof(AnimalMob), typeof(LedgeEntity), typeof(WaterTileEntity)} },
            { typeof(HumanoidEntity), new() { typeof(AnimalMob), typeof(HumanoidEntity), typeof(WaterTileEntity) } },
            { typeof(AnimalMob), new() { typeof(AnimalMob), typeof(WaterTileEntity) } },
            { typeof(InteractiveItemEntity), new() { typeof(AnimalMob), typeof(HumanoidEntity), typeof(Player), typeof(InteractiveItemEntity), typeof(WaterTileEntity) } },
            { typeof(DestroyableEntity), new () { typeof(WaterTileEntity) } }
        };

        private static readonly Dictionary<Type, HashSet<Type>> IgnoreCollisionTransformationAdditional = new()
        {
            { typeof(Player), new() { typeof(ProjectileEntity)} },
        };

        public static void HandleUnrestrictedCollision(PhysicalBody bodyA, PhysicalBody bodyB)
        {

        }

        public static bool IgnoreCollision(PhysicalBody bodyA, PhysicalBody bodyB, bool additional = false)
        {
            Type typeA = bodyA.Owner.GetType();
            Type typeB = bodyB.Owner.GetType();

            //platforms
            if (!Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVEDOWNPRESSED])
            {
                if (typeB == typeof(PlatformEntity))
                {
                    return CollisionHelper.IsBodyOverBody(bodyA, bodyB);
                }
                if (typeA == typeof(PlatformEntity))
                {
                    return CollisionHelper.IsBodyOverBody(bodyB, bodyA);
                }
            }

            if ((IgnoreCollisionTransformationGeneral.TryGetValue(typeA, out var setA) && setA.Contains(typeB)) ||
                (IgnoreCollisionTransformationGeneral.TryGetValue(typeB, out var setB) && setB.Contains(typeA)))
            {
                return true;
            }

            if(additional)
            {
                if ((IgnoreCollisionTransformationAdditional.TryGetValue(typeA, out var additionalSetA) && additionalSetA.Contains(typeB)) ||
                (IgnoreCollisionTransformationAdditional.TryGetValue(typeB, out var additionalSetB) && additionalSetB.Contains(typeA)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
