using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using static Resources.StaticSpriteFactory;

namespace Entities
{
    public class PlatformEntity : PhysicalEntity
    {

        public AnimationSet[] aManagers;
        public int[] Indicies;

        public PlatformEntity(Vector2 pos, int width, float rot = 0) : base()
        {
            Indicies = GenerateIndicies(width);
            Model = new Model();
            Model.Body = PhysicalBodyFactory.CreatePhysicalBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(32 * width, 5), 1f, 0.5f);
            Init(pos, rot);
        }

        public void Init(Vector2 pos, float rot)
        {
            Model.Body.MoveTo(PhysicalConverter.ToFlatVector(pos));
            Model.Body.RotateTo(rot);
            Physics.Physics.flatWorld.AddBody(Model.Body);
            Model.Body.Owner = this;

            StaticSprite[] data = PlatformSetCut(Vector2.Zero);
            aManagers = new AnimationSet[data.Length];

            for (int i = 0; i < aManagers.Length; i++)
            {
                aManagers[i] = new AnimationSet(data[i]);
            }
        }


        public int[] GenerateIndicies(int width)
        {

            int[] indices = new int[width];
            for (int x = 0; x < width; x++)
            {
                if(x == 0)
                {
                    indices[x] = 0;
                }
                else if(x < width - 1)
                {
                    indices[x] = 1; 
                }
                else if(x == width - 1)
                {
                    indices[x] = 2;
                }
            }

            return indices;
        }

        public override void DrawCollider()
        {
            Color drawColor = new Color((byte)Color.Green.R, (byte)Color.Green.G, (byte)Color.Green.B, (byte)64);
            Graphics.Graphics.shapes.DrawBoxFill(PhysicalConverter.ToVector2(Model.Body.Position), Model.Body.Width, Model.Body.Height, Model.Body.Angle, drawColor);
        }

        public override void Draw()
        {
            Matrix rotationMatrix = Matrix.CreateRotationZ(Model.Body.Angle);

            for (int y = 0; y < Indicies.Length; y++)
            {
                Vector2 localPos = new Vector2(y * 32, 0);
                Vector2 rotatedPos = Vector2.Transform(localPos, rotationMatrix);
                Vector2 worldPos = new Vector2(PhysicalConverter.ToVector2(Model.Body.Position).X + rotatedPos.X, PhysicalConverter.ToVector2(Model.Body.Position).Y - rotatedPos.Y);

                aManagers[Indicies[y]].DrawCurrent(
                    worldPos,
                    Color.White,
                    this.Model.Body.Angle,
                    new Vector2(Model.Body.Width / 2f, (32-5) / 2f),
                    Vector2.One,
                    0f
                );
            }
        }
    }
}
