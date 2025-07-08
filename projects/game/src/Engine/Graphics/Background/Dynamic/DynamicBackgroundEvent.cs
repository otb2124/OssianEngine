using Microsoft.Xna.Framework;
using System.Linq;
using Utils;

namespace Graphics
{
    public class DynamicBackgroundEvent
    {
        public enum BackgroundEntityDynamicy
        {
            LEFTRIGHT
        }

        public enum DynamicBackgroundEvents
        {
            CLOUDY_SKY,
        }

        public float SpawnTimer = 0f;
        public float SpawnInterval = 0.25f;
        public int CountLimit = 60;

        public BackgroundEntityDynamicy Dynamicy;
        public DynamicBackgroundEvents Type;

        public DynamicBackgroundEvent(DynamicBackgroundEvents eventType)
        {
            Type = eventType;
        }

        public void Update()
        {
            for (int i = 0; i < Graphics.backgroundManager.entities.Count; i++)
            {
                if (Graphics.backgroundManager.entities[i] is DynamicBackgroundEntity ent)
                {
                    ent.Update();
                }
            }

            SpawnTimer += (float)Graphics.gameTime.ElapsedGameTime.TotalSeconds;

            if (SpawnTimer >= SpawnInterval)
            {
                int dynamicEntityCount = Graphics.backgroundManager.entities.Count(e => e is DynamicBackgroundEntity);
                if (dynamicEntityCount < CountLimit)
                {
                    SpawnEntity();
                }
                SpawnTimer = 0f;
            }
        }

        public static void SpawnEntity()
        {
            int layerCount = Graphics.backgroundManager.parallax.ParallaxBackLayers.Length;
            int randomLayerId = RandomHelper.RandomInteger(0, layerCount + 1);
            float layerYOffset = layerCount > 0 ? randomLayerId * Graphics.screen.Height / layerCount : 0;
            if (randomLayerId >= layerCount)
            {
                layerYOffset = layerCount > 0 ? (layerCount - 1) * Graphics.screen.Height / layerCount : 0;
            }

            Directions direction = RandomHelper.RandomInteger(0, 2) == 0 ? Directions.LEFT : Directions.RIGHT;

            float spawnX = direction == Directions.LEFT
                ? Graphics.camera.Position.X + Graphics.screen.Width / 2 + 200
                : Graphics.camera.Position.X - Graphics.screen.Width / 2 - 200;
            float spawnY = RandomHelper.RandomFloating(
                Graphics.camera.Position.Y - (Graphics.screen.Height / 2) - layerYOffset,
                Graphics.camera.Position.Y + (Graphics.screen.Height / 2) - layerYOffset
            );

            Graphics.backgroundManager.AddEntity(new DynamicBackgroundEntity(StaticSprites.GRAPHICS_CLOUD_0, new Vector2(spawnX, spawnY), randomLayerId, direction));
        }
    }
}
