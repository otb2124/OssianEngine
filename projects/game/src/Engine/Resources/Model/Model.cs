using Entities;
using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using System.Drawing.Drawing2D;
using Utils;
using static Entities.PhysicalEntity;
using static Graphics.Animation;
using static Resources.ModelFactory;


namespace Resources
{
    public class Model
    {

        public FlatBody Body;
        public StaticSpriteFactory.SpriteData spriteData;

        public AnimationManager aManager;

        public Vector2 bodyOffset;
        public Directions Direction;
        public AnimationStates animationState;
        public ModelStates ModelState;

        public float DrawAngle = 0;


        public Model()
        {
        }
        public Model(ModelPreset preset)
        {
            this.bodyOffset = preset.offset;
            this.Body = FlatBodyFactory.createFlatBody(preset.bodyPreset, this.bodyOffset);
            this.spriteData = preset.spriteData;
            aManager = new AnimationManager();
        }

        public void DrawCollider()
        {
            Color drawColor = new Color((byte)Color.Green.R, (byte)Color.Green.G, (byte)Color.Green.B, (byte)64);

            if (this.Body.BodyShapeType == BodyShapeType.Box)
            {
                Graphics.Graphics.shapes.DrawBoxFill(FlatConverter.ToVector2(Body.Position), Body.Width, Body.Height, Body.Angle, drawColor);
            }
            else
            {
                Graphics.Graphics.shapes.DrawCircleFill(FlatConverter.ToVector2(Body.Position), Body.Radius, 26, drawColor);
            }
        }
        public void Draw()
        {
            //Model
            Rectangle spriteSize = aManager.GetCurrent().GetCurrentFrame();
            float scaleX = 1f;
            float scaleY = 1f;
            Vector2 newPos = new Vector2(Body.Position.X, Body.Position.Y);
            Vector2 textureCenter = new Vector2(spriteSize.Width / 2f, spriteSize.Height / 2f);

            float bodyWidth = Body.Width + bodyOffset.X;
            float bodyHeight = Body.Height + bodyOffset.Y;

            if (Body.BodyShapeType == BodyShapeType.Box)
            {
                scaleX = bodyWidth / spriteSize.Width;
                scaleY = bodyHeight / spriteSize.Height;
                newPos = FlatConverter.ToVector2(Body.Position) - new Vector2(bodyWidth / 2f, bodyHeight / 2f);
                newPos += new Vector2(spriteSize.Width / 2f * scaleX, spriteSize.Height / 2f * scaleY);
            }
            else
            {
                scaleX = Body.Radius / spriteSize.Width * 2;
                scaleY = Body.Radius / spriteSize.Height * 2;
                newPos = FlatConverter.ToVector2(Body.Position) - new Vector2(Body.Radius, Body.Radius);
                newPos += new Vector2(spriteSize.Width / 2f * scaleX, spriteSize.Height / 2f * scaleY);
            }

            aManager.GetCurrent().Draw(newPos, Color.White, DrawAngle, textureCenter, new Vector2(scaleX, scaleY), 0f);
        }

        public void SwapDirection()
        {
            Direction = GetOppositeDirection(Direction);
        }

        public static Directions GetOppositeDirection(Directions direction)
        {
            if(direction == Directions.RIGHT)
            {
                return Directions.LEFT;
            }

            return Directions.RIGHT;
        }

    }
}
