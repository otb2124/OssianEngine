using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class KeyHandlerUtil
    {


        public static bool isPlayerMoving()
        {
            return Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] |
                Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED] |
                Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED] |
                Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.BLOCKPRESSED];
        }


        public static bool isCameraMoving()
        {
            return Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERADOWNPRESSED] |
                Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAUPPRESSED] |
                Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERALEFTPRESSED] |
                Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERARIGHTPRESSED];
        }

        public static bool isCameraZooming()
        {
            return Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAZOOMUPPRESSED] |
                Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.CAMERAZOOMDOWNPRESSED];
        }
    }
}
