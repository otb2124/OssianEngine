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
            Rectangle spriteSize = sprite.srcRect;
            float scaleX = 1f;
            float scaleY = 1f;
            Vector2 newPos = new Vector2(body.Position.X, body.Position.Y);
            Vector2 textureCenter = new Vector2(spriteSize.Width / 2f, spriteSize.Height / 2f);

            float bodyWidth = body.Width + bodyOffset.X;
            float bodyHeight = body.Height + bodyOffset.Y;

            if (body.BodyShapeType == BodyShapeType.Box)
            {
                scaleX = bodyWidth / spriteSize.Width;
                scaleY = bodyHeight / spriteSize.Height;
                newPos = FlatConverter.ToVector2(body.Position) - new Vector2(bodyWidth / 2f, bodyHeight / 2f);
                newPos += new Vector2(spriteSize.Width / 2f * scaleX, spriteSize.Height / 2f * scaleY);
            }
            else
            {
                scaleX = body.Radius / spriteSize.Width * 2;
                scaleY = body.Radius / spriteSize.Height * 2;
                newPos = FlatConverter.ToVector2(body.Position) - new Vector2(body.Radius, body.Radius);
                newPos += new Vector2(spriteSize.Width / 2f * scaleX, spriteSize.Height / 2f * scaleY);
            }

            sprite.Draw(
                newPos,
                Color.White,
                body.Angle,
                textureCenter,
                new Vector2(scaleX, scaleY),
                SpriteEffects.FlipVertically, 0f);
        }

    }
}
