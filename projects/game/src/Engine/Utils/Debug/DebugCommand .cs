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
        public string Description => "Toggle debug mode";

        public void Execute(string[] args)
        {
            bool isDebugMode = GameStateManager.gameMode == GameStateManager.GameModes.debugMode;
            GameStateManager.gameMode = isDebugMode ? GameStateManager.GameModes.playMode : GameStateManager.GameModes.debugMode;

            Console.WriteLine($"Debug mode {(isDebugMode ? "disabled" : "enabled")}");
        }
    }
}
