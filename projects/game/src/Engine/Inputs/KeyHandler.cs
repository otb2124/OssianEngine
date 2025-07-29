using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Inputs
{
    public class KeyHandler
    {

        public enum KeyStates
        {
            //player keys
            JUMPPRESSED,
            MOVERIGHTPRESSED,
            MOVELEFTPRESSED,
            MOVEDOWNPRESSED,
            ATTACKPRESSED,
            TOGGLEWEAPONPRESSED,
            SPRINTPRESSED,
            INTERACTRESSED,
            PARRYPRESSED,
            BLOCKPRESSED,

            //camera
            CAMERALEFTPRESSED,
            CAMERARIGHTPRESSED,
            CAMERAUPPRESSED,
            CAMERADOWNPRESSED,

            CAMERAZOOMUPPRESSED,
            CAMERAZOOMDOWNPRESSED,

            //UI
            TOGGLEMENUPRESSED,
            TOGGLEHUDPRESSED,

            //debug
            TOGGLEDEBUGPRESSED
        }

        public Dictionary<KeyStates, bool> keyStates = new Dictionary<KeyStates, bool>
        {
            //player keys
            { KeyStates.JUMPPRESSED, false },
            { KeyStates.MOVERIGHTPRESSED, false },
            { KeyStates.MOVELEFTPRESSED, false },
            { KeyStates.MOVEDOWNPRESSED, false },
            { KeyStates.ATTACKPRESSED, false },
            { KeyStates.TOGGLEWEAPONPRESSED, false },
            { KeyStates.SPRINTPRESSED, false },
            { KeyStates.INTERACTRESSED, false },
            { KeyStates.BLOCKPRESSED, false },
            { KeyStates.PARRYPRESSED, false },

            //camera
            { KeyStates.CAMERALEFTPRESSED, false },
            { KeyStates.CAMERARIGHTPRESSED, false },
            { KeyStates.CAMERAUPPRESSED, false },
            { KeyStates.CAMERADOWNPRESSED, false },

            { KeyStates.CAMERAZOOMUPPRESSED, false },
            { KeyStates.CAMERAZOOMDOWNPRESSED, false },

            //ui
            { KeyStates.TOGGLEMENUPRESSED, false },
            { KeyStates.TOGGLEHUDPRESSED, false },

            //debug
            { KeyStates.TOGGLEDEBUGPRESSED, false }
        };


        public Dictionary<(KeyStates state, bool clickOnly), Keys> keyBindings = new Dictionary<(KeyStates, bool), Keys>
        {
            //player keys
            { (KeyStates.MOVERIGHTPRESSED, false), Keys.D },
            { (KeyStates.MOVELEFTPRESSED, false), Keys.A },
            { (KeyStates.MOVEDOWNPRESSED, false), Keys.S },

            { (KeyStates.SPRINTPRESSED, false), Keys.LeftShift },

            { (KeyStates.JUMPPRESSED, false), Keys.Space },

            { (KeyStates.INTERACTRESSED, true), Keys.E },

            { (KeyStates.BLOCKPRESSED, false), Keys.LeftAlt },

            { (KeyStates.TOGGLEWEAPONPRESSED, true), Keys.Q },

            { (KeyStates.PARRYPRESSED, false), Keys.LeftControl },

            { (KeyStates.ATTACKPRESSED, true), Keys.Enter },
            
            
            

            //camera
            { (KeyStates.CAMERALEFTPRESSED, false), Keys.Left },
            { (KeyStates.CAMERARIGHTPRESSED, false), Keys.Right },
            { (KeyStates.CAMERAUPPRESSED, false), Keys.Up },
            { (KeyStates.CAMERADOWNPRESSED, false), Keys.Down },

            { (KeyStates.CAMERAZOOMUPPRESSED, false), Keys.OemPlus },
            { (KeyStates.CAMERAZOOMDOWNPRESSED, false), Keys.OemMinus },

            //ui
            { (KeyStates.TOGGLEMENUPRESSED, true), Keys.Escape },
            { (KeyStates.TOGGLEHUDPRESSED, true), Keys.F1 },

            //debug
            { (KeyStates.TOGGLEDEBUGPRESSED, true), Keys.F3 },
        };

        

        public KeyHandler() {

            foreach (var (state, _) in keyBindings.Keys)
            {
                if (!keyStates.ContainsKey(state))
                {
                    keyStates[state] = false;
                }
            }
        }

        public void Update()
        {
            HandleKeyClicks();
            HandleKeyPresses();
            HandleKeyReleases();
        }

        private void HandleKeyClicks()
        {
            foreach (var (state, clickOnly) in keyBindings.Keys)
            {
                if (clickOnly && Inputs.keyboard.IsKeyClicked(keyBindings[(state, clickOnly)]))
                {
                    keyStates[state] = true;
                }
                else if(clickOnly && !Inputs.keyboard.IsKeyClicked(keyBindings[(state, clickOnly)]))
                {
                    keyStates[state] = false; 
                }
            }
        }

        private void HandleKeyPresses()
        {
            foreach (var (state, clickOnly) in keyBindings.Keys)
            {
                if (!clickOnly && Inputs.keyboard.IsKeyDown(keyBindings[(state, clickOnly)]))
                {
                    keyStates[state] = true;
                }
            }
        }

        private void HandleKeyReleases()
        {
            foreach (var (state, clickOnly) in keyBindings.Keys)
            {
                if (!clickOnly && Inputs.keyboard.IsKeyReleased(keyBindings[(state, clickOnly)]))
                {
                    keyStates[state] = false;
                }
            }
        }


        private bool isAnyPressed()
        {
            return Inputs.keyboard.GetPressedKeys().Count > 0 || Inputs.mouse.GetPressedButtons().Count > 0;
        }
    }
}
