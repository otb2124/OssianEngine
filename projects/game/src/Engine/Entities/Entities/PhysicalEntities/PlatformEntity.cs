using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System.Collections.Generic;
using static Resources.StaticSpriteFactory;
using Utils;


namespace Entities
{
    public class PlatformEntity : Entity
    {

        public Vector2 Layout;
        public FlatBody Body;
        public AnimationManager[] aManagers;


        public PlatformEntity(Vector2 pos, Vector2 layout, float rot = 0f) : base()
        {
            this.Layout = layout;
            this.Body = FlatBodyFactory.createFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(32*layout.X, 32*layout.Y), 1f, 0.5f);
            Body.MoveTo(FlatConverter.ToFlatVector(pos));
            Body.RotateTo(rot);
            Physics.Physics.flatWorld.AddBody(Body);
            Body.owner = this;

            SpriteData[] data = PlatformCut(Vector2.Zero);
            aManagers = new AnimationManager[data.Length];

            for (int i = 0; i < aManagers.Length; i++)
            {
                aManagers[i] = new AnimationManager();
                aManagers[i].AddStaticAnimation(data[i]);
            }
        }


        public override void DrawCollider()
        {
            Color drawColor = new Color((byte)Color.Green.R, (byte)Color.Green.G, (byte)Color.Green.B, (byte)64);
            Graphics.Graphics.shapes.DrawBoxFill(FlatConverter.ToVector2(Body.Position), Body.Width, Body.Height, Body.Angle, drawColor);
        }


        public static Dictionary<Vector2, int[][]> layoutToIndicies = new()
        {
            {
                //vert flip
                new Vector2(2, 1),
                new int[][]
                {
                    new int[]{ 2, 3 },
                }
            },
            { 
                //vert flip
                new Vector2(3, 1), 
                new int[][]
                {
                    new int[]{ 2, 5, 3 },
                } 
            },
            {
                new Vector2(3, 2),
                new int[][]
                {
                    new int[]{ 0, 4, 1 },
                    new int[]{ 2, 5, 3 },
                }
            },
            {
                new Vector2(3, 3),
                new int[][]
                {
                    new int[]{ 0, 4, 1 },
                    new int[]{ 11, 13, 12 },
                    new int[]{ 2, 5, 3 },
                }
            },
        };

        public override void Draw()
        {
            //0 outer corner left top
            //1 outer corner right top
            //2 outer corner left bottom
            //3 outer corner right bottom
            //4 top border
            //5 bottom border
            //6 inner corner right top
            //7 inner corner left top
            //8 inner corner right bottom
            //9 inner corner left bottom
            //10 pillar top
            //11 left border
            //12 right border
            //13 inner
            //14 inner alt
            //15 pillar bottom

            int[][] indicies = layoutToIndicies[Layout];

            for (int x = 0; x < indicies.Length; x++)
            {
                for (int y = 0; y < indicies[0].Length; y++)
                {
                    aManagers[indicies[x][y]].GetCurrent().Draw(FlatConverter.ToVector2(new FlatVector(Body.Position.X + 32*y, Body.Position.Y + 32*x)), Color.White, this.Body.Angle, new Vector2(Body.Width / 2f, Body.Height/2f), Vector2.One, 0f, true);
                }
            }
        }


    }


}
