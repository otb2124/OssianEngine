using Microsoft.Xna.Framework;
using Resources;
using System;
using Utils;

namespace App
{
    public class Game : Microsoft.Xna.Framework.Game
    {

        ConsoleCommandManager commandManager;

        public Game()
        {
            Graphics.Graphics.OnGameObjectConstruction(this);
        }

        protected override void Initialize()
        {
            Graphics.Graphics.SetGameProps(this);
            Physics.Physics.Init();
            Inputs.Inputs.Init();
            Entities.Entities.Init();
            Graphics.Graphics.Init();
            UI.UI.Init();
            Sounds.Sounds.Init();

            GameStateManager.SetDefault();
            commandManager = new ConsoleCommandManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            ResourceLoader.LoadResources();
            Entities.Entities.EntityMapManager.LoadInitialMap();
        }

        protected override void Update(GameTime gameTime)
        {
            GameStateManager.CheckGameStatusChange();

            Graphics.Graphics.UpdateGameTime(gameTime);
            Inputs.Inputs.Update();
            Physics.Physics.Update();
            Entities.Entities.Update();

            commandManager.ProcessCommands();

            Graphics.Graphics.Update();
            UI.UI.UIManager.Update();
            Sounds.Sounds.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
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
