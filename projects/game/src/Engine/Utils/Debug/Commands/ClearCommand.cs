using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class ClearCommand : IConsoleCommand
    {

        public string Name => "clear";
        public string Description => "Clear the console";
        public bool IsForDebug => false;

        public void Execute(string[] args)
        {
            if (args.Length != 0)
            {
                Console.WriteLine("Usage: /clear");
                return;
            }
            Console.Clear();
            Console.WriteLine("Command Prompt Ready. Type /help for commands.");
        }
    }
}
