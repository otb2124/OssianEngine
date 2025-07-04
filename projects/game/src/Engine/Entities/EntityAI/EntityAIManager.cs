using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class EntityAIManager
    {
        private Queue<EntityAICommand> CurrentQueue;
        
        private EntityAICommand CurrentCommand;
        public bool IsExecutingCommand;

        public EntityAIBehaviourManager BehaviourManager;

        public EntityAIManager(EntityAIBehaviourManager.BehaviourPatterns Pattern)
        {
            CurrentQueue = new Queue<EntityAICommand>();
            BehaviourManager = new EntityAIBehaviourManager(Pattern);
        }

        public void Update(StatsEntity entity, EntityAIBehaviourManager.BehaviourCases currentCase)
        {
            BehaviourManager.UpdateCurrentCase(CurrentQueue, entity, currentCase);
            UpdateCommandExecution();
        }

        public void UpdateCommandExecution()
        {
            if (!IsExecutingCommand && CurrentQueue.Count > 0)
            {
                CurrentCommand = CurrentQueue.Dequeue();
                CurrentCommand.CommandTime = 0f;
                IsExecutingCommand = true;
                Console.WriteLine(CurrentCommand.Duration);
            }

            if (IsExecutingCommand && CurrentCommand != null)
            {
                CurrentCommand.CommandTime++;

                if (CurrentCommand.CommandTime >= CurrentCommand.Duration * 60f && !CurrentCommand.IsDurationInfinite)
                {
                    BehaviourManager.UpdateCommands(CurrentQueue, CurrentCommand);
                    IsExecutingCommand = false;
                    return;
                }

                CurrentCommand.CommandAction(CurrentCommand);
            }
            else
            {
                BehaviourManager.UpdateCommands(CurrentQueue, null);
            }
        }
    }
}
