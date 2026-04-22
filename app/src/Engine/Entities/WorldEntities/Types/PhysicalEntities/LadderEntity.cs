using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public enum Ladders
    {
        LADDER0,
    };


    public class LadderEntity : PhysicalEntity
    {


        public Ladders Type;

        public AnimationSet[] aManagers;
        public int[] Indicies;

        public LadderEntity(Ladders type, Vector2 pos, int height, float rot = 0) : base()
        {
            Type = type;
            Indicies = GenerateIndicies(height);
            Model = new Model();
            Model.Body = PhysicalBodyFactory.CreatePhysicalBody(
                BodyDynamics.STATIC,
                BodyShapeType.Box,
                new Vector2(16, 32 * height),
                1f, 0.5f
            );
            Init(pos, rot);
        }

        public void Init(Vector2 pos, float rot)
        {
            Model.Body.MoveTo(PhysicalConverter.ToPhysicalVector(pos));
            Model.Body.RotateTo(rot);
            Physics.Physics.flatWorld.AddBody(Model.Body);
            Model.Body.Owner = this;

            StaticSprite[] data = StaticSpriteFactory.LadderSetCut(Type);
            aManagers = new AnimationSet[data.Length];

            for (int i = 0; i < aManagers.Length; i++)
            {
                aManagers[i] = new AnimationSet(data[i]);
            }
        }


        public int[] GenerateIndicies(int height)
        {
            int[] indices = new int[height];
            for (int x = 0; x < height; x++)
            {
                if (x == 0)                   
                    indices[x] = 2;
                else if (x < height - 1)
                    indices[x] = 1;
                else
                    indices[x] = 0;
            }
            return indices;
        }

        public override void DrawCollider()
        {
            Color drawColor = new Color((byte)Color.Green.R, (byte)Color.Green.G, (byte)Color.Green.B, (byte)64);
            Graphics.Graphics.Shapes.DrawBoxFill(
                PhysicalConverter.ToVector2(Model.Body.Position),
                Model.Body.Width,
                Model.Body.Height,
                Model.Body.Angle,
                drawColor
            );
        }

        public override void Draw()
        {
            Matrix rotationMatrix = Matrix.CreateRotationZ(Model.Body.Angle);
            Vector2 bodyCenter = PhysicalConverter.ToVector2(Model.Body.Position) - new Vector2(Model.Body.Width, Model.Body.Height/2f);

            for (int y = 0; y < Indicies.Length; y++)
            {
                Vector2 localPos = new Vector2(0, y * 32);
                Vector2 rotatedOffset = Vector2.Transform(localPos, rotationMatrix);
                Vector2 worldPos = bodyCenter + rotatedOffset;

                aManagers[Indicies[y]].DrawCurrent(
                    worldPos,
                    Color.White,
                    Model.Body.Angle,
                    Vector2.Zero,
                    Vector2.One,
                    0f
                );
            }
        }
    }
}
