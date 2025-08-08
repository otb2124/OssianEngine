using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Utils;
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
            Point modifiedSize = new Point((int)flatBody.Width + 10, (int)flatBody.Height + 10);

            Rectangle groundBoxA = new Rectangle(new Point((int)flatBody.Position.X - modifiedSize.X / 2, (int)flatBody.Position.Y - modifiedSize.Y / 2), modifiedSize);

            foreach (var item in Physics.flatWorld.bodyList)
            {
                if(flatBody != item)
                {
                    Rectangle bodyBBox = new Rectangle(new Point((int)item.Position.X - (int)item.Width/2, (int)item.Position.Y - (int)item.Height/2), new Point((int)item.Width, (int)item.Height));
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
            if (GetGround(ent.Model.Body) == null &&
                (ent.Model.ModelState == ModelStates.JUMPING ||
                 ent.Model.ModelState == ModelStates.JUMPING_AND_MOVING ||
                 ent.Model.ModelState == ModelStates.JUMPING_DESCENDING ||
                 ent.Model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING))
            {
                FlatBody body = ent.Model.Body;

                // Update highest Y if current position is higher
                if (body.Position.Y > ent.HighestJumpY)
                {
                    ent.HighestJumpY = body.Position.Y;
                    return false; // Still ascending or at peak
                }
                else if (body.Position.Y < ent.HighestJumpY)
                {
                    return true; // Descending
                }
            }

            // Reset highestJumpY only when grounded
            // This is handled in Player.Update to avoid redundancy
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
