using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Physics
{
    public class CollisionHandler
    {


        public CollisionHandler() { }

        private static readonly Dictionary<Type, HashSet<Type>> ignoreCollisionTransformation = new()
        {
            { typeof(Player), new() { typeof(PlatformEntity), typeof(HumanoidMob), typeof(AnimalMob)} },
            { typeof(HumanoidMob), new() { typeof(AnimalMob), typeof(HumanoidMob) } },
            { typeof(AnimalMob), new() { typeof(AnimalMob) } },
            { typeof(InteractiveItemEntity), new() { typeof(AnimalMob), typeof(HumanoidMob), typeof(Player), typeof(InteractiveItemEntity) } },

            /*
            { typeof(GroupMember), new() { typeof(GroupMember) } },
            { typeof(TileEntity), new() { typeof(GroupMember) } },
            { typeof(InteractiveItemEntity), new() { typeof(DynamicEntity) } },
            { typeof(LadderEntity), new() { typeof(DynamicEntity) } },
            { typeof(FlatEntity), new() { typeof(FlatEntity) } },
            { typeof(NPC), new() { typeof(NPC), typeof(DynamicEntity) } }
            */
        };

        public static bool IsBodyOverBody(FlatBody body, FlatBody ground)
        {
            return body.GetAABB().Min.Y <= ground.GetAABB().Min.Y;
        }

        public static FlatBody GetGround(FlatBody flatBody)
        {
            Rectangle groundBoxA = new Rectangle(new Point((int)flatBody.Position.X, (int)flatBody.Position.Y - 3), new Point((int)flatBody.Width, 5));

            foreach (var item in Physics.flatWorld.bodyList)
            {
                if(flatBody != item)
                {
                    Rectangle bodyBBox = new Rectangle(new Point((int)item.Position.X - (int)item.Width/2, (int)item.Position.Y), new Point((int)item.Width, (int)item.Height));
                    if(bodyBBox.Intersects(groundBoxA))
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public static bool IsDescending(PhysicalEntity ent)
        {
            if (GetGround(ent.Model.Body) == null)
            {
                FlatBody body = ent.Model.Body;

                //body position is higher
                if (body.Position.Y > ent.heighestJumpY)
                {
                    //body position is getting higher
                    ent.heighestJumpY = body.Position.Y;
                    return false;
                }
                //body position is lower
                else if (body.Position.Y < ent.heighestJumpY)
                {
                    //body position is getting lower
                    ent.heighestJumpY = body.Position.Y;
                    return true;
                }
            }

            ent.heighestJumpY = 0f;
            return false;
        }

        public bool IgnoreCollision(FlatBody bodyA, FlatBody bodyB)
        {
            Type typeA = bodyA.Owner.GetType();
            Type typeB = bodyB.Owner.GetType();


            //platforms
            if (!Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVEDOWNPRESSED])
            {
                if (typeB == typeof(PlatformEntity))
                {
                    return IsBodyOverBody(bodyA, bodyB);
                }
                if (typeA == typeof(PlatformEntity))
                {
                    return IsBodyOverBody(bodyB, bodyA);
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
