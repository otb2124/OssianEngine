using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Inputs
{
    public class KeyHandler
    {

        public enum KeyStates
        {
            JUMPPRESSED,
            MOVERIGHTPRESSED,
            MOVELEFTPRESSED,
            CAMERALEFTPRESSED,
            CAMERARIGHTPRESSED,
            CAMERAUPPRESSED,
            CAMERADOWNPRESSED,
            CAMERAZOOMUPPRESSED,
            CAMERAZOOMDOWNPRESSED,
        }

        public Dictionary<KeyStates, bool> keyStates = new Dictionary<KeyStates, bool>
        {
            { KeyStates.JUMPPRESSED, false },
            { KeyStates.MOVERIGHTPRESSED, false },
            { KeyStates.MOVELEFTPRESSED, false },
            { KeyStates.CAMERALEFTPRESSED, false },
            { KeyStates.CAMERARIGHTPRESSED, false },
            { KeyStates.CAMERAUPPRESSED, false },
            { KeyStates.CAMERADOWNPRESSED, false },
            { KeyStates.CAMERAZOOMUPPRESSED, false },
            { KeyStates.CAMERAZOOMDOWNPRESSED, false }
        };


        public Dictionary<(KeyStates state, bool clickOnly), Keys> keyBindings = new Dictionary<(KeyStates, bool), Keys>
        {
            { (KeyStates.JUMPPRESSED, true), Keys.Space },
            { (KeyStates.MOVERIGHTPRESSED, false), Keys.D },
            { (KeyStates.MOVELEFTPRESSED, false), Keys.A },
            { (KeyStates.CAMERALEFTPRESSED, false), Keys.Left },
            { (KeyStates.CAMERARIGHTPRESSED, false), Keys.Right },
            { (KeyStates.CAMERAUPPRESSED, false), Keys.Up },
            { (KeyStates.CAMERADOWNPRESSED, false), Keys.Down },
            { (KeyStates.CAMERAZOOMUPPRESSED, false), Keys.OemPlus },
            { (KeyStates.CAMERAZOOMDOWNPRESSED, false), Keys.OemMinus }
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
