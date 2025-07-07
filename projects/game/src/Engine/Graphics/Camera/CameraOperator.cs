using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;

namespace Graphics
{
    public class CameraOperator
    {

        public Camera camera;
        public float cameraSpeed = 2f;

        public CameraOperator(Camera camera)
        {
            this.camera = camera;
        }

        public void Update()
        {
            Vector2 mapSize = Entities.Entities.entityMapManager.maps[Entities.Entities.entityMapManager.CurrentMapId].Size.ToVector2();
            Vector2 screenSize = new Vector2(Graphics.screen.Width, Graphics.screen.Height);

            // Calculate bounds with zoom adjustment
            float currentZoom = (float)camera.Z;
            float baseZoom = camera.MaxZ;
            float adjScale = currentZoom / baseZoom;

            // Apply zoom scaling to all bounds
            float topBound = (mapSize.Y - screenSize.Y * 1.5f) / adjScale;
            float bottomBound = (-mapSize.Y + screenSize.Y * 1.5f) / adjScale;
            float leftBound = (-mapSize.X + screenSize.X * 1.5f) / adjScale;
            float rightBound = (mapSize.X - screenSize.X * 1.5f) / adjScale;

            Vector2 newPos = camera.position;

            // Movement
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAUPPRESSED] && newPos.Y < topBound)
            {
                camera.MoveUp(cameraSpeed);
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERADOWNPRESSED] && newPos.Y > bottomBound)
            {
                camera.MoveUp(-cameraSpeed);
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERARIGHTPRESSED] && newPos.X < rightBound)
            {
                camera.MoveRight(cameraSpeed);
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERALEFTPRESSED] && newPos.X > leftBound)
            {
                camera.MoveRight(-cameraSpeed);
            }

            // Zoom
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAZOOMUPPRESSED])
            {
                camera.MoveZ(-2f);
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAZOOMDOWNPRESSED])
            {
                camera.MoveZ(2f);
            }
        }
    }
}
