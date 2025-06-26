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


            /*
            if (game.gameState == game.PLAYSTATE)
            {
                camera.position = FlatConverter.ToVector2(game.player.Body.Position);
            }
            else if (game.gameState == game.DIALOGUESTATE)
            {
                if (game.player.isInterracting != null)
                {


                    if (!camera.hasMoved)
                    {
                        //MoveFor(FlatConverter.ToVector2(game.aiManager.GetClosest(game.player, typeof(NPC)).Body.Position), 10);
                    }
                    else
                    {

                    }

                }

            }
            else if (game.gameState == game.MENUSTATE)
            {
                camera.position = Vector2.Zero;
            }
            */


            //camera.position = Vector2.Zero;

            Vector2 mapSize = Entities.Entities.entityMapManager.maps[Entities.Entities.entityMapManager.CurrentMapId].Size.ToVector2();
            Vector2 screenSize = new Vector2(Graphics.screen.Width, Graphics.screen.Height);

            float topBound = mapSize.Y - screenSize.Y * 1.5f;
            float bottomBound = -mapSize.Y + screenSize.Y * 1.5f;
                //+ screenSize.Y * 0.5f;
            float leftBound = -mapSize.X + screenSize.X * 1.5f;
            float rightBound = mapSize.X - screenSize.X * 1.5f;

            float currentZoom = (float)camera.Z;
            float baseZoom = camera.MaxZ;
            float adjScale = currentZoom / baseZoom;

            topBound /= adjScale;
            bottomBound /= adjScale;
            leftBound /= adjScale;
            rightBound /= adjScale;


            //Debug.WriteLine(bounds.ToString());


            Vector2 newPos = new Vector2(camera.position.X, camera.position.Y); 


                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAUPPRESSED] && newPos.Y <= topBound)
                {
                    camera.MoveUp(cameraSpeed);
                }
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERADOWNPRESSED] && newPos.Y >= bottomBound)
                {
                    camera.MoveUp(-cameraSpeed);
                }

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERARIGHTPRESSED] && newPos.X <= rightBound)
                {
                    camera.MoveRight(cameraSpeed);
                }
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERALEFTPRESSED] && newPos.X >= leftBound)
                {
                    camera.MoveRight(-cameraSpeed);
                }
            


            //zoom
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
