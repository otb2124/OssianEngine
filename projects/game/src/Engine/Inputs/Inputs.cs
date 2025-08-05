using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inputs
{
    public static class Inputs
    {
        public static FlatKeyboard keyboard;
        public static FlatMouse mouse;
        public static KeyHandler keyHandler;

        public static void Init()
        {
            keyHandler = new KeyHandler();
        }

        public static void Update()
        {
            keyboard = FlatKeyboard.Instance;
            mouse = FlatMouse.Instance;
            keyboard.Update();
            mouse.Update();
            keyHandler.Update();
        }
    }
}
