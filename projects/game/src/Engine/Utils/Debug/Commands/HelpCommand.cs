using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class HelpCommand : IConsoleCommand
    {

        private readonly IEnumerable commands;
        public string Name => "help";
        public string Description => "Show available commands";
        public bool IsForDebug => false;

        public HelpCommand(IEnumerable<IConsoleCommand> commands)
        {
            this.commands = commands;
        }

        public void Execute(string[] args)
        {
            Console.WriteLine("Available commands:");
            foreach (IConsoleCommand command in commands)
            {
                if(!command.IsForDebug || (command.IsForDebug && GameStateManager.IsDevMode) || command is DevModeCommand)
                {
                    Console.WriteLine($"/{command.Name} - {command.Description}");
                }
            }

            if(GameStateManager.IsDevMode)
            {
                Console.WriteLine("DEBUG MODE: F3");
            }
        }
    }
}
