using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System.Collections.Generic;


namespace Entities
{
    public class PlatformEntity : Entity
    {

        public Vector2 layout;
        public FlatBody body;
        public AnimationManager[] aManagers;


        public PlatformEntity(Vector2 pos, float layoutX = 3, float layoutY = 2, float rot = 0f) : base()
        {
            this.layout = new Vector2(layoutX, layoutY);
            this.body = FlatBodyFactory.createFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(32*layout.X, 32), 1f, 0.5f);
            body.MoveTo(FlatConverter.ToFlatVector(pos));
            Physics.Physics.flatWorld.AddBody(body);
            body.owner = this;

            StaticSpriteFactory.SpriteData[] data = StaticSpriteFactory.PlatformCut(Vector2.Zero);
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
            Graphics.Graphics.shapes.DrawBoxFill(FlatConverter.ToVector2(body.Position), body.Width, body.Height, body.Angle, drawColor);
        }

        public override void Draw()
        {


           aManagers[0].GetCurrent().Draw(FlatConverter.ToVector2(this.body.Position), Color.White, this.body.Angle, Vector2.Zero, Vector2.One, 0f);
            
            
        }


    }


}
