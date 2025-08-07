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
    public class EntityAICommandManager
    {
        public Queue<EntityAICommand> CurrentQueue;

        public EntityAICommand CurrentCommand;
        public bool IsExecutingCommand;

        public EntityAICommandManager()
        {
            CurrentQueue = new Queue<EntityAICommand>();
        }

        public void Update(EntityAIBehaviourManager behaviourManager)
        {
            if (!IsExecutingCommand && CurrentQueue.Count > 0)
            {
                CurrentCommand = CurrentQueue.Dequeue();
                CurrentCommand.CommandTime = 0f;
                IsExecutingCommand = true;
            }

            if (IsExecutingCommand && CurrentCommand != null)
            {
                CurrentCommand.CommandTime++;

                if (CurrentCommand.CommandTime >= CurrentCommand.CurrentDuration * Graphics.Graphics.UpdatesPerSecond && !CurrentCommand.IsDurationInfinite)
                {
                    behaviourManager.UpdateCommands(CurrentQueue, CurrentCommand);
                    IsExecutingCommand = false;
                    return;
                }

                CurrentCommand.CommandAction(CurrentCommand);
            }
            else
            {
                behaviourManager.UpdateCommands(CurrentQueue, null);
            }
        }
    }
}
