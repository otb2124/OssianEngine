using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System.Collections.Generic;


namespace Entities
{
    public class PlatformEntity : Entity
    {
        public enum PlatformPerspective
        {
            RIGHT,
            LEFT,
            BOTH
        }

        public Vector2 layout;
        public FlatBody body;
        public AnimationManager[] aManagers;

        public float baseSpriteZ;
        public float spriteZ;

        public PlatformPerspective perspective = PlatformPerspective.LEFT;


        public PlatformEntity(Vector2 pos, float spriteZ, float layoutX = 3, float layoutY = 2, float rot = 0f) : base()
        {
            this.layout = new Vector2(layoutX, layoutY);
            this.body = FlatBodyFactory.createFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(32*layout.X, 32), 1f, 0.5f);
            body.MoveTo(FlatConverter.ToFlatVector(pos));
            body.RotateTo(rot);
            Physics.Physics.flatWorld.AddBody(body);
            body.owner = this;

            StaticSpriteFactory.SpriteData[] data = StaticSpriteFactory.PlatformCut(Vector2.Zero);
            aManagers = new AnimationManager[data.Length];

            for (int i = 0; i < aManagers.Length; i++)
            {
                aManagers[i] = new AnimationManager();
                aManagers[i].AddStaticAnimation(data[i]);
            }

            baseSpriteZ = spriteZ;
            this.spriteZ = baseSpriteZ;
        }


        public static Dictionary<Vector2, int[][]> platformLayouts = new Dictionary<Vector2, int[][]>
        {
            {
                new Vector2(2, 1), new int[][]
                {
                    new int[] { 0, 2, },
                }
            },
            {
                new Vector2(2, 2), new int[][]
                {
                    new int[] { 0, 2, },
                    new int[] { 8, 10, },
                }
            },
            { 
                new Vector2(3, 1), new int[][]
                {
                    new int[] { 0, 1, 2 },
                }
            },
            { 
                new Vector2(3, 2), new int[][]
                {
                    new int[] { 0, 1, 2 },
                    new int[] { 8, 9, 10 },
                }
            },
            {
                new Vector2(3, 3), new int[][]
                {
                    new int[] { 0, 1, 2 },
                    new int[] { 4, 5, 6 },
                    new int[] { 8, 9, 10 },
                }
            },
            {
                new Vector2(5, 2), new int[][]
                {
                    new int[] { 0, 1, 1, 1, 2 },
                    new int[] { 8, 9, 9, 9, 10 },
                }
            },
            {
                new Vector2(10, 2), new int[][]
                {
                    new int[] { 0, 1, 1, 1, 2 },
                    new int[] { 8, 9, 9, 9, 10 },
                }
            },
        };


        public override void DrawCollider()
        {
            Color drawColor = new Color((byte)Color.Green.R, (byte)Color.Green.G, (byte)Color.Green.B, (byte)64);
            Graphics.Graphics.shapes.DrawBoxFill(FlatConverter.ToVector2(body.Position), body.Width, body.Height, body.Angle, drawColor);
        }

        public override void Draw()
        {
            if (!platformLayouts.ContainsKey(layout))
                return;

            // Adjusted body offset: 16px extra to the left, 30px extra to the right
            Vector2 bodyOffset = new Vector2(16 + 30, 0);

            int[][] currentLayout = platformLayouts[layout];

            Rectangle spriteSize = aManagers[0].GetCurrent().GetCurrentFrame();
            Vector2 scale = Vector2.Zero;
            Vector2 textureCenter = new Vector2(spriteSize.Width / 2f, spriteSize.Height / 2f);

            float bodyWidth = this.body.Width + bodyOffset.X;
            float bodyHeight = this.body.Height + bodyOffset.Y;

            scale.X = bodyWidth / spriteSize.Width;
            scale.Y = bodyHeight / spriteSize.Height;

            int rows = currentLayout.Length;
            int cols = currentLayout[0].Length;
            scale.X /= cols;

            float tileWidth = bodyWidth / cols;
            float tileHeight = bodyHeight;

            Vector2 baseDrawPos = FlatConverter.ToVector2(this.body.Position)
                - new Vector2(bodyWidth / 2f, bodyHeight / 2f)
                + new Vector2(spriteSize.Width / 2f * scale.X, spriteSize.Height / 2f * scale.Y);

            // Apply rotation to base position
            Matrix rotationMatrix = Matrix.CreateRotationZ(this.body.Angle);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int spriteIndex = currentLayout[row][col];
                    if (spriteIndex < 0 || spriteIndex >= aManagers.Length)
                        continue;

                    // Local offset for the tile
                    Vector2 localOffset = new Vector2(
                        col * tileWidth - bodyWidth / 2 + tileWidth / 2,
                        -row * tileHeight - bodyHeight / 2 + tileHeight * 1.25f
                    );

                    // Rotate the tile position correctly
                    Vector2 rotatedPos = Vector2.Transform(localOffset, rotationMatrix) + FlatConverter.ToVector2(this.body.Position);

                    aManagers[spriteIndex].GetCurrent().Draw(rotatedPos, Color.White, this.body.Angle, textureCenter, scale, 0f);
                }
            }
        }


    }


}
