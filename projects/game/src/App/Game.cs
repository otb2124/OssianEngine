using Entities;
using Graphics;
using Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System;
using System.Diagnostics;
using Utils;

namespace App
{
    public class Game : Microsoft.Xna.Framework.Game
    {

        ConsoleCommandManager commandManager;

        public Game()
        {
            Graphics.Graphics.graphicsDeviceManager  = new GraphicsDeviceManager(this);
            Graphics.Graphics.graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            IsMouseVisible = false;
            IsFixedTimeStep = true;

            const double UpdatesPerSecond = 60d;
            this.TargetElapsedTime = TimeSpan.FromTicks((long)Math.Round((double)TimeSpan.TicksPerSecond / UpdatesPerSecond));
        }

        protected override void Initialize()
        {

            this.Window.Position = new Point(10, 40);

            Graphics.Graphics.contentManager = Content;
            Graphics.Graphics.contentManager.RootDirectory = "Content";

            ResourceLoader.LoadResources();

            FlatUtil.SetRelativeBackBufferSize(Graphics.Graphics.graphicsDeviceManager, 0.85f);
            Graphics.Graphics.screen = new Screen(this, 1280, 720);
            Graphics.Graphics.sprites = new Sprites(this);
            Graphics.Graphics.shapes = new Shapes(this);

            Physics.Physics.flatWorld = new FlatWorld();
            Physics.Physics.collisionHandler = new CollisionHandler();

            Inputs.Inputs.keyHandler = new KeyHandler();

            Graphics.Graphics.camera = new Camera(Graphics.Graphics.screen, this);
            
            //ENTITIES
            Entities.Entities.Init();
            Graphics.Graphics.Init();

            UI.UI.Init();

            Physics.Physics.watch = new Stopwatch();
            Physics.Physics.sampleTimer.Start();

            GameStateManager.SetDefault();

            commandManager = new ConsoleCommandManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Entities.Entities.entityMapManager.ChangeMap(0, new Vector2(0, 20));
        }

        protected override void Update(GameTime gameTime)
        {
            GameStateManager.CheckGameStatusChange();

            Graphics.Graphics.gameTime = gameTime;
            Inputs.Inputs.Update();
            Physics.Physics.Update();
            UI.UI.UIManager.Update();
            Entities.Entities.entityManager.Update();
            Entities.Entities.eventManager.Update();

            commandManager.ProcessCommands();

            Graphics.Graphics.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Graphics.Graphics.Draw();

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            //commandManager.Dispose();
            base.OnExiting(sender, args);
        }
    }
}
