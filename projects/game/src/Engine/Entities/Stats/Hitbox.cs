using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class Hitbox
    {

        public RotatedRectangle extends;

        public Hitbox()
        {
            extends = new RotatedRectangle();
        }

        public void Update(Vector2 pos, Vector2 size)
        {
            this.extends.Center = pos;
            this.extends.Width = size.X;
            this.extends.Height = size.Y;
        }
        public void Update(Vector2 pos, Vector2 size, float rot)
        {
            this.extends.Center = pos;
            this.extends.Width = size.X;
            this.extends.Height = size.Y;
            this.extends.Rotation = rot;
        }

        public void Draw(Color color)
        {
            Color drawColor = new Color((byte)color.R, (byte)color.G, (byte)color.B, (byte)64); // 128 is 50% transparency
            Graphics.Graphics.shapes.DrawBoxFill(this.extends.Center, extends.Width, extends.Height, extends.Rotation, drawColor);
        }
    }
}
