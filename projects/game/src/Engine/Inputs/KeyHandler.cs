using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Inputs
{
    public class KeyHandler
    {

        public struct InputKey
        {
            public bool IsMouseButton;
            public Keys? KeyboardKey;
            public FlatMouse.MouseButtons? MouseButton;

            public InputKey(Keys key)
            {
                IsMouseButton = false;
                KeyboardKey = key;
                MouseButton = null;
            }

            public InputKey(FlatMouse.MouseButtons button)
            {
                IsMouseButton = true;
                KeyboardKey = null;
                MouseButton = button;
            }
        }

        public enum KeyStates
        {
            //Player keys
            JUMPPRESSED,
            MOVERIGHTPRESSED,
            MOVELEFTPRESSED,
            MOVEDOWNPRESSED,
            MOVEUPPRESSED,
            ATTACKLIGHTPRESSED,
            ATTACKHEAVYPRESSED,
            TOGGLEWEAPONPRESSED,
            SPRINTPRESSED,
            INTERACTRESSED,
            PARRYPRESSED,
            BLOCKPRESSED,

            //Camera
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
            TOGGLECOLLISIONDEBUGPRESSED,
            TOGGLEHITBOXDEBUGPRESSED
        }

        public Dictionary<KeyStates, bool> KeyStateMap = new Dictionary<KeyStates, bool>
        {
            //Player keys
            { KeyStates.JUMPPRESSED, false },
            { KeyStates.MOVERIGHTPRESSED, false },
            { KeyStates.MOVELEFTPRESSED, false },
            { KeyStates.MOVEDOWNPRESSED, false },
            { KeyStates.MOVEUPPRESSED, false },
            { KeyStates.ATTACKLIGHTPRESSED, false },
            { KeyStates.ATTACKHEAVYPRESSED, false },
            { KeyStates.TOGGLEWEAPONPRESSED, false },
            { KeyStates.SPRINTPRESSED, false },
            { KeyStates.INTERACTRESSED, false },
            { KeyStates.BLOCKPRESSED, false },
            { KeyStates.PARRYPRESSED, false },

            //Camera
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
            { KeyStates.TOGGLECOLLISIONDEBUGPRESSED, false },
            { KeyStates.TOGGLEHITBOXDEBUGPRESSED, false }
        };


        public Dictionary<(KeyStates state, bool clickOnly), List<InputKey>> KeyBindings = new Dictionary<(KeyStates, bool), List<InputKey>>
        {
            //Player keys
            { (KeyStates.MOVERIGHTPRESSED, false), new List<InputKey> { new InputKey(Keys.D) } },
            { (KeyStates.MOVELEFTPRESSED, false), new List<InputKey> { new InputKey(Keys.A) } },
            { (KeyStates.MOVEDOWNPRESSED, false), new List<InputKey> { new InputKey(Keys.S) } },
            { (KeyStates.MOVEUPPRESSED, false), new List<InputKey> { new InputKey(Keys.W) } },
            { (KeyStates.SPRINTPRESSED, false), new List<InputKey> { new InputKey(Keys.LeftShift) } },
            { (KeyStates.JUMPPRESSED, false), new List<InputKey> { new InputKey(Keys.Space) } },
            { (KeyStates.INTERACTRESSED, true), new List<InputKey> { new InputKey(Keys.E) } },
            { (KeyStates.BLOCKPRESSED, false), new List<InputKey> { new InputKey(Keys.LeftAlt) } },
            { (KeyStates.TOGGLEWEAPONPRESSED, true), new List<InputKey> { new InputKey(Keys.R),  new InputKey(Keys.CapsLock) } },
            { (KeyStates.PARRYPRESSED, false), new List<InputKey> { new InputKey(Keys.LeftControl) } },
            { (KeyStates.ATTACKLIGHTPRESSED, true), new List<InputKey> { new InputKey(FlatMouse.MouseButtons.Left) } },
            { (KeyStates.ATTACKHEAVYPRESSED, true), new List<InputKey> { new InputKey(FlatMouse.MouseButtons.Right) } },

            //Camera
            { (KeyStates.CAMERALEFTPRESSED, false), new List<InputKey> { new InputKey(Keys.Left) } },
            { (KeyStates.CAMERARIGHTPRESSED, false), new List<InputKey> { new InputKey(Keys.Right) } },
            { (KeyStates.CAMERAUPPRESSED, false), new List<InputKey> { new InputKey(Keys.Up) } },
            { (KeyStates.CAMERADOWNPRESSED, false), new List<InputKey> { new InputKey(Keys.Down) } },
            { (KeyStates.CAMERAZOOMUPPRESSED, false), new List<InputKey> { new InputKey(Keys.OemPlus) } },
            { (KeyStates.CAMERAZOOMDOWNPRESSED, false), new List<InputKey> { new InputKey(Keys.OemMinus) } },

            //ui
            { (KeyStates.TOGGLEMENUPRESSED, true), new List<InputKey> { new InputKey(Keys.Escape) } },
            { (KeyStates.TOGGLEHUDPRESSED, true), new List<InputKey> { new InputKey(Keys.F1) } },

            //debug
            { (KeyStates.TOGGLECOLLISIONDEBUGPRESSED, true), new List<InputKey> { new InputKey(Keys.F3) } },
            { (KeyStates.TOGGLEHITBOXDEBUGPRESSED, true), new List<InputKey> { new InputKey(Keys.F4) } },
        };


        public KeyHandler() {

            foreach (var (state, _) in KeyBindings.Keys)
            {
                if (!KeyStateMap.ContainsKey(state))
                {
                    KeyStateMap[state] = false;
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
            foreach (var ((state, clickOnly), bindings) in KeyBindings)
            {
                if (!clickOnly) continue;

                bool isPressed = false;
                foreach (var binding in bindings)
                {
                    if (binding.IsMouseButton)
                    {
                        isPressed |= Inputs.mouse.IsMouseButtonPressed(binding.MouseButton!.Value);
                    }
                    else
                    {
                        isPressed |= Inputs.keyboard.IsKeyClicked(binding.KeyboardKey!.Value);
                    }
                }

                KeyStateMap[state] = isPressed;
            }
        }

        private void HandleKeyPresses()
        {
            foreach (var ((state, clickOnly), bindings) in KeyBindings)
            {
                if (clickOnly) continue;

                bool isPressed = false;
                foreach (var binding in bindings)
                {
                    if (binding.IsMouseButton)
                    {
                        isPressed |= Inputs.mouse.IsMouseButtonDown(binding.MouseButton!.Value);
                    }
                    else
                    {
                        isPressed |= Inputs.keyboard.IsKeyDown(binding.KeyboardKey!.Value);
                    }
                }

                KeyStateMap[state] = isPressed;
            }
        }

        private void HandleKeyReleases()
        {
            foreach (var ((state, clickOnly), bindings) in KeyBindings)
            {
                if (clickOnly) continue;

                bool allReleased = true;
                foreach (var binding in bindings)
                {
                    bool isReleased = false;
                    if (binding.IsMouseButton)
                    {
                        isReleased = Inputs.mouse.IsMouseButtonReleased(binding.MouseButton!.Value);
                    }
                    else
                    {
                        isReleased = Inputs.keyboard.IsKeyReleased(binding.KeyboardKey!.Value);
                    }
                    allReleased &= isReleased;
                }

                if (allReleased)
                {
                    KeyStateMap[state] = false;
                }
            }
        }


        private bool IsAnyPressed()
        {
            return Inputs.keyboard.GetPressedKeys().Count > 0 || Inputs.mouse.GetPressedButtons().Count > 0;
        }
    }
}
