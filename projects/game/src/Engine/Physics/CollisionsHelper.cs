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
            Point modifiedSize = new Point((int)flatBody.Width + GroundingBodySizeOffset, (int)GroundingBodySizeOffset);
            return new Rectangle(new Point((int)flatBody.Position.X - modifiedSize.X / 2, (int)flatBody.Position.Y - (int)flatBody.Height/2 - GroundingBodySizeOffset), modifiedSize);
        }

        public static Rectangle CreateCeilingRectangle(FlatBody flatBody)
        {
            Point modifiedSize = new Point((int)flatBody.Width + GroundingBodySizeOffset, (int)GroundingBodySizeOffset);
            return new Rectangle(new Point((int)flatBody.Position.X - modifiedSize.X / 2, (int)flatBody.Position.Y + (int)flatBody.Height / 2 + GroundingBodySizeOffset), modifiedSize);
        }

        public static FlatBody GetAnyGround(FlatBody flatBody)
        {
            return GetAnyBodyAtRectangleForOtherBody(flatBody, CreateGroundingRectangle(flatBody));
        }

        public static FlatBody GetAnyCeiling(FlatBody flatBody)
        {
            return GetAnyBodyAtRectangleForOtherBody(flatBody, CreateCeilingRectangle(flatBody));
        }

        public static FlatBody GetAnyBodyAtRectangleForOtherBody(FlatBody flatBody, Rectangle rect)
        {
            foreach (FlatBody item in Physics.flatWorld.bodyList)
            {
                //TODO FIX FOR IGNORECOLLISION
                if (item == flatBody)
                {
                    return null;
                }

                Rectangle bodyBBox = new Rectangle(new Point((int)item.Position.X - (int)item.Width / 2, (int)item.Position.Y - (int)item.Height / 2), new Point((int)item.Width, (int)item.Height));

                if (bodyBBox.Intersects(rect))
                {
                    if (CollisionHandler.IgnoreCollision(flatBody, item))
                    {
                        return null;
                    }

                    return item;
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
