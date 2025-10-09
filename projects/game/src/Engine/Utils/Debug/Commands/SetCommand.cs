using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class SetCommand : IConsoleCommand
    {
        public string Name => "set";
        public string Description => "Set a entity's stat /stat [entity_type] [stat] [value], /stat [stat] [value]";
        public bool IsForDebug => true;

        public void Execute(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("Usage: /stat [entity_type] [stat] [value], /stat [stat] [value]");
                return;
            }

            string stat = args[0];
            string value = args[1];

            if (stat == "hp")
            {
                Entities.Entities.Player.StatsManager.IndicatorStats.HP = int.Parse(value);
            }

            Console.WriteLine($"Setting entities {args[0]} to: {args[1]}");
        }
    }
}
