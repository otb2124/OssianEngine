using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;

namespace Graphics
{
    public class CameraOperator
    {

        Camera camera;

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
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAUPPRESSED])
            {
                camera.MoveUp(2f);
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERADOWNPRESSED])
            {
                camera.MoveUp(-2f);
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERARIGHTPRESSED])
            {
                camera.MoveRight(2f);
            }
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERALEFTPRESSED])
            {
                camera.MoveRight(-2f);
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
