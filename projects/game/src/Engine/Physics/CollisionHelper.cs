using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Physics
{
    public static class CollisionHelper
    {
        public static int GroundingBodySizeOffset = 10;

        public static bool IsBodyOverBody(PhysicalBody body, PhysicalBody ground)
        {
            return body.GetAABB().Min.Y <= ground.GetAABB().Min.Y;
        }

        public static RotatedRectangle CreateGroundingRectangle(PhysicalBody flatBody)
        {
            Vector2 modifiedSize = new Vector2(flatBody.Width + GroundingBodySizeOffset, GroundingBodySizeOffset);
            return new RotatedRectangle(new Vector2(flatBody.Position.X, flatBody.Position.Y - flatBody.Height/ 2 - GroundingBodySizeOffset / 2), modifiedSize, flatBody.Angle);
        }

        public static RotatedRectangle CreateCeilingRectangle(PhysicalBody flatBody)
        {
            Vector2 modifiedSize = new Vector2(flatBody.Width + GroundingBodySizeOffset, GroundingBodySizeOffset);
            return new RotatedRectangle(new Vector2(flatBody.Position.X, flatBody.Position.Y + flatBody.Height/2 - GroundingBodySizeOffset/2), modifiedSize, flatBody.Angle);
        }

        public static RotatedRectangle CreateSidingRectangle(PhysicalBody flatBody)
        {
            Vector2 modifiedSize = new Vector2(flatBody.Width + GroundingBodySizeOffset, flatBody.Height/2);
            return new RotatedRectangle(new Vector2(flatBody.Position.X, flatBody.Position.Y), modifiedSize, flatBody.Angle);
        }


        public static PhysicalBody GetAnyWalls(Resources.Model model)
        {
            PhysicalBody candidate = GetAnyBodyAtRectangleForOtherBody(model.Body, model.SidingRectangle);

            if (candidate != null && candidate.Owner.IsWall)
            {
                return candidate;
            }

            return null;
        }

        public static PhysicalBody GetAnyGround(Resources.Model model)
        {
            return GetAnyBodyAtRectangleForOtherBody(model.Body, model.GroundingRectangle);
        }

        public static PhysicalBody GetAnyCeiling(Resources.Model model)
        {
            return GetAnyBodyAtRectangleForOtherBody(model.Body, model.CeilingRectangle);
        }

        public static PhysicalBody GetSpecificEntityTypeBodyAtRectangleForOtherBody(PhysicalBody flatBody, RotatedRectangle rect, Type type)
        {
            PhysicalBody candidate = GetAnyBodyAtRectangleForOtherBody(flatBody, rect);
            if (candidate != null && type.IsInstanceOfType(candidate.Owner))
            {
                return candidate;
            }

            return null;
        }

        public static PhysicalBody GetAnyBodyAtRectangleForOtherBody(PhysicalBody flatBody, RotatedRectangle rect)
        {
            foreach (var item in Physics.flatWorld.bodyList)
            {
                RotatedRectangle bodyBBox = item.ToRectangle();
                if (bodyBBox.Intersects(rect))
                {
                    if (item != null)
                    {
                        if (item != flatBody && !CollisionHandler.IgnoreCollision(flatBody, item, true))
                        {
                            return item;
                        }
                    }
                }
            }
            
            return null;
        }

        public static PhysicalBody GetAnyBodyAtRectangle(RotatedRectangle rect)
        {
            foreach (var item in Physics.flatWorld.bodyList)
            {
                RotatedRectangle bodyBBox = item.ToRectangle();
                if (bodyBBox.Intersects(rect))
                {
                    return item;
                }
            }

            return null;
        }

        public static LedgeEntity GetAnyLedges(PhysicalBody body)
        {
            foreach (PhysicalBody item in Physics.flatWorld.bodyList)
            {
                if(item.Owner is LedgeEntity)
                {
                    RotatedRectangle bodyBBox = body.ToRectangle();
                    RotatedRectangle itemBox = item.ToRectangle();

                    if (bodyBBox.Intersects(itemBox))
                    {
                        return (LedgeEntity)item.Owner;
                    }
                }
                
            }

            return null;
        }
    }
}
