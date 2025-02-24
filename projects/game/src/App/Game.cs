using Graphics;
using Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using System;
using System.Diagnostics;

namespace App
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        FlatBody body1;
        Color color1 = Color.Red;

        FlatBody body2;
        Color color2 = Color.Blue;

        public Game()
        {
            Graphics.Graphics.graphicsDeviceManager  = new GraphicsDeviceManager(this);
            Graphics.Graphics.graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            //Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;

            const double UpdatesPerSecond = 60d;
            this.TargetElapsedTime = TimeSpan.FromTicks((long)Math.Round((double)TimeSpan.TicksPerSecond / UpdatesPerSecond));
        }

        protected override void Initialize()
        {

            this.Window.Position = new Point(10, 40);

            FlatUtil.SetRelativeBackBufferSize(Graphics.Graphics.graphicsDeviceManager, 0.85f);
            Graphics.Graphics.screen = new Screen(this, 1280, 768);
            Graphics.Graphics.sprites = new Sprites(this);
            Graphics.Graphics.shapes = new Shapes(this);

            Physics.Physics.flatWorld = new FlatWorld();


            GameManager.Init();
            Inputs.Inputs.keyHandler = new KeyHandler(this);

            Graphics.Graphics.camera = new Camera(Graphics.Graphics.screen, this);
            Graphics.Graphics.camera.zoom = 20;

                //ENTITIES
                string errorMsg;
                bool success = FlatBody.CreateBoxBody(10, 10, 1, false, 0, out body1, out errorMsg);
                body1.MoveTo(FlatVector.Zero);
                Physics.Physics.flatWorld.AddBody(body1);

                bool success1 = FlatBody.CreateBoxBody(100, 10, 1, true, 0, out body2, out errorMsg);
                body2.MoveTo(new FlatVector(0, -100));
                Physics.Physics.flatWorld.AddBody(body2);

            Physics.Physics.watch = new Stopwatch();
            Physics.Physics.sampleTimer.Start();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            GameManager.LoadResources();
        }

        protected override void Update(GameTime gameTime)
        {

            Inputs.Inputs.keyboard = FlatKeyboard.Instance;
            Inputs.Inputs.mouse = FlatMouse.Instance;
            Inputs.Inputs.keyboard.Update();
            Inputs.Inputs.mouse.Update();
            Inputs.Inputs.keyHandler.Update();

            FlatWorld.TransformCount = 0;
            FlatWorld.NoTransformCount = 0;

            Physics.Physics.watch.Restart();
            Physics.Physics.flatWorld.Step(FlatUtil.GetElapsedTimeInSeconds(gameTime), 20);
            Physics.Physics.watch.Stop();

            Physics.Physics.totalWorldStepTIme += Physics.Physics.watch.Elapsed.TotalMilliseconds;
            Physics.Physics.totalBodyCount += Physics.Physics.flatWorld.BodyCount;
            Physics.Physics.totalSampleCount++;

            GameManager.Update();

            Graphics.Graphics.camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Graphics.Graphics.screen.Set();
            Graphics.Graphics.shapes.Begin(Graphics.Graphics.camera);

                //ENTITIES
                Graphics.Graphics.shapes.DrawBoxFill(Physics.FlatConverter.ToVector2(body1.Position), body1.Width, body1.Height, body1.Angle, color1);
                Graphics.Graphics.shapes.DrawBoxFill(Physics.FlatConverter.ToVector2(body2.Position), body2.Width, body2.Height, body2.Angle, color2);

            Graphics.Graphics.sprites.Begin(Graphics.Graphics.camera);

            //UIMANAGER
            Graphics.Graphics.sprites.Draw(GameManager.resourceLoader.sprites[0].texture, Vector2.One, new Rectangle(0, 0, 64, 64), Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

            //Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, float layerDepth

            Graphics.Graphics.shapes.End();
            Graphics.Graphics.sprites.End();

            Graphics.Graphics.screen.Unset();
            Graphics.Graphics.screen.Present(Graphics.Graphics.sprites, Color.Black, true);

            base.Draw(gameTime);
        }
    }
}
