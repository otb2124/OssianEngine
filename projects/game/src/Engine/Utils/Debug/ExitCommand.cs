using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class ExitCommand : IConsoleCommand
    {

        public string Name => "exit";
        public string Description => "Exit the game";

        public void Execute(string[] args)
        {
            if (args.Length != 0)
            {
                Console.WriteLine("Usage: /exit");
                return;
            }
            GameStateManager.RequestExit();
        }
    }
}
