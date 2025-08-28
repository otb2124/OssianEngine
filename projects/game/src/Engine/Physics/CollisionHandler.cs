using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Physics
{
    public static class CollisionHandler
    {

        private static readonly Dictionary<Type, HashSet<Type>> ignoreCollisionTransformation = new()
        {
            { typeof(Player), new() { typeof(PlatformEntity), typeof(HumanoidMob), typeof(AnimalMob), typeof(LedgeEntity)} },
            { typeof(HumanoidMob), new() { typeof(AnimalMob), typeof(HumanoidMob) } },
            { typeof(AnimalMob), new() { typeof(AnimalMob) } },
            { typeof(InteractiveItemEntity), new() { typeof(AnimalMob), typeof(HumanoidMob), typeof(Player), typeof(InteractiveItemEntity) } },
            { typeof(ProjectileEntity), new() { typeof(ProjectileEntity), typeof(AnimalMob), typeof(HumanoidMob), typeof(Player), typeof(InteractiveItemEntity) } },
            /*
            { typeof(GroupMember), new() { typeof(GroupMember) } },
            { typeof(TileEntity), new() { typeof(GroupMember) } },
            { typeof(InteractiveItemEntity), new() { typeof(DynamicEntity) } },
            { typeof(LadderEntity), new() { typeof(DynamicEntity) } },
            { typeof(FlatEntity), new() { typeof(FlatEntity) } },
            { typeof(NPC), new() { typeof(NPC), typeof(DynamicEntity) } }
            */
        };

        public static bool IgnoreCollision(FlatBody bodyA, FlatBody bodyB)
        {
            Type typeA = bodyA.Owner.GetType();
            Type typeB = bodyB.Owner.GetType();

            //platforms
            if (!Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVEDOWNPRESSED])
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

            if ((ignoreCollisionTransformation.TryGetValue(typeA, out var setA) && setA.Contains(typeB)) ||
                (ignoreCollisionTransformation.TryGetValue(typeB, out var setB) && setB.Contains(typeA)))
            {
                return true;
            }

            return false;
        }
    }
}
