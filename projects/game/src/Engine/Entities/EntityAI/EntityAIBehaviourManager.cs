using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class EntityAIBehaviourManager
    {
        public enum BehaviourPatterns
        {
            ANIMAL_DEFAULT,
        };

        public enum BehaviourCases
        {
            NONE,
            IDLE,
            AGGRO,
        };


        public EntityAICommand[] CommandPool;
        public BehaviourPatterns Pattern;
        public BehaviourCases CurrentCase;

        private int commandsExecutedSinceLastRepeat = 0;

        public EntityAIBehaviourManager(BehaviourPatterns pattern)
        {
            Pattern = pattern;
            CurrentCase = BehaviourCases.NONE;
        }

        public void UpdateCommands(Queue<EntityAICommand> currentQueue, EntityAICommand lastCompletedCommand)
        {
            if (lastCompletedCommand != null)
            {
                commandsExecutedSinceLastRepeat++;
                if (lastCompletedCommand.RepeatAfterCommandsCount > 0 &&
                    commandsExecutedSinceLastRepeat >= lastCompletedCommand.RepeatAfterCommandsCount)
                {
                    currentQueue.Enqueue(lastCompletedCommand);
                    commandsExecutedSinceLastRepeat = 0;
                }
            }

            if (currentQueue.Count == 0)
            {
                foreach (var command in CommandPool)
                {
                    if (command.RepeatAfterRestart)
                    {
                        currentQueue.Enqueue(command);
                    }
                }
                commandsExecutedSinceLastRepeat = 0;
            }
        }

        public void UpdateCurrentCase(Queue<EntityAICommand> currentQueue, StatsEntity entity, BehaviourCases bCase)
        {
            if(CurrentCase == bCase) { return; }
                
            CurrentCase = bCase;

            switch (Pattern)
            {
                case BehaviourPatterns.ANIMAL_DEFAULT:
                    switch (CurrentCase)
                    {
                        case BehaviourCases.IDLE:

                            CommandPool = new EntityAICommand[]
                            {
                                //new EntityAICommand(entity => { entity.Move(Directions.RIGHT); },   5f),
                                //new EntityAICommand(entity => { entity.Move(Directions.LEFT); },    6f),
                                //new EntityAICommand(entity => { entity.Move(Directions.RIGHT); },   7f),
                                //new EntityAICommand(entity => { entity.Move(Directions.LEFT); },    5f),
                                new EntityAICommand(entity => { entity.Move(Directions.RIGHT); },   5f, true),
                                new EntityAICommand(entity => { entity.Jump(); },                   1.5f, true),
                                new EntityAICommand(entity => { entity.Move(Directions.LEFT); },    5f, true),
                                new EntityAICommand(entity => { entity.Jump(); },                   1.5f, true),
                                new EntityAICommand(entity => { entity.StandStill(); },             3f, true),
                            };
                            
                            break;
                    }
                    break;
            }


            foreach (var command in CommandPool)
            {
                command.Entity = entity;
            }


            currentQueue.Clear();
            foreach (var command in CommandPool)
            {
                currentQueue.Enqueue(command);
            }

        }
    }
}
