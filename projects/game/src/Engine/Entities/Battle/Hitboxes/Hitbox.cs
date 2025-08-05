using Microsoft.Xna.Framework;
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

        public virtual void Update(Vector2 pos, Vector2 size)
        {
            this.extends.Position = pos;
            this.extends.Width = size.X;
            this.extends.Height = size.Y;
        }

        public virtual void Update(Vector2 pos, Vector2 size, float rot)
        {
            this.extends.Position = pos;
            this.extends.Width = size.X;
            this.extends.Height = size.Y;
            this.extends.Rotation = rot;
        }

        public virtual void Draw(Color color)
        {
            Color drawColor = new Color((byte)color.R, (byte)color.G, (byte)color.B, (byte)64);
            Graphics.Graphics.shapes.DrawBoxFill(this.extends.Position, extends.Width, extends.Height, extends.Rotation, drawColor);
        }
    }
}
