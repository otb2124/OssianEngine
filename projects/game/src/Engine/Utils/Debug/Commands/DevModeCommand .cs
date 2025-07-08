using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class DevModeCommand : IConsoleCommand
    {
        public string Name => "dev";
        public string Description => "Toggle dev mode";
        public bool IsForDebug => false;

        public void Execute(string[] args)
        {
            GameStateManager.IsDevMode = !GameStateManager.IsDevMode;
            Console.WriteLine($"Dev mode {(GameStateManager.IsDevMode ? "enabled" : "disabled")}");
        }
    }
}
