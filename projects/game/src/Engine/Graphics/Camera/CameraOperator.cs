using Microsoft.Xna.Framework;
using Physics;
using System;

namespace Graphics
{
    public class CameraOperator
    {
        public float cameraSpeed = 5f; 
        public Vector2 targetPosition; 
        private readonly float transitionSpeed = 0.05f;

        public CameraOperator()
        {
            targetPosition = Graphics.camera.position;
        }

        public void Update()
        {
            targetPosition = PhysicalConverter.ToVector2(Entities.Entities.Player.Model.Body.Position);

            Vector2 mapSize = Entities.Entities.EntityMapManager.GetCurrentMap().Size.ToVector2() / 2f;
            Vector2 screenSize = new Vector2(Graphics.screen.Width, Graphics.screen.Height);

            // Zoom
            float currentZoom = (float)Graphics.camera.Z;
            float initialZoom = (float)Graphics.camera.MaxZ*1.5f;
            float adjScale = currentZoom / initialZoom;

            // Camera screen bounds (adjusted for zoom)
            float cameraLeft = targetPosition.X - (screenSize.X / 2 * adjScale);
            float cameraRight = targetPosition.X + (screenSize.X / 2 * adjScale);
            float cameraTop = targetPosition.Y + (screenSize.Y / 2 * adjScale);
            float cameraBottom = targetPosition.Y - (screenSize.Y / 2 * adjScale);

            // World bounds
            float leftBound = -mapSize.X;
            float rightBound = mapSize.X;
            float topBound = mapSize.Y;
            float bottomBound = -mapSize.Y;

            // Camera movement
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERAUPPRESSED])
            {
                targetPosition.Y += cameraSpeed;
            }
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERADOWNPRESSED])
            {
                targetPosition.Y -= cameraSpeed;
            }
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERARIGHTPRESSED])
            {
                targetPosition.X += cameraSpeed;
            }
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERALEFTPRESSED])
            {
                targetPosition.X -= cameraSpeed;
            }

            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERAZOOMUPPRESSED])
            {
                Graphics.camera.MoveZ(-2f);
            }
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERAZOOMDOWNPRESSED])
            {
                Graphics.camera.MoveZ(2f);
            }

            float clampedX = targetPosition.X;
            float clampedY = targetPosition.Y;

            if (cameraLeft < leftBound)
            {
                targetPosition.X += leftBound - cameraLeft;
            }
            else if (cameraRight > rightBound)
            {
                targetPosition.X += rightBound - cameraRight;
            }

            if (cameraBottom < bottomBound)
            {
                targetPosition.Y += bottomBound - cameraBottom;
            }
            else if (cameraTop > topBound)
            {
                targetPosition.Y += topBound - cameraTop;
            }

            Graphics.camera.position = Vector2.Lerp(Graphics.camera.position, targetPosition, transitionSpeed);
        }
    }
}

