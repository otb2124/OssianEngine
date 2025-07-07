using Microsoft.Xna.Framework;
using Physics;
using System;

namespace Graphics
{
    public class CameraOperator
    {
        public Camera camera; 
        public float cameraSpeed = 5f; 
        private Vector2 targetPosition; 
        private readonly float transitionSpeed = 0.05f;

        public CameraOperator(Camera camera)
        {
            this.camera = camera;
            targetPosition = camera.position;
        }

        public void Update()
        {
            Vector2 mapSize = Entities.Entities.entityMapManager.maps[Entities.Entities.entityMapManager.CurrentMapId].Size.ToVector2();
            Vector2 screenSize = new Vector2(Graphics.screen.Width, Graphics.screen.Height);

            float currentZoom = (float)camera.Z;
            float baseZoom = (float)camera.GetZFromHeight(Graphics.screen.Height);
            float adjScale = currentZoom / baseZoom;

            // Calculate bounds
            float leftBound = (-mapSize.X + screenSize.X * 1.5f) / adjScale;
            float rightBound = (mapSize.X - screenSize.X * 1.5f) / adjScale;
            float bottomBound = (-mapSize.Y + screenSize.Y * 1.5f) / adjScale;
            float topBound = (mapSize.Y - screenSize.Y * 1.5f) / adjScale;

            Vector2 playerPosition = FlatConverter.ToVector2(Entities.Entities.player.Model.body.Position);

            float distToLeft = playerPosition.X - leftBound;
            float distToRight = rightBound - playerPosition.X;

            float minDistance = MathHelper.Min(distToLeft, distToRight);
            

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAUPPRESSED])
            {
                targetPosition.Y += cameraSpeed;
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERADOWNPRESSED])
            {
                targetPosition.Y -= cameraSpeed;
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERARIGHTPRESSED])
            {
                targetPosition.X += cameraSpeed;
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERALEFTPRESSED])
            {
                targetPosition.X -= cameraSpeed;
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAZOOMUPPRESSED])
            {
                camera.MoveZ(-2f);
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAZOOMDOWNPRESSED])
            {
                camera.MoveZ(2f);
            }

            targetPosition.X = MathHelper.Clamp(targetPosition.X, leftBound, rightBound);
            targetPosition.Y = MathHelper.Clamp(targetPosition.Y, bottomBound, topBound);

            camera.position = Vector2.Lerp(camera.position, targetPosition, transitionSpeed);
        }
    }
}

