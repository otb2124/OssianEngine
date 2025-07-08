using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class SpawnCommand : IConsoleCommand
    {
        public string Name => "spawn";
        public string Description => "Spawn an entity [entity_type]";
        public bool IsForDebug => true;

        public void Execute(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: /spawn [entity_type]");
                return;
            }
            string entityType = args[0];
            Console.WriteLine($"Spawning entity: {entityType}");
            // Add logic to spawn entity, e.g., Entities.entityManager.SpawnEntity(entityType);
        }
    }
}
