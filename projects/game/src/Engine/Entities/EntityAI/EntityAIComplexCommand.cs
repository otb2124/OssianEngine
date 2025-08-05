using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EntityAIComplexCommand
    {
        public float TotalDurationSec;
        public EntityAICommand[] Commands;
        public int CurrentCommandId;

        public bool UnInitialized = true;

        public EntityAIComplexCommand(EntityAICommand[] commands)
        {
            Commands = commands;
            CurrentCommandId = 0;

            foreach (EntityAICommand command in commands)
            {
                TotalDurationSec += command.Duration;
            }

            UnInitialized = false;
        }

        public EntityAIComplexCommand()
        {
            
        }

        public void Execute()
        {
            Commands[CurrentCommandId].CommandAction(Commands[CurrentCommandId]);

            if (Commands[CurrentCommandId].CommandTime > Commands[CurrentCommandId].Duration)
            {
                CurrentCommandId++;
            }
        }
    }
}
