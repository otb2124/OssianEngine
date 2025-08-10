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

        public static bool IgnoreCollision(FlatBody bodyA, FlatBody bodyB)
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


        public static Rectangle CreateGroundingRectangle(FlatBody flatBody)
        {
            Point modifiedSize = new Point((int)flatBody.Width + 10, (int)flatBody.Height + 10);
            return new Rectangle(new Point((int)flatBody.Position.X - modifiedSize.X / 2, (int)flatBody.Position.Y - modifiedSize.Y / 2), modifiedSize);
        }

        //TODO MOVE SOMEWHERE ELSE
        public static FlatBody GetAnyGround(FlatBody flatBody)
        {
            return GetGroundAtRectangleForBody(flatBody, CreateGroundingRectangle(flatBody));
        }

        //TODO MOVE SOMEWHERE ELSE
        public static FlatBody GetGroundAtRectangleForBody(FlatBody flatBody, Rectangle rect)
        {
            foreach (FlatBody item in Physics.flatWorld.bodyList)
            {
                //TODO FIX FOR IGNORECOLLISION
                //|| IgnoreCollision(flatBody, item)
                if (flatBody == item)
                {
                    return null;
                }

                Rectangle bodyBBox = new Rectangle(new Point((int)item.Position.X - (int)item.Width / 2, (int)item.Position.Y - (int)item.Height / 2), new Point((int)item.Width, (int)item.Height));

                if (bodyBBox.Intersects(rect))
                {
                    return item;
                }
            }

            return null;
        }

        //TODO MOVE SOMEWHERE ELSE
        public static FlatBody GetGroundAtRectangle(Rectangle rect)
        {
            foreach (var item in Physics.flatWorld.bodyList)
            {
                Rectangle bodyBBox = new Rectangle(new Point((int)item.Position.X - (int)item.Width / 2, (int)item.Position.Y - (int)item.Height / 2), new Point((int)item.Width, (int)item.Height));
                if (bodyBBox.Intersects(rect))
                {
                    return item;
                }
            }

            return null;
        }

        //TODO MOVE SOMEWHERE ELSE
        public static bool IsDescending(PhysicalEntity ent)
        {
            if (GetAnyGround(ent.Model.Body) == null &&
                (ent.Model.ModelState == ModelStates.JUMPING ||
                 ent.Model.ModelState == ModelStates.JUMPING_AND_MOVING ||
                 ent.Model.ModelState == ModelStates.JUMPING_DESCENDING ||
                 ent.Model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING))
            {
                FlatBody body = ent.Model.Body;

                if (body.Position.Y > ent.HighestJumpY)
                {
                    ent.HighestJumpY = body.Position.Y;
                    return false;
                }
                else if (body.Position.Y < ent.HighestJumpY)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
