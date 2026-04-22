using Microsoft.Xna.Framework;
using Resources;
using Utils;

namespace Graphics
{
    public class DynamicBackgroundEntity : BackgroundEntity
    {

        public float Speed;
        public Directions Direction;

        public DynamicBackgroundEntity(StaticSprites spritePreset, Vector2 pos, int layer, Directions direction) : base(spritePreset, pos, layer)
        {
            Speed = 0.25f * (layer + 1);
            Direction = direction;
            isStickToCamera = false;
            isStickToZoom = false;
        }

        public void Update()
        {
            float movement = Speed;

            if (LayerToDrawOn < Graphics.BackgroundManager.parallax.ParallaxBackLayers.Length)
            {
                movement = Speed;//find a way to bind to the parallax layer speeds
            }

            pos.X += Direction == Directions.RIGHT ? movement : -movement;

            float cameraLeft = Graphics.Camera.position.X - Graphics.Screen.Width / 2;
            float cameraRight = Graphics.Camera.position.X + Graphics.Screen.Width / 2;
            if (Direction == Directions.RIGHT && pos.X > cameraRight + 200)
            {
                Graphics.BackgroundManager.RemoveEntity(this);
            }
            else if (Direction == Directions.LEFT && pos.X < cameraLeft - 200)
            {
                Graphics.BackgroundManager.RemoveEntity(this);
            }
        }

    }
}
