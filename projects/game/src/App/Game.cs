using Graphics;
using Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System;
using System.Diagnostics;

namespace App
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        
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

            ResourceLoader.LoadResources();

            FlatUtil.SetRelativeBackBufferSize(Graphics.Graphics.graphicsDeviceManager, 0.85f);
            Graphics.Graphics.screen = new Screen(this, 1280, 768);
            Graphics.Graphics.sprites = new Sprites(this);
            Graphics.Graphics.shapes = new Shapes(this);

            Physics.Physics.flatWorld = new FlatWorld();

            Inputs.Inputs.keyHandler = new KeyHandler();

            Graphics.Graphics.camera = new Camera(Graphics.Graphics.screen, this);
            Graphics.Graphics.camera.zoom = 20;

            //ENTITIES
            Entities.Entities.Init();

            Physics.Physics.watch = new Stopwatch();
            Physics.Physics.sampleTimer.Start();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            Graphics.Graphics.gameTime = gameTime;
            Inputs.Inputs.Update();
            Physics.Physics.Update();
            Entities.Entities.entityManager.Update();

            Graphics.Graphics.camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Graphics.Graphics.Draw();

            base.Draw(gameTime);
        }
    }
}
