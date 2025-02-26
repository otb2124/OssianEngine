using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using SharpDX.Direct3D9;
using Utils;


namespace Resources
{
    public class Model
    {
        public FlatBody body;
        public Sprite sprite;
        public Vector2 bodyOffset;

        public Model(FlatBody body, Sprite sprite)
        {
            this.body = body;
            this.sprite = sprite;
        }

        public void DrawDebug()
        {
            
             if (this.body.BodyShapeType == BodyShapeType.Box)
             {
                Graphics.Graphics.shapes.DrawBoxFill(FlatConverter.ToVector2(body.Position), body.Width, body.Height, body.Angle, Color.Red);
             }
             else
             {
                Graphics.Graphics.shapes.DrawCircleFill(FlatConverter.ToVector2(body.Position), body.Radius, 26, Color.Blue);
             }
            
        }

        public void Draw()
        {
            //model
            float scaleX = 1f;
            float scaleY = 1f;
            Vector2 newPos = new Vector2(body.Position.X, body.Position.Y);
            Vector2 textureCenter = new Vector2(sprite.texture.Width / 2f, sprite.texture.Height / 2f);

            float bodyWidth = body.Width + bodyOffset.X;
            float bodyHeight = body.Height + bodyOffset.Y;

            if (body.BodyShapeType == BodyShapeType.Box)
            {
                scaleX = bodyWidth / sprite.texture.Width;
                scaleY = bodyHeight / sprite.texture.Height;
                newPos = FlatConverter.ToVector2(body.Position) - new Vector2(bodyWidth / 2f, bodyHeight / 2f);
                newPos += new Vector2(sprite.texture.Width / 2f * scaleX, sprite.texture.Height / 2f * scaleY);
            }
            else
            {
                scaleX = body.Radius / sprite.texture.Width * 2;
                scaleY = body.Radius / sprite.texture.Height * 2;
                newPos = FlatConverter.ToVector2(body.Position) - new Vector2(body.Radius, body.Radius);
                newPos += new Vector2(sprite.texture.Width / 2f * scaleX, sprite.texture.Height / 2f * scaleY);
            }

            sprite.Draw(
                newPos,
                new Rectangle(0, 0, sprite.texture.Width, sprite.texture.Height),
                Color.White,
                body.Angle,
                textureCenter,
                new Vector2(scaleX, scaleY),
                SpriteEffects.FlipVertically, 0f);
        }

    }
}
