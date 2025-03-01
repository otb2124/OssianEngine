using Graphics;
using Inputs;
using Microsoft.Xna.Framework;
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
            Graphics.Graphics.screen = new Screen(this, 1280, 720);
            Graphics.Graphics.sprites = new Sprites(this);
            Graphics.Graphics.shapes = new Shapes(this);

            Physics.Physics.flatWorld = new FlatWorld();
            Physics.Physics.collisionHandler = new CollisionHandler();

            Inputs.Inputs.keyHandler = new KeyHandler();

            Graphics.Graphics.camera = new Camera(Graphics.Graphics.screen, this);
            Graphics.Graphics.Init();

            //ENTITIES
            Entities.Entities.Init();

            UI.UI.Init();

            Physics.Physics.watch = new Stopwatch();
            Physics.Physics.sampleTimer.Start();


            GameStateManager.gameState = GameStateManager.GameStates.playState;
            GameStateManager.gameMode = GameStateManager.GameModes.playMode;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            GameStateManager.CheckGameStatusChange();

            Graphics.Graphics.gameTime = gameTime;
            Inputs.Inputs.Update();
            Physics.Physics.Update();
            UI.UI.UIManager.Update();
            Entities.Entities.entityManager.Update();

            Graphics.Graphics.Update();

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
