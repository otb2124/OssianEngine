using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class RefillCommand : IConsoleCommand
    {
        public string Name => "refill";
        public string Description => "RefillAll Player stats";
        public bool IsForDebug => true;

        public void Execute(string[] args)
        {
            Entities.Entities.Player.StatsManager.RefillAll();
            Console.WriteLine($"Player StatsManager Refilled");
        }
    }
}
