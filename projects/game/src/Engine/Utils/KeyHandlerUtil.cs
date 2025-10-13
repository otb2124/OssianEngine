using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class KeyHandlerUtil
    {




        public static bool isCameraMoving()
        {
            return Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERADOWNPRESSED] |
                Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERAUPPRESSED] |
                Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERALEFTPRESSED] |
                Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERARIGHTPRESSED];
        }

        public static bool isCameraZooming()
        {
            return Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERAZOOMUPPRESSED] |
                Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.CAMERAZOOMDOWNPRESSED];
        }
    }
}
