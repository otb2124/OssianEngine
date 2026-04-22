using Microsoft.Xna.Framework;
using Resources;
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
        public int CountLimit = 30;

        public BackgroundEntityDynamicy Dynamicy;
        public DynamicBackgroundEvents Type;

        public DynamicBackgroundEvent(DynamicBackgroundEvents eventType)
        {
            Type = eventType;
        }

        public void Update()
        {
            for (int i = 0; i < Graphics.BackgroundManager.entities.Count; i++)
            {
                if (Graphics.BackgroundManager.entities[i] is DynamicBackgroundEntity ent)
                {
                    ent.Update();
                }
            }

            SpawnTimer += (float)Graphics.CurrentLogicTime;

            if (SpawnTimer >= SpawnInterval)
            {
                int dynamicEntityCount = Graphics.BackgroundManager.entities.Count(e => e is DynamicBackgroundEntity);
                if (dynamicEntityCount < CountLimit)
                {
                    SpawnEntity();
                }
                SpawnTimer = 0f;
            }
        }

        public static void SpawnEntity()
        {
            int layerCount = Graphics.BackgroundManager.parallax.ParallaxBackLayers.Length;
            int randomLayerId = RandomHelper.RandomInteger(0, layerCount + 1);
            float layerYOffset = layerCount > 0 ? randomLayerId * Graphics.Screen.Height / layerCount : 0;
            if (randomLayerId >= layerCount)
            {
                layerYOffset = layerCount > 0 ? (layerCount - 1) * Graphics.Screen.Height / layerCount : 0;
            }

            Directions direction = RandomHelper.RandomInteger(0, 2) == 0 ? Directions.LEFT : Directions.RIGHT;

            float spawnX = direction == Directions.LEFT
                ? Graphics.Camera.Position.X + Graphics.Screen.Width / 2 + 200
                : Graphics.Camera.Position.X - Graphics.Screen.Width / 2 - 200;
            float spawnY = RandomHelper.RandomFloating(
                Graphics.Camera.Position.Y - (Graphics.Screen.Height / 2) - layerYOffset,
                Graphics.Camera.Position.Y + (Graphics.Screen.Height / 2) - layerYOffset
            );

            Graphics.BackgroundManager.AddEntity(new DynamicBackgroundEntity(StaticSprites.GRAPHICS_CLOUD_0, new Vector2(spawnX, spawnY), randomLayerId, direction));
        }
    }
}
