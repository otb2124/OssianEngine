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
            return Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.CAMERADOWNPRESSED] |
                Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.CAMERAUPPRESSED] |
                Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.CAMERALEFTPRESSED] |
                Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.CAMERARIGHTPRESSED];
        }

        public static bool isCameraZooming()
        {
            return Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.CAMERAZOOMUPPRESSED] |
                Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.CAMERAZOOMDOWNPRESSED];
        }
    }
}
