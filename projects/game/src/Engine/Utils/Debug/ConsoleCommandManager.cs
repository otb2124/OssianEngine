using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils;

namespace Utils
{

    public class ConsoleCommandManager : IDisposable
    {
        private ConcurrentQueue<string> commandQueue;
        private readonly Dictionary<string, IConsoleCommand> commands;
        private Thread inputThread; 
        private volatile bool isRunning;

        public ConsoleCommandManager()
        {
            Console.WriteLine("Console allocated. Command Prompt Ready. Use Minecraft-style commands (e.g., /spawn entity). Type /help for commands.");
            commandQueue = new ConcurrentQueue<string>();
            commands = new Dictionary<string, IConsoleCommand>();
            isRunning = true;
            inputThread = new Thread(ReadConsoleInput);
            inputThread.IsBackground = true;

            // Register commands
            RegisterCommands();

            Console.WriteLine("Starting console input thread...");
            inputThread.Start();
        }

        private void RegisterCommands()
        {
            var commandList = new List<IConsoleCommand>
            {
                new ExitCommand(),
                new DevModeCommand(),
                new SpawnCommand(),
                new ClearCommand(),
                new RefillCommand(),
                new GodCommand()
            };
            commands.Add("help", new HelpCommand(commandList));
            foreach (var command in commandList)
            {
                commands.Add(command.Name, command);
            }
        }

        private void ReadConsoleInput()
        {
            while (isRunning)
            {
                string input = Console.ReadLine();
                if (input != null && input.Trim().StartsWith("/"))
                {
                    commandQueue.Enqueue(input.Trim());
                }
                else if (!string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid command. CommandPool must start with '/'. Type /help for commands.");
                }
            }
        }

        public void ProcessCommands()
        {
            while (commandQueue.TryDequeue(out string command))
            {
                ExecuteCommand(command);
            }
        }

        private void ExecuteCommand(string command)
        {
            string[] parts = command.Substring(1).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                Console.WriteLine("Empty command. Type /help for commands.");
                return;
            }

            string commandName = parts[0].ToLower();
            string[] args = parts.Skip(1).ToArray();

            if (commands.TryGetValue(commandName, out var cmd) && !(cmd.IsForDebug && !GameStateManager.IsDevMode))
            {
                cmd.Execute(args);
            }
            else
            {
                Console.WriteLine($"Unknown command: {commandName}. Type /help for commands.");
            }
        }

        public void Dispose()
        {
            isRunning = false;
        }
    }
}

