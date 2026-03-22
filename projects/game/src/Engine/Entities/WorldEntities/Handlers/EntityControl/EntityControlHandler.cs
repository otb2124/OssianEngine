using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Inputs.KeyHandler;

namespace Entities
{
    public class EntityControlHandler
    {

        public bool ApplyUIPrevention = false;

        public static KeyStates[] Controls =
        {
            //main
            KeyStates.JUMPPRESSED,
            KeyStates.MOVERIGHTPRESSED,
            KeyStates.MOVELEFTPRESSED,
            KeyStates.MOVEDOWNPRESSED,
            KeyStates.MOVEUPPRESSED,
            KeyStates.ATTACKLIGHTPRESSED,
            KeyStates.ATTACKHEAVYPRESSED,
            KeyStates.TOGGLEWEAPONPRESSED,
            KeyStates.SPRINTPRESSED,
            KeyStates.INTERACTRESSED,
            KeyStates.BLOCKPRESSED,
            KeyStates.PARRYPRESSED,

            //camera
            KeyStates.CAMERALEFTPRESSED,
            KeyStates.CAMERARIGHTPRESSED,
            KeyStates.CAMERAUPPRESSED,
            KeyStates.CAMERADOWNPRESSED,

            KeyStates.CAMERAZOOMUPPRESSED,
            KeyStates.CAMERAZOOMDOWNPRESSED,

            //ui
            KeyStates.TOGGLEMENUPRESSED,
            KeyStates.TOGGLEHUDPRESSED,

            //debug
            KeyStates.TOGGLECOLLISIONDEBUGPRESSED,
            KeyStates.TOGGLEHITBOXDEBUGPRESSED
        };

        public Dictionary<KeyStates, bool> ControlStateMap;

        public EntityControlHandler(bool applyUIPrevention)
        {

            ControlStateMap = new Dictionary<KeyStates, bool>();

            ResetAllStates();
            ApplyUIPrevention = applyUIPrevention;
        }

        public void SetState(KeyStates key, bool pressed)
        {
            if(ApplyUIPrevention && 
               //UI.UI.PreventButtonPressedOverlap && 
               (key == KeyStates.ATTACKLIGHTPRESSED || key == KeyStates.ATTACKHEAVYPRESSED))
               return;

            ControlStateMap[key] = pressed;
        }

        public bool IsPressed(KeyStates key)
        {
            return ControlStateMap.TryGetValue(key, out bool pressed) && pressed;
        }

        public void ResetAllStates()
        {
            foreach (var state in Controls)
            {
                ControlStateMap[state] = false;
            }
        }
        }
}
