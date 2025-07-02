using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class DebugCommand : IConsoleCommand
    {
        public string Name => "debug";
        public string Description => "Toggle debug mode [on|off]";


        public void Execute(string[] args)
        {
            if (args.Length != 1 || (args[0] != "on" && args[0] != "off"))
            {
                Console.WriteLine("Usage: /debug [on|off]");
                return;
            }
            bool debugOn = args[0] == "on";

            GameStateManager.gameMode = debugOn ? GameStateManager.GameModes.debugMode : GameStateManager.GameModes.playMode;

            Console.WriteLine($"Debug mode {(debugOn ? "enabled" : "disabled")}");
        }
    }
}
