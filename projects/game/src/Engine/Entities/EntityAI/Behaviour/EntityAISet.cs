using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.EntityAIBehaviourManager;

namespace Entities
{
    public class EntityAISet
    {

        public EntityAICommandManager AIManager;
        public EntityAIBehaviourManager BehaviourManager;

        public EntityAISet(AIEntity entity, BehaviourPatterns bPattern, BehaviourCases bCase)
        {
            AIManager = new EntityAICommandManager();
            BehaviourManager = new EntityAIBehaviourManager(bPattern);
            BehaviourManager.UpdateCurrentCase(AIManager.CurrentQueue, entity, bCase);
        }

        public void Update(AIEntity entity)
        {
            BehaviourManager.UpdateCurrentCase(AIManager.CurrentQueue, entity, EntityAIHelper.GetBehaviourCase(entity));
            AIManager.Update(BehaviourManager);
        }
    }
}
