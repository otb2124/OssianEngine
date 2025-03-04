using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;

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


            //move
            Rectangle bounds = Graphics.backgroundManager.parallax.bounds;
            Vector2 newPos = new Vector2((camera.position.X + cameraSpeed)*(float)(camera.Z/camera.BaseZ), camera.position.Y + cameraSpeed*(float)(camera.Z / camera.BaseZ)); 


                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAUPPRESSED] && newPos.Y <= bounds.Height)
                {
                    camera.MoveUp(cameraSpeed);
                }
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERADOWNPRESSED] && newPos.Y >= bounds.Y)
                {
                    camera.MoveUp(-cameraSpeed);
                }

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERARIGHTPRESSED] && newPos.X <= bounds.Width)
                {
                    camera.MoveRight(cameraSpeed);
                }
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERALEFTPRESSED] && newPos.X >= bounds.X)
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
