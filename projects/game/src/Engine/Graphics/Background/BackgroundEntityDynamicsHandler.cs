using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using Utils;

namespace Graphics
{
    public static class BackgroundEntityDynamicsHandler
    {
        private static float cloudSpawnTimer = 0f;
        private const float CloudSpawnInterval = 0.5f;

        public static void Handle()
        {
            foreach (var background in Graphics.backgroundManager.backgrounds)
            {
                switch (background.type)
                {
                    case BackgroundEntity.BGEntityDynamics.STATIC:
                        break;
                    case BackgroundEntity.BGEntityDynamics.CLOUD:
                        HandleCloudLogic(background);
                        break;
                }
            }

            HandleStateLogic();
        }

        public static void HandleCloudLogic(BackgroundEntity background)
        {
            background.pos.X -= 0.5f;

            if (background.pos.X < (Graphics.camera.Position.X - Graphics.screen.Width/2) - 200)
            {
                Graphics.backgroundManager.RemoveEntity(background);
            }
        }

        public static void HandleStateLogic()
        {
            switch (Graphics.backgroundManager.state)
            {
                case BackgroundManager.BGState.NONE:
                    break;
                case BackgroundManager.BGState.CLOUDS:
                    HandleCloudStateLogic();
                    break;
            }
        }

        public static void HandleCloudStateLogic()
        {
            cloudSpawnTimer += (float)Graphics.gameTime.ElapsedGameTime.TotalSeconds;

            if (cloudSpawnTimer >= CloudSpawnInterval)
            {
                SpawnCloud();
                cloudSpawnTimer = 0f;
            }
        }

        private static void SpawnCloud()
        {
            float spawnX = Graphics.camera.Position.X + Graphics.screen.Width + 50;
            float spawnY = RandomHelper.RandomFloating(Graphics.camera.Position.Y - (Graphics.screen.Height + 100), Graphics.camera.Position.Y + (Graphics.screen.Height + 100)); 

            BackgroundEntity newCloud = new BackgroundEntity(StaticSprites.BG_CLOUD_0, new Vector2(spawnX, spawnY), BackgroundEntity.BGEntityDynamics.CLOUD);
            Graphics.backgroundManager.AddEntity(newCloud);
        }
    }
}
