using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public static class CollisionsHelper
    {
        public static int GroundingBodySizeOffset = 10;

        public static bool IsBodyOverBody(FlatBody body, FlatBody ground)
        {
            return body.GetAABB().Min.Y <= ground.GetAABB().Min.Y;
        }

        public static Rectangle CreateGroundingRectangle(FlatBody flatBody)
        {
            Point modifiedSize = new Point((int)flatBody.Width + GroundingBodySizeOffset, GroundingBodySizeOffset);
            return new Rectangle(new Point((int)flatBody.Position.X - modifiedSize.X / 2, (int)flatBody.Position.Y - (int)flatBody.Height/2 - GroundingBodySizeOffset), modifiedSize);
        }

        public static Rectangle CreateCeilingRectangle(FlatBody flatBody)
        {
            Point modifiedSize = new Point((int)flatBody.Width + GroundingBodySizeOffset, GroundingBodySizeOffset);
            return new Rectangle(new Point((int)flatBody.Position.X - modifiedSize.X / 2, (int)flatBody.Position.Y + GroundingBodySizeOffset), modifiedSize);
        }

        public static Rectangle CreateSidingRectangle(FlatBody flatBody)
        {
            Point modifiedSize = new Point((int)flatBody.Width + GroundingBodySizeOffset, (int)flatBody.Height);
            return new Rectangle(new Point((int)flatBody.Position.X - modifiedSize.X / 2, (int)flatBody.Position.Y - modifiedSize.Y/2), modifiedSize);
        }

        public static FlatBody GetAnySiding(FlatBody flatBody)
        {
            return GetAnyBodyAtRectangleForOtherBody(flatBody, CreateSidingRectangle(flatBody));
        }

        public static FlatBody GetAnyWalls(FlatBody flatBody)
        {
            FlatBody candidate = GetSpecificEntityTypeBodyAtRectangleForOtherBody(flatBody, CreateSidingRectangle(flatBody), typeof(TileEntity));

            if (candidate != null && ((TileEntity)candidate.Owner).DisableEntityBodyGroundingStatusOnWalls)
            {
                return candidate;
            }

            return null;
        }

        public static FlatBody GetAnyGround(FlatBody flatBody)
        {
            return GetAnyBodyAtRectangleForOtherBody(flatBody, CreateGroundingRectangle(flatBody));
        }

        public static FlatBody GetAnyCeiling(FlatBody flatBody)
        {
            return GetAnyBodyAtRectangleForOtherBody(flatBody, CreateCeilingRectangle(flatBody));
        }

        public static FlatBody GetSpecificEntityTypeBodyAtRectangleForOtherBody(FlatBody flatBody, Rectangle rect, Type type)
        {
            FlatBody candidate = GetAnyBodyAtRectangleForOtherBody(flatBody, rect);
            if (candidate != null && type.IsInstanceOfType(candidate.Owner))
            {
                return candidate;
            }

            return null;
        }

        public static FlatBody GetAnyBodyAtRectangleForOtherBody(FlatBody flatBody, Rectangle rect)
        {
            FlatBody candidate = GetAnyBodyAtRectangle(rect);

            if (candidate != null)
            {
                if (candidate != flatBody && !CollisionHandler.IgnoreCollision(flatBody, candidate))
                {
                    return candidate;
                }
            }
            
            return null;
        }

        public static FlatBody GetAnyBodyAtRectangle(Rectangle rect)
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
        public static bool IsDescending(PhysicalEntity ent)
        {
            if (GetAnyGround(ent.Model.Body) == null)
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
