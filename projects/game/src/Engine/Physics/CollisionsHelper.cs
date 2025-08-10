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

        public static bool IsBodyOverBody(FlatBody body, FlatBody ground)
        {
            return body.GetAABB().Min.Y <= ground.GetAABB().Min.Y;
        }

        public static Rectangle CreateGroundingRectangle(FlatBody flatBody)
        {
            Point modifiedSize = new Point((int)flatBody.Width + 10, (int)flatBody.Height + 10);
            return new Rectangle(new Point((int)flatBody.Position.X - modifiedSize.X / 2, (int)flatBody.Position.Y - modifiedSize.Y / 2), modifiedSize);
        }

        public static FlatBody GetAnyGround(FlatBody flatBody)
        {
            return GetGroundAtRectangleForBody(flatBody, CreateGroundingRectangle(flatBody));
        }

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
