using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class KillCommand : IConsoleCommand
    {
        public string Name => "kill";
        public string Description => "Kill";
        public bool IsForDebug => true;

        public void Execute(string[] args)
        {
            Entities.Entities.EntityManager.RemoveEntity(Entities.Entities.Player);
            Console.WriteLine("Killed");
        }
    }
}
