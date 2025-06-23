using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CSPlatformerSandbox.Engine.Entities.Stats
{
    public class WeaponHitbox : Hitbox
    {

        public RotatedRectangle outerHalf;

        public WeaponHitbox() : base()
        {
            outerHalf = new RotatedRectangle();
        }

        public override void Update(Vector2 pos, Vector2 size)
        {
            base.Update(pos, size);

            this.extends.Height = size.Y*2;

            this.outerHalf.Width = size.X;
            this.outerHalf.Height = size.Y;
            this.outerHalf.Rotation = this.extends.Rotation;
            Vector2 offset = new Vector2(0, size.Y / 2);
            offset = Vector2.Transform(offset, Matrix.CreateRotationZ(this.extends.Rotation));
            this.outerHalf.Center = this.extends.Center - offset;

        }

        public override void Update(Vector2 pos, Vector2 size, float rot)
        {
            base.Update(pos, size);

            this.extends.Height = size.Y * 2;

            this.outerHalf.Width = size.X;
            this.outerHalf.Height = size.Y;
            this.outerHalf.Rotation = this.extends.Rotation;
            Vector2 offset = new Vector2(0, size.Y / 2);
            offset = Vector2.Transform(offset, Matrix.CreateRotationZ(this.extends.Rotation));
            this.outerHalf.Center = this.extends.Center - offset;

        }

        public override void Draw(Color color)
        {
            Color drawColor = new Color((byte)color.R, (byte)color.G, (byte)color.B, (byte)64);
            Graphics.Graphics.shapes.DrawBoxFill(this.outerHalf.Center, outerHalf.Width, outerHalf.Height, outerHalf.Rotation, drawColor);
        }
    }
}
