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
        public string Description => "Refill player stats";

        public void Execute(string[] args)
        {
            Entities.Entities.player.Stats.Refill();

            Console.WriteLine($"Player Stats Refilled");
        }
    }
}
