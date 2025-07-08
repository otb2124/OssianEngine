using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class GodCommand : IConsoleCommand
    {

        public string Name => "god";
        public string Description => "God mode";
        public bool IsForDebug => true;

        public void Execute(string[] args)
        {
            GameStateManager.IsGod = !GameStateManager.IsGod;
            Console.WriteLine($"God mode {(GameStateManager.IsGod ? "enabled" : "disabled")}");
        }
    }
}
